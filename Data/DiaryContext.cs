using Microsoft.EntityFrameworkCore;
using Diary.Models;

namespace Diary.Data
{
    public class DiaryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DiaryEntry> DiaryEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //SQLite database will be created in project folder
            optionsBuilder.UseSqlite("Data Source=diary.db");
        }
    }
}