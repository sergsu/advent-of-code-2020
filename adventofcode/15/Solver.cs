using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace adventofcode._15
{
    class Solver
    {
        static public void Run()
        {
            List<int> input = File.ReadAllText("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\15\\input.txt").Split(',').ToList().ConvertAll<int>(line => int.Parse(line));

            Console.WriteLine("1:" + Solve("1,3,2".Split(',').ToList().ConvertAll<int>(line => int.Parse(line)), 2020));
            Console.WriteLine("10:" + Solve("2,1,3".Split(',').ToList().ConvertAll<int>(line => int.Parse(line)), 2020));
            Console.WriteLine("27:" + Solve("1,2,3".Split(',').ToList().ConvertAll<int>(line => int.Parse(line)), 2020));
            Console.WriteLine("78:" + Solve("2,3,1".Split(',').ToList().ConvertAll<int>(line => int.Parse(line)), 2020));
            Console.WriteLine("436:" + Solve("0,3,6".Split(',').ToList().ConvertAll<int>(line => int.Parse(line)), 2020));
            Console.WriteLine("438:" + Solve("3,2,1".Split(',').ToList().ConvertAll<int>(line => int.Parse(line)), 2020));
            Console.WriteLine("1836:" + Solve("3,1,2".Split(',').ToList().ConvertAll<int>(line => int.Parse(line)), 2020));
            Console.WriteLine("-----------------");
            Console.WriteLine(Solve(input, 2020));
            Console.WriteLine("-----------------");
            Console.WriteLine("175594:" + Solve("0,3,6".Split(',').ToList().ConvertAll<int>(line => int.Parse(line)), 30000000));
            Console.WriteLine("-----------------");
            Console.WriteLine(Solve(input, 30000000));
        }

        private static int Solve(List<int> input, int iterations)
        {
            int i = 0;
            int lastNumberSpoken = input.Last();
            Dictionary<int, int[]> spokenNumbers = input.ToDictionary(x => x, x => new int[2] { -1, i++ });

            for (i = input.Count; i < iterations; i++)
            {
                if (!spokenNumbers.ContainsKey(lastNumberSpoken) || spokenNumbers[lastNumberSpoken][0] == -1)
                {
                    lastNumberSpoken = 0;
                }
                else
                {
                    lastNumberSpoken = i - spokenNumbers[lastNumberSpoken][0] - 1;
                }
                if (!spokenNumbers.ContainsKey(lastNumberSpoken))
                {
                    spokenNumbers[lastNumberSpoken] = new int[2] { -1, -1 };
                }
                spokenNumbers[lastNumberSpoken][0] = spokenNumbers[lastNumberSpoken][1];
                spokenNumbers[lastNumberSpoken][1] = i;
            }

            return lastNumberSpoken;
        }
    }
}
