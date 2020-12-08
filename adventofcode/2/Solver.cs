using System;
using System.IO;
using System.Text.RegularExpressions;

namespace adventofcode._2
{
    class Solver
    {
        static public void Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\2\\input.txt");
            int validPasswords = 0;
            Regex reg = new Regex("^(\\d+)\\-(\\d+) ([a-z]): ([a-z]+)$");

            Console.WriteLine(lines.Length);
            foreach (string line in lines)
            {
                Match matches = reg.Match(line);
                //int min = int.Parse(matches.Groups[1].Value);
                //int max = int.Parse(matches.Groups[2].Value);
                //string letter = matches.Groups[3].Value;
                //string password = matches.Groups[4].Value;
                //int count = Regex.Matches(password, letter).Count;

                //if (min <= count && count <= max)
                //{
                //    validPasswords++;
                //}
                int left = int.Parse(matches.Groups[1].Value) - 1;
                int right = int.Parse(matches.Groups[2].Value) - 1;
                string letter = matches.Groups[3].Value;
                string password = matches.Groups[4].Value;
                int count = (password[left] == letter[0] ? 1 : 0) + (password[right] == letter[0] ? 1 : 0);

                if (count == 1)
                {
                    validPasswords++;
                }
            }
            Console.WriteLine(validPasswords);
        }

    }
}
