using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode._1
{
    class Solver
    {
        static public void Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\1\\input.txt");
            List<int> numbers = lines.ToList().ConvertAll<int>(line => int.Parse(line));

            int iters = 0;
            Console.WriteLine(numbers.Count);
            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    for (int k = j + 1; k < numbers.Count; k++)
                    {
                        iters++;
                        int sum = numbers[i] + numbers[j] + numbers[k];
                        if (sum == 2020)
                        {
                            int multiplication = numbers[i] * numbers[j] * numbers[k];
                            Console.WriteLine(numbers[i]);
                            Console.WriteLine(numbers[j]);
                            Console.WriteLine(numbers[k]);
                            Console.WriteLine(multiplication);
                            Console.WriteLine(sum);
                            Console.WriteLine(iters);
                            return;
                        }
                    }
                }
            }
        }
    }
}
