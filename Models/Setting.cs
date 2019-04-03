using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema ;

namespace MathsExercise.Models
{
    public class Setting
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(36)]
        public string GUIDValue { get; set; }
        [MaxLength(4)]
        public string Operations { get; set; }
        public int Amount { get; set;}
        
        public int QuantityOfOperation { get; set; }
        public int MaxValue { get; set; }
        public DateTime CreateTime { get; set; }

        public List<MathsExercises> _mathsExercises { get; set; }

    }
}