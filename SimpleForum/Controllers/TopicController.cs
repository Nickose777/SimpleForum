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
using SimpleForum.Mappings;

namespace SimpleForum.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService service;

        public TopicController(ITopicService service)
        {
            this.service = service;
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
            bool ok = true;

            if (ModelState.IsValid)
            {
                TopicCreateDTO topicDTO = Mapper.Map<TopicCreateModel, TopicCreateDTO>(model);

                ServiceMessage serviceMessage = service.Create(topicDTO);
                if (!serviceMessage.Succeeded)
                {
                    foreach (string error in serviceMessage.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    ok = false;
                }
            }
            else
            {
                ok = false;
            }

            return ok ? RedirectToAction("List") as ActionResult : View(model);
        }

        public ActionResult List()
        {
            IEnumerable<TopicListModel> topics;

            DataServiceMessage<IEnumerable<TopicListDTO>> serviceMessage = service.GetAll();
            if (serviceMessage.Succeeded)
            {
                topics = Mapper.Instance.MapAll<TopicListDTO, TopicListModel>(serviceMessage.Data);
            }
            else
            {
                topics = new List<TopicListModel>();
            }

            return View(topics);
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