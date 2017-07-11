using SimpleForum.Logic.Contracts;
using SimpleForum.Logic.DTO.Topic;
using SimpleForum.Logic.Infrastructure;
using SimpleForum.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleForum.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService service;

        public TopicController(ITopicService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(TopicCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TopicCreateDTO topicDTO = new TopicCreateDTO
            {
                Title = model.Title,
                Description = model.Description,
                CreatorLogin = HttpContext.User.Identity.Name
            };

            ServiceMessage serviceMessage = service.Create(topicDTO);
            if (serviceMessage.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (string error in serviceMessage.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(model);
            }
        }
    }
}