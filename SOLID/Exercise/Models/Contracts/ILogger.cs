using System.Collections.Generic;

namespace SolidExercise.Models.Contracts
{
    public interface ILogger
    {
        IReadOnlyCollection<IAppender> Appenders { get; }
        void Log(IError error);
    }
}
