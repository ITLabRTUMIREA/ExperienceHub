using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VRTeleportator.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace VRTeleportator
{
    public class AppDataBase : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDataBase(DbContextOptions options) : base(options)
        {
        }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<User> UserAccounts { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<UserLessons>().HasKey(t => new { t.UserId, t.LessonId });
        //}
    }
}
