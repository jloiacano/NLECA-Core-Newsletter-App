using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly ILogger<NewsletterController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INewsletterService _newsletter;
        private readonly IArticleService _articleService;

        public NewsletterController(ILogger<NewsletterController> logger, IConfiguration config, IHttpContextAccessor httpContextAccessor, INewsletterService newsletter, IArticleService articleService)
        {
            _logger = logger;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _newsletter = newsletter;
            _articleService = articleService;
        }


        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult Index()
        {
            return RedirectToAction("NewsletterManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult NewsletterManager()
        {
            List<NewsletterModel> model = _newsletter.GetAllNewsletters();
            return View(model);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult AddNewsletter()
        {
            int newsletterId = _newsletter.AddNewsletter();

            return RedirectToAction("EditNewsletter", new { newsletterId = newsletterId });
        }


        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult EditNewsletter(int newsletterId)
        {
            NewsletterModel model = _newsletter.GetNewsletterById(newsletterId);
            model.IsEdit = true;
            return View(model);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UpdateNewsletter(int newsletterId, string oldMemo, string memo, string oldDisplayDate, string displayDate)
        {
            // handle possible nulls.
            oldMemo = string.IsNullOrEmpty(oldMemo) ? "" : oldMemo.Trim();
            memo = string.IsNullOrEmpty(memo) ? "" : memo.Trim();
            oldDisplayDate = string.IsNullOrEmpty(oldDisplayDate) ? "" : oldDisplayDate.Trim();
            displayDate = string.IsNullOrEmpty(displayDate) ? "" : displayDate.Trim();

            bool memoChanged = (oldMemo.Equals(memo) == false);
            bool displayDateChanged = (oldDisplayDate.Equals(displayDate) == false);

            if (memoChanged || displayDateChanged)
            {
                NewsletterModel oldNewsletter = _newsletter.GetNewsletterById(newsletterId);
                if (memoChanged)
                {
                    oldNewsletter.Memo = memo;
                }
                if (displayDateChanged)
                {
                    oldNewsletter.DisplayDate = displayDate;
                }
                bool successfulUpdate = _newsletter.UpdateNewsletter(oldNewsletter);
            }

            return RedirectToAction("EditNewsletter", new { newsletterId = newsletterId });
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult RemoveNewsletter(int newsletterId)
        {
            if (User.IsInRole("ReadOnlyUser"))
            {
                return RedirectToAction("NewsletterManager");
            }

            try
            {
                _newsletter.DeleteNewsletter(newsletterId);
            }
            catch (Exception ex)
            {
                string error = string.Format("There was an error deleting a newsletter with the id {0}", newsletterId);
                _logger.LogError(error, ex);
            }
                
            return RedirectToAction("NewsletterManager");
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public IActionResult SaveNewsletter(NewsletterModel newsletter)
        {
            NewsletterModel model = newsletter;
            return RedirectToAction("NewsletterManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult PublishNewsletter(int newsletterId)
        {
            _newsletter.PublishNewsletter(newsletterId);
            return RedirectToAction("NewsletterManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UnpublishNewsletter(int newsletterId)
        {
            _newsletter.UnpublishNewsletter(newsletterId);
            return RedirectToAction("NewsletterManager");
        }
    }
}