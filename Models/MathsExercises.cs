using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema ;
using MathsExercise.Models;
namespace MathsExercise.Models
{
    public class MathsExercises
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int SettingId { get; set; }
        [MaxLength(500)]
        public string Formula { get; set; }
        [MaxLength(20)]
        public string UserAnswer { get; set;}
        [MaxLength(20)]
        public string RightAnswer { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? SaveTime { get; set; }
        
        public Setting _setting { get; set;}
        // public ICollection<Enrollment> Enrollments { get; set; }
    }
}