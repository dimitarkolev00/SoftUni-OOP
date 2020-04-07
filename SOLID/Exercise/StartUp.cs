using System;
using System.Collections.Generic;
using System.Linq;
using SolidExercise.Core;
using SolidExercise.Core.Contracts;
using SolidExercise.Factories;
using SolidExercise.Models;
using SolidExercise.Models.Contracts;

namespace SolidExercise
{
    public class StartUp
    {
         static void Main()
         {
             int appendersCount = int.Parse(Console.ReadLine());

             ICollection<IAppender> appenders= new List<IAppender>();
             PareAppendersInput(appendersCount, appenders);

             ILogger logger = new Logger(appenders);
             IEngine engine = new Engine(logger);
             engine.Run();
         }

         private static void PareAppendersInput(int appendersCount, ICollection<IAppender> appenders)
         {
             AppenderFactory appenderFactory = new AppenderFactory();

             for (int i = 0; i < appendersCount; i++)
             {
                 string[] appenderArgs = Console.ReadLine()
                     .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                     .ToArray();

                 string appenderType = appenderArgs[0];
                 string layoutType = appenderArgs[1];
                 string level = "INFO";

                 if (appenderArgs.Length == 3)
                 {
                     level = appenderArgs[2];
                 }

                 try
                 {
                     IAppender appender = appenderFactory
                         .ProduceAppender(appenderType, layoutType, level);

                     appenders.Add(appender);
                 }
                 catch (ArgumentException ae)
                 {
                     Console.WriteLine(ae.Message);
                 }
             }
         }
    }
}
