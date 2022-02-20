using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.ActionFilters;
using WebProject.Entities;
using WebProject.ExtentionMethods;
using WebProject.Repositories;
using WebProject.ViewModels.Users;

namespace WebProject.Controllers
{
    [AuthenticationFilter]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            WebProjectDbContext context = new WebProjectDbContext();

            IndexVM model = new IndexVM();
            model.Items = context.Users.ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            WebProjectDbContext context = new WebProjectDbContext();

            User item = context.Users.Where(u => u.Id == id).FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Index", "Users");
            }

            if (item.Id != loggedUser.Id)
            {
                return RedirectToAction("Index", "Users");
            }

            context.Users.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            WebProjectDbContext context = new WebProjectDbContext();

            User item = context.Users.Where(u => u.Id == id).FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Index", "Users");
            }

            if (item.Id != loggedUser.Id)
            {
                return RedirectToAction("Index", "Users");
            }

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.Username = item.Username;
            model.Password = item.Password;
            model.FirstName = item.FirstName;
            model.LastName = item.LastName;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            WebProjectDbContext context = new WebProjectDbContext();

            if (!ModelState.IsValid)
                return View(model);

            User item = new User();
            item.Id = model.Id;
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            context.Users.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }
    }
}
