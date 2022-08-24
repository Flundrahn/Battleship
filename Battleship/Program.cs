using System;
using System.IO;

namespace Battleship
{
    public class Program
    {
        static void Main()
        {
            var sourceFile = new FileInfo($"{Directory.GetCurrentDirectory()}/sample.in");
            var sourceFileReader = new StreamReader(sourceFile.FullName);
            Console.SetIn(sourceFileReader);

            BattleShip game;

            int testCases = int.Parse(Console.ReadLine());

            for (var test = 0; test < testCases; test++)
            {
                string[] gameParameters;

                while ((gameParameters = ReadLineAndSplit()).Length != 3)
                {
                    continue;
                }

                game = new BattleShip(int.Parse(gameParameters[1]),
                                      int.Parse(gameParameters[2]));

                Console.WriteLine(game.Play());
            }
        }

        public static string[] ReadLineAndSplit()
        {
            return Console.ReadLine().Split(
                new char[] { ' ' },
                StringSplitOptions.None
            );
        }
    }
}
