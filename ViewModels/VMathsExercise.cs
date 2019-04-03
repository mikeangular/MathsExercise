using System;

namespace MathsExercise.ViewModels
{
    public class VMathsExercise
    {
        public int ID { get; set; }
        public int SettingId { get; set; }
        public string Formula { get; set; }
        public string UserAnswer { get; set;}
        public string RightAnswer { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? SaveTime { get; set; }
    }
}