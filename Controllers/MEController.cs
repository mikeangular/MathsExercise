using MathsExercise.Models;
using MathsExercise.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MathsExercise.Controllers
{
    [Route("api/[controller]")]
    public class MEController
    {
        private readonly MEDBContext _context;

        public MEController(MEDBContext context)
        {
            _context = context;
        }
        [HttpGet("[action]")]
        public IEnumerable<MathsExercises> GetExample()
        {
            //return _context.Exercises.ToArray<MathsExercises>();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new MathsExercise.Models.MathsExercises
            {
                // DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                // TemperatureC = rng.Next(-20, 55),
                // Summary = Summaries[rng.Next(Summaries.Length)]
                ID = index,
                HashValue = index.ToString(), 
                Formula="1+2", 
                Anwser ="", 
                CreateTime = DateTime.Today ,
                SaveTime = DateTime.Now

            });
        }
    }
}