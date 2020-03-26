using System;
using System.Linq;

namespace P03_JediGalaxy
{
    public class Program
    {
        private static int[,] matrix;
        private static long sum;
        public static void Main()
        {
            int[] dimensions = Console.ReadLine()
                .Split(new string[] { " " },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int x = dimensions[0];
            int y = dimensions[1];

            InitializeField(x, y);

            sum = 0;
            string command = Console.ReadLine();
            
            while (command != "Let the Force be with you")
            {
                ProcessCoordinates(command);

                command = Console.ReadLine();
            }

            Console.WriteLine(sum);

        }

        private static void ProcessCoordinates(string command)
        {
            int[] ivoCoordinates = command
                .Split(new string[] { " " },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int[] evilCoordinates = Console.ReadLine()
                .Split(new string[] { " " },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            MoveEvilPlayer(evilCoordinates);
            MoveIvo(ivoCoordinates);
        }

        private static void MoveIvo(int[] ivoCoordinates)
        {
            int ivoRow = ivoCoordinates[0];
            int ivoCol = ivoCoordinates[1];

            while (ivoRow >= 0 && ivoCol < matrix.GetLength(1))
            {
                if (ivoRow >= 0
                    && ivoRow < matrix.GetLength(0)
                    && ivoCol >= 0
                    && ivoCol < matrix.GetLength(1))
                {
                    sum += matrix[ivoRow, ivoCol];
                }

                ivoCol++;
                ivoRow--;
            }
        }

        private static void MoveEvilPlayer(int[] evilCoordinates)
        {
            int evilRow = evilCoordinates[0];
            int evilCol = evilCoordinates[1];

            while (evilRow >= 0 && evilCol >= 0)
            {
                if (evilRow >= 0 && evilRow < matrix.GetLength(0)
                    && evilCol >= 0
                    && evilCol < matrix.GetLength(1))
                {
                    matrix[evilRow, evilCol] = 0;
                }
                evilRow--;
                evilCol--;
            }
        }

        private static void InitializeField(int x, int y)
        {
            matrix = new int[x, y];

            int currentNum = 0;
            for (int row = 0; row < x; row++)
            {
                for (int col = 0; col < y; col++)
                {
                    matrix[row, col] = currentNum;
                    currentNum++;
                }
            }
        }
    }
}
