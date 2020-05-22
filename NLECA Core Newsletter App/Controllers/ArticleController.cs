using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            ArticleModel article = _articleService.GetArticleByArticleId(articleId);
            EditArticleModel model = new EditArticleModel(article);
            return View(model);
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UpdateArticle(int newsletterId, int articleId
            , string oldArticleTableOfContentsText, string articleTableOfContentsText
            , string oldArticleTitle, string articleTitle
            , string oldArticleText, string articleText
            , int oldArticleType, int articleTypeSelector)
        {
            ArticleModel oldArticle = new ArticleModel()
            {
                ArticleType = oldArticleType,
                ArticleTableOfContentsText = oldArticleTableOfContentsText,
                ArticleTitle = oldArticleTitle,
                ArticleText = oldArticleText
            };

            ArticleModel currentArticle = new ArticleModel()
            {
                ArticleType = articleTypeSelector,
                ArticleTableOfContentsText = articleTableOfContentsText,
                ArticleTitle = articleTitle,
                ArticleText = articleText
            };

            if (oldArticle != currentArticle)
            {
                ArticleModel article = _articleService.GetArticleByArticleId(articleId);
                article.ArticleType = articleTypeSelector;
                article.ArticleTableOfContentsText = articleTableOfContentsText.Trim();
                article.ArticleTitle = articleTitle.Trim();
                article.ArticleText = articleText.Trim();

                _articleService.UpdateArticle(article);
            }
            return RedirectToAction("EditNewsletter", "Newsletter", new { newsletterId });
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