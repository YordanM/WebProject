using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Entities;
using WebProject.ExtentionMethods;
using WebProject.Repositories;
using WebProject.ViewModels.Playlists;

namespace WebProject.Controllers
{
    public class PlaylistsController : Controller
    {
        public IActionResult Index()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            WebProjectDbContext context = new WebProjectDbContext();

            IndexVM model = new IndexVM();
            model.Items = context.Playlists.ToList();
            model.Items = context.Playlists.Where(p => p.OwnerId == loggedUser.Id).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddSong(int playListId)
        {
            WebProjectDbContext context = new WebProjectDbContext();

            List<int> remainingSongs = context.SongToPlaylists
                                            .Where(i => i.PlaylistId == playListId)
                                            .Select(i => i.SongId)
                                            .ToList();
            List<Song> availableSongs = context.Songs
                                            .Where(i => remainingSongs.Contains(i.Id) == false)
                                            .ToList();
            AddSongVM model = new AddSongVM();

            model.Songs = availableSongs;
            return View(model);
        }

        /*[HttpPost]
        public IActionResult AddSong(AddSongVM model)
        {
            WebProjectDbContext context = new WebProjectDbContext();

            
            SongToPlaylist item = new SongToPlaylist();
            item.PlaylistId = model
            item.SongId = model.SongId;

            context.SongToPlaylists.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Playlist", new { Id = model.SongId });
        }*/

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

            Playlist item = new Playlist();
            item.Name = model.Name;
            item.OwnerId = model.OwnerId;

            context.Playlists.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Playlists");
        }

        public IActionResult Delete(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            WebProjectDbContext context = new WebProjectDbContext();

            Playlist item = context.Playlists.Where(u => u.Id == id).FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Index", "Playlists");
            }

            /*if (item.Id != loggedUser.Id)
            {
                return RedirectToAction("Index", "Playlists");
            }*/

            context.Playlists.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Playlists");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            WebProjectDbContext context = new WebProjectDbContext();

            Playlist item = context.Playlists.Where(u => u.Id == id).FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Index", "Playlists");
            }

            /*if (item.Id != loggedUser.Id)
            {
                return RedirectToAction("Index", "Playlists");
            }*/

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.Name = item.Name;
            item.OwnerId = model.OwnerId;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {

            WebProjectDbContext context = new WebProjectDbContext();

            if (!ModelState.IsValid)
                return View(model);

            Playlist item = new Playlist();
            item.Id = model.Id;
            item.Name = model.Name;
            item.OwnerId = model.OwnerId + 1;

            context.Playlists.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Playlists");
        }
    }
}
