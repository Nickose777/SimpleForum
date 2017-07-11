using AutoMapper;
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
            return RedirectToAction("List");
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

            TopicCreateDTO topicDTO = Mapper.Map<TopicCreateModel, TopicCreateDTO>(model);

            ServiceMessage serviceMessage = service.Create(topicDTO);
            if (serviceMessage.Succeeded)
            {
                return RedirectToAction("List");
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

        public ActionResult List()
        {
            DataServiceMessage<IEnumerable<TopicListDTO>> serviceMessage = service.GetAll();
            if (serviceMessage.Succeeded)
            {
                IEnumerable<TopicListModel> topics = serviceMessage.Data
                    .Select(topic => Mapper.Map<TopicListDTO, TopicListModel>(topic));

                return View(topics);
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            DataServiceMessage<TopicDetailsDTO> serviceMessage = service.Get(id);
            if (serviceMessage.Succeeded)
            {
                TopicDetailsModel topic = Mapper.Map<TopicDetailsDTO, TopicDetailsModel>(serviceMessage.Data);
                return View(topic);
            }
            else
            {
                return Content(String.Join(Environment.NewLine, serviceMessage.Errors));
            }
        }
    }
}