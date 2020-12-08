using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode._5
{
    class Solver
    {
        static public void Run()
        {
            string text = File.ReadAllText("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\5\\input.txt");
            text = text.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');
            string[] lines = text.Split("\n");
            List<int> numbers = lines.ToList().ConvertAll<int>(line => Convert.ToInt32(line, 2));
            numbers.Sort();
            Console.WriteLine(numbers.Last());

            // Part Two
            IEnumerable<int> range = Enumerable.Range(numbers.First(), numbers.Last() - numbers.First());
            IEnumerable<int> missingNumbers = range.Except(numbers);
            Console.WriteLine(string.Join(",", missingNumbers));
        }
    }
}
