using System.IO;
using SolidExercise.Models.Enumerations;

namespace SolidExercise.Models.Contracts
{
    public interface IAppender
    {
        ILayout Layout { get; }
        Level Level { get; }
        long MessagesAppended { get; }
        void Append(IError error);
    }
}
