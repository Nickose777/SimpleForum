using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SimpleForum.Logic.Contracts;
using System.Net;
using SimpleForum.Logic.DTO.User;
using SimpleForum.Logic.Infrastructure;
using SimpleForum.Models;

namespace SimpleForum.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserRegisterDTO user = new UserRegisterDTO
            {
                Email = model.Email,
                Password = model.Password,
                Login = model.Login,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            ServiceMessage serviceMessage = service.RegisterUser(user);
            if (serviceMessage.Succeeded)
            {
                return RedirectToAction("Login");
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

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ServiceMessage serviceMessage = service.SignIn(model.Login, model.Password);
            if (serviceMessage.Succeeded)
            {
                return RedirectToAction("Index", "Home");
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

        public ActionResult LogOff()
        {
            service.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}