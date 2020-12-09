using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace adventofcode._9
{
    class Solver
    {
        static Dictionary<int, bool> cache = new Dictionary<int, bool>();

        static public void Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\9\\input.txt");
            List<long> numbers = lines.ToList().ConvertAll<long>(line => long.Parse(line));
            long weakness = 0;

            // Part One
            for (int i = 25; i < numbers.Count; i++)
            {
                if (!checkNumber(numbers, i, 25))
                {
                    weakness = numbers[i];
                    Console.WriteLine(weakness);
                    break;
                }
            }

            // Part Two
            Console.WriteLine("-----------------");
            for (int i = 0; i < numbers.Count; i++)
            {
                long sum = numbers[i];
                int j = i;
                while (sum < weakness && j < numbers.Count)
                {
                    j++;
                    sum += numbers[j];
                }

                if (sum == weakness && i != j)
                {
                    List<long> range = numbers.GetRange(i, j - i + 1);
                    Console.WriteLine(i);
                    Console.WriteLine(j);
                    Console.WriteLine(numbers[i]);
                    Console.WriteLine(numbers[j]);
                    Console.WriteLine(range.Min() + range.Max());
                }
            }
        }

        static bool checkNumber(List<long> numbers, int index, int preambleSize)
        {
            if (cache.ContainsKey(index))
            {
                return cache[index];
            }

            long number = numbers[index];
            List<long> preamble = numbers.GetRange(index - preambleSize, preambleSize);

            foreach (long minuend in preamble)
            {
                long difference = number - minuend;
                if (preamble.Contains(difference))
                {
                    cache.Add(index, true);
                    return true;
                }
            }

            cache.Add(index, false);
            return false;
        }
    }
}
