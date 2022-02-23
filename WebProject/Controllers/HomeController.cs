using Microsoft.AspNetCore.Mvc;
using WebProject.ActionFilters;
using WebProject.Entities;
using WebProject.ExtentionMethods;
using WebProject.Repositories;
using WebProject.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            WebProjectDbContext context = new WebProjectDbContext();

            User loggedUser = context.Users.Where(u => u.Username == model.Username &&
                                                       u.Password == model.Password)
                                           .FirstOrDefault();
            if (loggedUser == null)
            {
                this.ModelState.AddModelError("authError", "Invalid username or password!");
                return View(model);
            }

           

            HttpContext.Session.SetObject("loggedUser", loggedUser);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            WebProjectDbContext context = new WebProjectDbContext();

            if (!ModelState.IsValid)
                return View(model);
            

            if(model.Password != model.rPassword)
            {
                this.ModelState.AddModelError("retypeError", "Please type the password twice!");
                return View(model);
            }
            if(model.Username == null || model.Password == null || model.FirstName == null || model.LastName == null)
            {
                this.ModelState.AddModelError("emptyInput", "All fields must be filled!");
                return View(model);
            }
            

            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            context.Users.Add(item);
            context.SaveChanges();

            LoginVM loginModel = new LoginVM()
            {
                Username = model.Username,
                Password = model.Password
            };

            Login(loginModel);
            return RedirectToAction("Index", "Home");
        }

        [AuthenticationFilter]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedUser");

            return RedirectToAction("Login", "Home");
        }
    }
}
