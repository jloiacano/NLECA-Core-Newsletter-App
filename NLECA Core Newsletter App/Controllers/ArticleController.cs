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
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
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
            ArticleModel model = _articleService.GetArticleByArticleId(articleId);
            return View(model);
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UpdateArticle(int newsletterId, int articleId, string oldArticleText, string articleText)
        {
            if (oldArticleText.Trim().Equals(articleText.Trim()) == false)
            {
                ArticleModel article = _articleService.GetArticleByArticleId(articleId);
                article.ArticleText = articleText.Trim();

                _articleService.UpdateArticle(article);
            }
            return RedirectToAction("EditNewsletter", "Newsletter", new { newsletterId });
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult RemoveArticle(int newsletterId, int articleId)
        {
            _articleService.DeleteArticle(articleId);
            return RedirectToAction("EditNewsletter", "Newsletter", new { newsletterId });
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult SaveArticle(ArticleModel article)
        {
            _articleService.UpdateArticle(article);
            return RedirectToAction("EditNewsletter", "Newsletter", new { article.NewsletterId });
        }
    }
}