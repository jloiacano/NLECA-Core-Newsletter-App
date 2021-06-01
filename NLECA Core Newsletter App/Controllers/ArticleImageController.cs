using Microsoft.AspNetCore.Authorization;
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

        public ArticleImageController(
            ILogger<ArticleImageController> logger
            , IArticleService articleService
            , IArticleImageService imageService
            , UserManager<ApplicationIdentityUser> userManager)
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