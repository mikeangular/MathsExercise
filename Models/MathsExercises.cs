using System;
using System.Collections.Generic;

namespace MathsExercise.Models
{
    public class MathsExercises
    {
        public int ID { get; set; }
        public string HashValue { get; set; }
        public string Formula { get; set; }
        public string Anwser { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime SaveTime { get; set; }
        

        // public ICollection<Enrollment> Enrollments { get; set; }
    }
}