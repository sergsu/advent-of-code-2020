using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace adventofcode._4
{
    class Solver
    {
        static public void Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\4\\input.txt");
            Regex reg = new Regex("^(([a-z]{3})\\:(\\S+) ?)+$");
            Regex hgtReg = new Regex("^(\\d+)(cm|in)$");
            Regex hclReg = new Regex("^\\#[0-9a-f]{6}$");
            List<string> eclList = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            Regex pidReg = new Regex("^[0-9]{9}$");

            List<string> validFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            int currentValidFields = 0;
            int validPassports = 0;

            foreach (string line in lines)
            {
                Match match = reg.Match(line);
                if (match.Length == 0)
                {
                    if (currentValidFields == validFields.Count)
                    {
                        validPassports++;
                    }
                    currentValidFields = 0;
                }
                else
                {
                    for (int i = 0; i < match.Groups[2].Captures.Count; i++)
                    {
                        bool valid = false;
                        if (validFields.Contains(match.Groups[2].Captures[i].Value))
                        {
                            bool isNum = int.TryParse(match.Groups[3].Captures[i].Value, out int numField);
                            switch (match.Groups[2].Captures[i].Value)
                            {
                                case "byr":
                                    valid = isNum && numField >= 1920 && numField <= 2002;
                                    break;
                                case "iyr":
                                    valid = isNum && numField >= 2010 && numField <= 2020;
                                    break;
                                case "eyr":
                                    valid = isNum && numField >= 2020 && numField <= 2030;
                                    break;
                                case "hgt":
                                    Match m = hgtReg.Match(match.Groups[3].Captures[i].Value);
                                    if (m.Success)
                                    {
                                        numField = int.Parse(m.Groups[1].Value);
                                        valid = (m.Groups[2].Value == "cm" && numField >= 150 && numField <= 193) || (m.Groups[2].Value == "in" && numField >= 59 && numField <= 76);
                                    }
                                    break;
                                case "hcl":
                                    valid = hclReg.Match(match.Groups[3].Captures[i].Value).Success;
                                    break;
                                case "ecl":
                                    valid = eclList.Contains(match.Groups[3].Captures[i].Value);
                                    break;
                                case "pid":
                                    valid = pidReg.Match(match.Groups[3].Captures[i].Value).Success;
                                    break;
                            }
                        }
                        if (valid)
                        {
                            currentValidFields++;
                        }
                    }
                }
            }
            if (currentValidFields == validFields.Count)
            {
                validPassports++;
            }
            Console.WriteLine(validPassports);
        }
    }
}
