using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.ActionFilters;
using WebProject.Entities;
using WebProject.ExtentionMethods;
using WebProject.Repositories;
using WebProject.ViewModels.Songs;

namespace WebProject.Controllers
{
    [AuthenticationFilter]
    public class SongsController : Controller
    {
        public IActionResult Index()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            WebProjectDbContext context = new WebProjectDbContext();
            IndexVM model = new IndexVM();
            model.Items = context.Songs.Where(p => p.OwnerId == loggedUser.Id).ToList();

            model.Items.AddRange(context.UserToSongs
                                            .Where(i => i.UserId == loggedUser.Id)
                                            .Select(i => i.Song)
                                            .ToList());


            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            CreateVM model = new CreateVM();
            model.OwnerId = loggedUser.Id;

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            if (model.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");

                return View(model);
            }

            WebProjectDbContext context = new WebProjectDbContext();

            Song item = new Song();
            item.Tittle = model.Tittle;
            item.Genre = model.Genre;
            item.OwnerId = model.OwnerId;

            context.Songs.Add(item);
            context.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            WebProjectDbContext context = new WebProjectDbContext();

            Song item = context.Songs.Where(p => p.Id == id).FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Index", "Songs");
            }

            if (item.OwnerId != loggedUser.Id)
            {
                return RedirectToAction("Index", "Songs");
            }

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.OwnerId = item.OwnerId;
            model.Tittle = item.Tittle;
            model.Genre = item.Genre;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            WebProjectDbContext context = new WebProjectDbContext();

            Song item = context.Songs
                                    .Where(p => p.Id == model.Id)
                                    .FirstOrDefault();

            if (item.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return View(model);
            }

            if (model.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return View(model);
            }

            item.Id = model.Id;
            item.OwnerId = model.OwnerId;
            item.Genre = model.Genre;
            item.Tittle = model.Tittle;

            context.Songs.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Songs");
        }

        public IActionResult Delete(int id)
        {
            WebProjectDbContext context = new WebProjectDbContext();

            Song item = new Song();
            item.Id = id;

            var shared = context.UserToSongs
                .Where(s => s.SongId == id)
                .FirstOrDefault();

            if (shared != null)
            {
                context.UserToSongs.Remove(shared);
            }

            context.Songs.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Songs");
        }

        [HttpGet]
        public IActionResult Share(int id)
        {
            WebProjectDbContext context = new WebProjectDbContext();

            ShareVM model = new ShareVM();
            model.Song = context.Songs.Where(p => p.Id == id).FirstOrDefault();

            model.Shares = context.UserToSongs.Where(i => i.SongId == id).ToList();

            List<int> usersSheredList = model.Shares.Select(i => i.UserId).ToList();



            usersSheredList.Add(model.Song.OwnerId);
            model.Users = context.Users.Where(i => !usersSheredList.Contains(i.Id)).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Share(ShareVM model)
        {
            WebProjectDbContext context = new WebProjectDbContext();

            foreach (int userId in model.UserIds)
            {
                UserToSong item = new UserToSong();
                item.UserId = userId;
                item.SongId = model.SongId;

                context.UserToSongs.Add(item);
                context.SaveChanges();
            }

            return RedirectToAction("Share", "Songs", new { Id = model.SongId });
        }

        [HttpGet]
        public IActionResult RevokeShare(int id)
        {
            WebProjectDbContext context = new WebProjectDbContext();

            UserToSong item = context.UserToSongs
                                        .Where(i => i.Id == id)
                                        .FirstOrDefault();

            context.UserToSongs.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Share", "Songs", new { Id = item.SongId });
        }
    }
}
