using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Models.Event;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System.Collections.Generic;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public EventController(IEventService eventService, UserManager<ApplicationIdentityUser> userManager)
        {
            _eventService = eventService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("EventManager");
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult EventManager()
        {
            IEnumerable<EventModel> events = _eventService.GetAllEvents();
            return View(events);
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult AddEvent()
        {
            EventModel eventModel = new EventModel(_userManager.GetUserId(this.User), this.User.Identity.Name);

            int newEventId = _eventService.AddEvent(eventModel);

            return RedirectToAction("EditEvent", new { eventId = newEventId });
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult EditEvent(int eventId)
        {
            EventModel eventModel = _eventService.GetEventById(eventId);
            return View(eventModel);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UpdateEvent(EventModel eventModel)
        {
            // TODO - J - Make sure to re-validate data (dates, Is's, etc.)
            bool successfullyUpdated = _eventService.UpdateEvent(eventModel);

            return RedirectToAction("EventManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult PublishEvent(int eventId)
        {
            bool successfullyPublished = _eventService.PublishEvent(eventId);

            return RedirectToAction("EventManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UnpublishEvent(int eventId)
        {
            bool successfullyUnpublished = _eventService.UnpublishEvent(eventId);

            return RedirectToAction("EventManager");
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult DeleteEvent(int eventId)
        {
            bool successfullyDeleted = _eventService.DeleteEvent(eventId);

            return RedirectToAction("EventManager");
        }


        //EventModel GetEventById(int eventId);

        //IEnumerable<EventModel> GetAllEvents();

        //IEnumerable<EventModel> GetAllPublishedEvents();

        //IEnumerable<EventModel> GetEventsForDay(DateTime day);


        //IEnumerable<EventModel> GetEventsInDateRange(DateTime start, DateTime end);


        //bool DeleteEvent(int eventId);
    }
}