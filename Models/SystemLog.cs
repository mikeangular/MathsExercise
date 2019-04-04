using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema ;
using MathsExercise.Models;
namespace MathsExercise.Models
{
    public class SystemLog
    {
        [Key]
        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
        [MaxLength(500)]
        public string Message {get;set;}
        [MaxLength(16)]
        public string Action {get;set;}
        
        
    }
}