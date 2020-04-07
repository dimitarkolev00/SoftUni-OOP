using System;
using System.Linq;
using SolidExercise.Core.Contracts;
using SolidExercise.Factories;
using SolidExercise.Models.Contracts;
using SolidExercise.Models.Errors;

namespace SolidExercise.Core
{
    public class Engine : IEngine
    {
        private ILogger logger;
        private ErrorFactory errorFactory;
        private Engine()
        {
            this.errorFactory = new ErrorFactory();
        }
        public Engine(ILogger logger)
        : this()
        {
            this.logger = logger;
        }
        public void Run()
        {
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] inputArgs = input
                    .Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string level = inputArgs[0];
                string dateTime = inputArgs[1];
                string message = inputArgs[2];

                try
                {
                    IError error = this.errorFactory.ProduceError(dateTime, message, level);
                    this.logger.Log(error);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }

            }

            Console.WriteLine(this.logger);
        }

    }
}
