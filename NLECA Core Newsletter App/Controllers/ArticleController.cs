using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IArticleService _articleService;
        private readonly IArticleImageService _imageService;
        private readonly IAzureStorageService _azureStorageService;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public ArticleController(
            IConfiguration configuration
            , IArticleService articleService
            , IArticleImageService imageService
            , IAzureStorageService azureStorageService
            , UserManager<ApplicationIdentityUser> userManager)
        {
            _configuration = configuration;
            _articleService = articleService;
            _imageService = imageService;
            _azureStorageService = azureStorageService;
            _userManager = userManager;
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult Index()
        {
            //No index page so redirect to newsletterManager
            return RedirectToAction("/Newsletter/NewsletterManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult AddArticle(int newsletterId)
        {
            int articleId = _articleService.AddArticleToNewsletter(newsletterId);

            return RedirectToAction("EditArticle", new { articleId });
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult EditArticle(int articleId)
        {
            ArticleModel article = _articleService.GetArticleByArticleId(articleId);
            EditArticleModel model = new EditArticleModel(article);

            return View(model);
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UpdateArticle(ArticleModel article, IFormFile imageFile, string redirect)
        {
            if (imageFile != null)
            {
                CloudBlobContainer container = _azureStorageService.GetBlobContainer(_configuration["AzureStorageConnectionString"], "article-images");
                Uri storageUri = container.StorageUri.PrimaryUri;
                // USE THIS CODE IF THE STORAGE EMULATOR GETS REMADE AND GIVES YOU A HARD TIME
                //var permissions = container.GetPermissionsAsync().Result;
                //permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                //container.SetPermissionsAsync(permissions);
                ArticleImageModel articleImage = new ArticleImageModel(storageUri, imageFile);
                articleImage.UploadedByUserId = _userManager.GetUserId(this.User);
                articleImage.UploadedByUserName = this.User.Identity.Name;
                _imageService.UploadArticleImage(articleImage);

                article.ImageFileLocation = articleImage.ImageLocation;
                _articleService.UpdateArticle(article);
                return RedirectToAction("EditArticle", new { articleId = article.ArticleId });
            }
            _articleService.UpdateArticle(article);

            if (redirect.Equals("ArticleImageManager"))
            {
                return RedirectToAction("ArticleImageManager", "ArticleImage", new { articleId = article.ArticleId });
            }

            return RedirectToAction("EditNewsletter", "Newsletter", new { article.NewsletterId });
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UpdateArticleOrder(IEnumerable<string> articleIds, IEnumerable<string> newArticleOrder)
        {
            bool success = true;
            string[] newArticleOrderArray = newArticleOrder.ToArray();
            string[] articleIdsArray = articleIds.ToArray();
            for (int i = 0; i < newArticleOrderArray.Length; i++)
            {
                if (i + 1 != Int32.Parse(newArticleOrderArray[i]))
                {
                    int newArticleSequence = Int32.Parse(newArticleOrderArray[i]);
                    int articleId = Int32.Parse(articleIdsArray[i]);
                    if (!_articleService.UpdateArticleSequence(articleId, i + 1))
                    {
                        success = false;
                    }
                }
            }

            if (success)
            {
                return Json(new { success = true, responseText = "Sequences successfully updated" });
            }
            return Json(new { success = false, responseText = "There was an error updating sequences." });
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult RemoveArticle(int newsletterId, int articleId)
        {
            _articleService.DeleteArticle(articleId);
            return RedirectToAction("EditNewsletter", "Newsletter", new { newsletterId });
        }
    }
}