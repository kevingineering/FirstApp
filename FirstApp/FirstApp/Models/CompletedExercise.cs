using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Models
{
    public class CompletedExercise
    {
        public CompletedExercise (string s, int w, List<int> l)
        {
            Name = s;
            Weight = w;
            AchievedReps = l;
        }
        public string Name { get; set; }            //Name of finished exercise
        public int Weight { get; set; }             //Weight used during exercise
        public List<int> AchievedReps { get; set; } //A list of the number of reps achieved
    }
}
