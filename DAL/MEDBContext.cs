using MathsExercise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MathsExercise.DAL
{
    public class MEDBContext : DbContext
    {
        public MEDBContext(DbContextOptions<MEDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MathsExercises>()
                .HasOne(p => p._setting)
                .WithMany(b => b._mathsExercises)
                .HasForeignKey(p => p.SettingId)
                .HasPrincipalKey(b => b.ID);
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     //配置mariadb连接字符串
        //     optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=BloggingDB; User=root;Password=;");
        // }
        public DbSet<MathsExercises> Exercises { get; set; }
        public DbSet<Setting> Setting { get; set; }

    }
}