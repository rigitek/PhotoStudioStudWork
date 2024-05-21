
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PhotoStudio.Models
{
    public class PhotoStudioContext:DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Photographer> Photographers { get; set; }
        public DbSet<PhotoSession> PhotoSessions { get; set; }
        public DbSet<TypeOfPhotoSession> TypeOfPhotoSessions { get; set; }

        public PhotoStudioContext()
        {
           Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=PhotoStudio.db");
        }
    }
}
