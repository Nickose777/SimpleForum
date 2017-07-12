using AutoMapper;
using SimpleForum.Helpers;
using SimpleForum.Logic.Contracts;
using SimpleForum.Logic.DTO.Message;
using SimpleForum.Logic.Infrastructure;
using SimpleForum.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleForum.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService service;

        public MessageController(IMessageService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(MessageCreateModel model)
        {
            bool success = true;
            string html = "";

            if (!ModelState.IsValid)
            {
                success = false;
            }
            else
            {
                MessageCreateDTO messageDTO = Mapper.Map<MessageCreateModel, MessageCreateDTO>(model);
                ServiceMessage serviceMessage = service.Create(messageDTO);

                if (!serviceMessage.Succeeded)
                {
                    foreach (string error in serviceMessage.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }

                    success = false;
                }
            }

            if (!success)
            {
                html = RenderHelper.PartialView(this, "~/Views/Message/Create.cshtml", model);
            }

            return Json(new { success = success, html = html });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Get(int id)
        {
            bool success = true;
            string html = "";

            DataServiceMessage<MessageEditDTO> serviceMessage = service.Get(id);
            if (serviceMessage.Succeeded)
            {
                MessageEditModel model = Mapper.Map<MessageEditDTO, MessageEditModel>(serviceMessage.Data);
                
                html = RenderHelper.PartialView(this, "~/Views/Message/Edit.cshtml", model);
            }
            else
            {
                success = false;
                foreach (string error in serviceMessage.Errors)
                {
                    html += String.Format("<p>{0}</p>", error);
                }
            }

            return Json(new { success = success, html = html }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(MessageEditModel model)
        {
            bool success = true;
            string html = "";

            if (!ModelState.IsValid)
            {
                success = false;
            }
            else
            {
                MessageEditDTO messageDTO = Mapper.Map<MessageEditModel, MessageEditDTO>(model);
                ServiceMessage serviceMessage = service.Edit(messageDTO);
                if (!serviceMessage.Succeeded)
                {
                    success = false;
                    foreach (string error in serviceMessage.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            if (!success)
            {
                html = RenderHelper.PartialView(this, "~/Views/Message/Edit.cshtml", model);
            }

            return Json(new { success = success, html = html });
        }
    }
}