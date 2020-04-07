using System;
using SolidExercise.Models.Enumerations;

namespace SolidExercise.Models.Contracts
{
    public interface IError
    {
         DateTime DateTime { get; }
         string Message { get; }
         Level Level { get; }

    }
}
