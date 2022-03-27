using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Entities;

namespace WebProject.Repositories
{
    public class WebProjectDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SongToPlaylist> SongToPlaylists { get; set; }
        public DbSet<UserToSong> UserToSongs { get; set; }

        
        public WebProjectDbContext()
        {
            this.Users = this.Set<User>();
            this.Songs = this.Set<Song>();
            this.Playlists = this.Set<Playlist>();
            this.Comments = this.Set<Comment>();
            this.SongToPlaylists = this.Set<SongToPlaylist>();
            this.UserToSongs = this.Set<UserToSong>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=DESKTOP-6157GQF\SQLEXPRESS;Database=WebProjectDB;Trusted_Connection=True;")
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Username = "iordan",
                    Password = "iordan",
                    FirstName = "iordan",
                    LastName = "mitrev"
                },
                new User()
                {
                    Id = 2,
                    Username = "unknown",
                    Password = "unknown",
                    FirstName = "unknown",
                    LastName = "unknown"
                }
                );
        }
    }
}
