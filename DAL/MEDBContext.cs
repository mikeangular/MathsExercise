using MathsExercise.Models;
using Microsoft.EntityFrameworkCore;

namespace MathsExercise.DAL
{
    public class MEDBContext : DbContext
    {
        public MEDBContext(DbContextOptions<MEDBContext> options) : base(options)
        {
        }

        public DbSet<MathsExercise.Models.MathsExercises> Exercises { get; set; }

    }
}