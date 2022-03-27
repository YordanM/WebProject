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

            User unknownUser = context.Users.FirstOrDefault(x => x.Username == "unknown");

            User loggedUser = context.Users.Where(u => u.Username == model.Username &&
                                                       u.Password == model.Password &&
                                                       u.Id != unknownUser.Id)
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

            //checks for empty input; if exist adds error and returns view with param model
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError("emptyInput", "Username and password should be between 6-15 characters. And all the fields must be filled!");
                    return View(model);
            }
                
            //checks if entered passwords are equal; if not adds error and returns view with param model
            if(model.Password != model.rPassword)
            {
                this.ModelState.AddModelError("retypeError", "Please type the password twice!");
                return View(model);
            }


            // checks if username already exists in database; if exists adds error and returns 
            WebProjectDbContext context2 = new WebProjectDbContext();

            User user = context2.Users.Where(u => u.Username.ToLower() == model.Username.ToLower())
                                           .FirstOrDefault();
            
            if(user != null)
            {
                this.ModelState.AddModelError("usernameTaken", "The username is taken!");

                return View(model);
            }
            
            


            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            context.Users.Add(item);
            context.SaveChanges();

            //if there's no errors logging in with the new account
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
