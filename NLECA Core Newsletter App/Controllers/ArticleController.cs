using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class ArticleController : Controller
    {

        private readonly ILogger<ArticleController> _logger;
        private readonly IConfiguration _config;
        private readonly IArticleService _articleService;

        public ArticleController(ILogger<ArticleController> logger, IConfiguration config, IArticleService articleService)
        {
            _logger = logger;
            _config = config;
            _articleService = articleService;
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult Index()
        {
            //No index page so redirect to newsletterManager
            return RedirectToAction("/Newsletter/NewsletterManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult AddArticle()
        {
            ArticleModel article = new ArticleModel();
            return View(article);
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult EditArticle(int articleId)
        {
            ArticleModel model = _articleService.GetArticleById(articleId);
            return View(model);
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult RemoveArticle(int articleId)
        {
            bool success = _articleService.DeleteArticle(articleId);
            return View();
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult SaveArticle(ArticleModel article)
        {
            bool success = _articleService.EditArticle(article);
            return RedirectToAction("EditNewsletter", "Newsletter", new {newsletterId = article.NewsletterId });
        }
    }
}