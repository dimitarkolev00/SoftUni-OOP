using System;
using System.Collections.Generic;
using System.Linq;

namespace P04_Hospital
{
    public class StartUp
    {
        public static void Main()
        {
            Dictionary<string, List<string>> doctors = new Dictionary<string, List<string>>();
            Dictionary<string, List<List<string>>> departments = new Dictionary<string, List<List<string>>>();

            string command = Console.ReadLine();
            while (command != "Output")
            {
                string[] tokens = command.Split();

                var departament = tokens[0];

                var doctorFirstName = tokens[1];
                var doctorLastName = tokens[2];

                var patient = tokens[3];
                var fullName = doctorFirstName + doctorLastName;

                if (!doctors.ContainsKey(fullName))
                {
                    doctors[fullName] = new List<string>();
                }
                if (!departments.ContainsKey(departament))
                {
                    departments[departament] = new List<List<string>>();

                    for (int rooms = 1; rooms <= 20; rooms++)
                    {
                        departments[departament].Add(new List<string>());
                    }
                }

                bool isFree = departments[departament]
                    .SelectMany(d => d).Count() < 60;
                if (isFree)
                {
                    int room = 0;
                    doctors[fullName].Add(patient);

                    for (int r = 0; r < departments[departament].Count; r++)
                    {
                        if (departments[departament][r].Count < 3)
                        {
                            room = r;
                            break;
                        }
                    }
                    departments[departament][room].Add(patient);
                }

                command = Console.ReadLine();
            }

            command = Console.ReadLine();

            while (command != "End")
            {
                string[] args = command.Split();

                if (args.Length == 1)
                {
                    Console.WriteLine(string.Join("\n", departments[args[0]].Where(x => x.Count > 0).SelectMany(x => x)));
                }
                else if (args.Length == 2 && int.TryParse(args[1], out int staq))
                {
                    Console.WriteLine(string.Join("\n", departments[args[0]][staq - 1].OrderBy(x => x)));
                }
                else
                {
                    Console.WriteLine(string.Join("\n", doctors[args[0] + args[1]].OrderBy(x => x)));
                }
                command = Console.ReadLine();
            }
        }
    }
}
