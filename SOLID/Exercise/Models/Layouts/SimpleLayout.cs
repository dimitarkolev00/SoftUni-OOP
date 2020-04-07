using System;
using SolidExercise.Models.Contracts;

namespace SolidExercise.Models.Layouts
{
    public class SimpleLayout:ILayout
    {
        public string Format => "{0} - {1} - {2}";

    }
}
