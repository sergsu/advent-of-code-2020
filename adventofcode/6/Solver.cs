using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode._6
{
    class Solver
    {
        static public void Run()
        {
            string text = File.ReadAllText("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\6\\input.txt");
            string[] groups = text.Split("\n\n");

            // Part One
            int sum = 0;
            foreach (string group in groups)
            {
                sum += group.Replace("\n", string.Empty).Distinct().Count();
            }
            Console.WriteLine(sum);

            // Part Two
            Console.WriteLine("-----------------");
            sum = 0;
            foreach (string group in groups)
            {
                string[] people = group.Split("\n");
                IEnumerable<char> aggregate = people[0];
                foreach (string person in people)
                {
                    aggregate = aggregate.Intersect(person.ToList());
                }
                sum += aggregate.Count();
            }
            Console.WriteLine(sum);
        }
    }
}
