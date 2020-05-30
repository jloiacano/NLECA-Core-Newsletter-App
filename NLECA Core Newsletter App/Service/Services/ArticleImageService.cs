using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class ArticleImageService : IArticleImageService
    {
        private readonly ILogger<ArticleImageService> _logger;

        public ArticleImageService(ILogger<ArticleImageService> logger)
        {
            _logger = logger;
        }

        public bool ExistsInAritcleImages(ArticleImage image)
        {
            // TODO - J - Check Images table for checksum
            return false;
        }


        public bool UploadArticleImage(ArticleImage image)
        {
            // Upload file to file structure
            string imageLocation = SaveFile(image);
            // Add entry in database with file location and checksum
            bool success = EnterImageIntoDatabase(image);
            return success;
        }


        private string SaveFile(ArticleImage image)
        {
            string path = "";
            try
            {
                path = Path.Combine(
                    Directory.GetCurrentDirectory(), 
                    "wwwroot\\Images\\ArticleImages", 
                    image.StoredImageName);

                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    image.ImageFile.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format("There was an error Saving {0} in ImageServices/SaveFile", image.ImageName);
                _logger.LogError(error, ex);
            }

            return path;
        }

        private bool EnterImageIntoDatabase(ArticleImage image)
        {
            try
            {
                // TODO - J - Update database and make call to upload image properties
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error entering {0} into database ImageServices/EnterImageIntoDatabase", 
                    image.ImageName);
                _logger.LogError(error, ex);
            }
            return true;
        }
    }

    public class ArticleImage
    {
        private byte[] FileBytes;

        public int ArticleId { get; private set; }
        public IFormFile ImageFile { get; private set; }
        public string UploadedByUserId { get; set; }
        public string UploadedByUserName { get; set; }

        public string SimpleCheckSum { get; private set; }
        public string ImageFileExtension { get; private set; }
        public string ImageName { get; private set; }
        public string StoredImageName { get; private set; }
        public string ImageLocation { get; private set; }
        public bool IsValidImageFormat { get; private set; }

        public ArticleImage(IFormFile imageFile, int articleId)
        {
            ImageFile = imageFile;
            ArticleId = articleId;
            FileBytes = ConvertFileToByteArray(imageFile);
            SimpleCheckSum = GetSimpleCheckSumFromFile();
            ImageFileExtension = "." + imageFile.FileName.Split('.')[imageFile.FileName.Split('.').Length - 1];
            ImageName = imageFile.FileName.Remove(imageFile.FileName.IndexOf(ImageFileExtension));
            StoredImageName = string.Format("{0}{1}", Guid.NewGuid().ToString(), ImageFileExtension);
            ImageLocation = string.Format("../../Images/ArticleImages/{0}", StoredImageName);
            IsValidImageFormat = GetImageFormat(FileBytes) != ImageFormat.unknown;
        }

        private string GetSimpleCheckSumFromFile()
        {
            byte[] fileBytes = this.FileBytes;

            string simpleCheckSum = GetImageByteArraySimpleCheckSum(fileBytes);

            System.Diagnostics.Debug.WriteLine("C# checksum: " + simpleCheckSum);

            return simpleCheckSum;
        }

        private enum ImageFormat
        {
            jpeg,
            png,
            gif,
            bmp,
            tiff,
            unknown
        }

        private byte[] ConvertFileToByteArray(IFormFile file)
        {
            byte[] fileBytes;

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                fileBytes = memoryStream.ToArray();
                return fileBytes;
            }
        }

        private static ImageFormat GetImageFormat(byte[] bytes)
        {
            var jpeg = new byte[] { 255, 216, 255, 224 };   // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };  // jpeg canon
            var png = new byte[] { 137, 80, 78, 71 };       // PNG
            var gif = Encoding.ASCII.GetBytes("GIF");       // GIF
            var bmp = Encoding.ASCII.GetBytes("BM");        // BMP
            var tiff = new byte[] { 73, 73, 42 };           // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };          // TIFF

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;

            return ImageFormat.unknown;
        }

        // DO NOT USE FOR HASHING ANYTHING IMPORTANT. THIS IS ONLY FOR CHECKSUMS!!
        public string GetImageByteArraySimpleCheckSum(byte[] imageByteArray)
        {
            string hash = string.Empty;
            int[] hashInts = new int[30];
            int index = 0;
            int lastRemainder = 0;
            foreach (byte imagebyte in imageByteArray)
            {
                int dividend = imagebyte/7;
                int remainder = imagebyte%7;
                hashInts[index] = hashInts[index] + lastRemainder + dividend;
                lastRemainder = remainder;
                index++;
                if (index == 30)
                {
                    index = 0;
                }
            }

            for (int i = 0; i < hashInts.Length; i++)
            {
                int preAsciiConversionDigit = hashInts[i] % 62;
                int asciiCharDigit;
                if (preAsciiConversionDigit < 10)
                {
                    asciiCharDigit = preAsciiConversionDigit + 48;
                }
                else if (preAsciiConversionDigit < 36)
                {
                    asciiCharDigit = preAsciiConversionDigit + 55;
                }
                else
                {
                    asciiCharDigit = preAsciiConversionDigit + 61;
                }

                char charToAddToHash = (char)asciiCharDigit;

                hash += charToAddToHash;
            }

            return hash;
        }
    }
}
