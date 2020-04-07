namespace SolidExercise.Models.Contracts
{
    public interface IIOManager
    {
        string CurrentDirectoryPath { get; }
        string CurrentFilePath { get; }
        string GetCurrentDirectory();
        void EnsureDirectoryAndFileExists();
    }
}
