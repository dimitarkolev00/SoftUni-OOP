using System;
using System.Globalization;
using System.IO;
using System.Linq;
using SolidExercise.Models.Contracts;
using SolidExercise.Models.Enumerations;
using SolidExercise.Models.IOManagement;

namespace SolidExercise.Models.Files
{
    public class LogFile : IFile
    {
        private IIOManager IOManager;

        public LogFile(string folderName, string fileName)
        {
            this.IOManager = new IOManager(folderName, fileName);
            this.IOManager.EnsureDirectoryAndFileExists();
        }
        public string Path => this.IOManager.CurrentFilePath;
        public long Size { get; }
        public string Write(ILayout layout, IError error)
        {
            string format = layout.Format;

            DateTime dateTime = error.DateTime;
            string message = error.Message;
            var level = error.Level;

            string formattedMessage = String.Format(format,
                dateTime.ToString("M/dd/yyyy h:mm:ss tt"),
                CultureInfo.InvariantCulture,
                message,
                level.ToString()) + Environment.NewLine;

            return formattedMessage;
        }
        private long GetFileSize()
        {
            string text = File.ReadAllText(this.Path);

            long size = text
                .Where(ch => Char.IsLetter(ch))
                .Sum(ch => ch);
            return size;
        }
    }
}
