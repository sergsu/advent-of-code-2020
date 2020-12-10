using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;

namespace adventofcode._10
{
    class Solver
    {
        static Dictionary<int, bool> cache = new Dictionary<int, bool>();

        static public void Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\10\\input.txt");

            List<int> numbers = lines.ToList().ConvertAll<int>(line => int.Parse(line));

            PartOne(numbers);
            Console.WriteLine("-----------------");
            Console.WriteLine("2:" + PartTwo("1,2".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("4:" + PartTwo("1,2,3".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("7:" + PartTwo("1,2,3,4".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("3:" + PartTwo("1,3,4".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("2:" + PartTwo("1,3,5".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("2:" + PartTwo("1,3,5,7".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("4:" + PartTwo("1,3,5,6".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("13:" + PartTwo("1,2,3,4,5".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("24:" + PartTwo("1,2,3,4,5,6".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("14:" + PartTwo("1,2,3,4,7,8,9".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("8:" + PartTwo("16,10,15,5,1,11,7,19,6,12,4".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("19208:" + PartTwo("28,33,18,42,31,14,46,20,48,47,24,23,49,45,19,38,39,11,1,32,25,35,8,17,7,9,4,2,34,10,3".Split(",").ToList().ConvertAll<int>(line => int.Parse(line))));
            Console.WriteLine("-----------------");
            Console.WriteLine(PartTwo(numbers));
        }

        private static void PartOne(List<int> numbers)
        {
            numbers.Sort();
            int[] diffCounts = new int[4] { 0, 0, 0, 1 };
            diffCounts[numbers[0]]++;

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                int diff = numbers[i + 1] - numbers[i];
                if (diffCounts.Length < diff)
                {
                    for (int j = diffCounts.Length; j <= diff; j++)
                    {
                        diffCounts = diffCounts.Append(0).ToArray();
                    }
                }
                diffCounts[diff]++;
            }

            Console.WriteLine(diffCounts[1] * diffCounts[3]);
        }

        private static void Merge(Dictionary<int, long> lastNumberCounts, int lastNumber, long count)
        {
            if (lastNumberCounts.ContainsKey(lastNumber))
            {
                lastNumberCounts[lastNumber] += count;
            }
            else
            {
                lastNumberCounts.Add(lastNumber, count);
            }
        }

        private static long PartTwo(List<int> numbers)
        {
            numbers.Add(0);
            numbers.Sort();

            Dictionary<int, long> lastNumberCounts = new Dictionary<int, long> { };
            lastNumberCounts.Add(numbers[0], 1);

            for (int j = 0; j < lastNumberCounts.Count; j++)
            {
                KeyValuePair<int, long> lastNumberCount = lastNumberCounts.ElementAt(j);
                int i = numbers.IndexOf(lastNumberCount.Key);
                if (numbers.Count > i + 1 && numbers[i + 1] - numbers[i] <= 3)
                {
                    Merge(lastNumberCounts, numbers[i + 1], lastNumberCount.Value);
                }
                if (numbers.Count > i + 2 && numbers[i + 2] - numbers[i] <= 3)
                {
                    Merge(lastNumberCounts, numbers[i + 2], lastNumberCount.Value);
                }
                if (numbers.Count > i + 3 && numbers[i + 3] - numbers[i] <= 3)
                {
                    Merge(lastNumberCounts, numbers[i + 3], lastNumberCount.Value);
                }
            }

            return lastNumberCounts[lastNumberCounts.Keys.Max()];
        }
    }
}
