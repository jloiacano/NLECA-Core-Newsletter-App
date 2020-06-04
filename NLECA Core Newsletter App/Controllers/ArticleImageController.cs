﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Models.ArticleImage;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System.Linq;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class ArticleImageController : Controller
    {
        private readonly ILogger<ArticleImageController> _logger;
        private readonly IArticleService _articleService;
        private readonly IArticleImageService _imageService;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public ArticleImageController(ILogger<ArticleImageController> logger, IArticleService articleService, IArticleImageService imageService, UserManager<ApplicationIdentityUser> userManager)
        {
            _logger = logger;
            _articleService = articleService;
            _imageService = imageService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ArticleImageManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult ArticleImageManager(int articleId = -1)
        {
            ArticleImageManagerModel model = new ArticleImageManagerModel()
            {
                ArticleId = articleId,
                IsSuperAdmin = User.IsInRole("SuperAdmin"),
                ArticleImages = User.IsInRole("SuperAdmin") ? 
                    _imageService.GetAllArticleImages() 
                    : _imageService.GetArticleImagesInArticles()
            };
            return View(model);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult CheckForArticleImage(string simpleCheckSum)
        {
            var imagesWithSameCheckSum = _imageService.GetArticleImagesWithSameCheckSum(simpleCheckSum);
            if (imagesWithSameCheckSum.Count() == 0)
            {
                return Json(new { success = true, fileexists = false, responseText = "Image does not exist" });
            }
            else
            {
                string images;
                images = System.Text.Json.JsonSerializer.Serialize(imagesWithSameCheckSum);
                return Json(new { success = true, fileexists = true, images = images });
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UploadArticleImage(IFormFile imageFile, int articleId)
        {
            ArticleImageModel articleImage = new ArticleImageModel(imageFile, articleId);

            if (articleImage.IsValidImageFormat
                && _imageService.ExistsInAritcleImages(articleImage.SimpleCheckSum) == false)
            {
                articleImage.UploadedByUserId = _userManager.GetUserId(this.User);
                articleImage.UploadedByUserName = this.User.Identity.Name;
                bool imageUploaded = _imageService.UploadArticleImage(articleImage);

                if (imageUploaded == false)
                {
                    string error = string.Format("There was an error uploading image: {0} to {1} for article {2}"
                        , articleImage.ImageName
                        , articleImage.ImageLocation
                        , articleId);
                    _logger.LogError(error);
                }

                ArticleModel article = _articleService.GetArticleByArticleId(articleId);
                article.ImageFileLocation = articleImage.ImageLocation;
                bool articleUpdated = _articleService.UpdateArticle(article);

                if (articleUpdated == false)
                {
                    string error = string.Format("There was an error updating article {0} with image: {1} at {2}"
                        , articleId
                        , articleImage.ImageName
                        , articleImage.ImageLocation);
                    _logger.LogError(error);
                }
            }
            return RedirectToAction("EditArticle", "Article", new { articleId });
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UseArticleImageInArticle(int articleId, string imageLocation)
        {
            ArticleModel article = _articleService.GetArticleByArticleId(articleId);
            article.ImageFileLocation = imageLocation;
            bool successfullUpdate = _articleService.UpdateArticle(article);
            return RedirectToAction("EditArticle", "Article", new { articleId });
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult RemoveArticleImageFromUse(string imageLocation)
        {
            //_imageService.RemoveArticleImage(imageLocation);
            return RedirectToAction("ArticleImageManager");
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteArticleImage(string imageLocation)
        {
            _imageService.DeleteArticleImage(imageLocation);
            return RedirectToAction("ArticleImageManager");
        }
    }
}