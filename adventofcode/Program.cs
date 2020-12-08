using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Eight();
        }

        static void One()
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

        static void Two()
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

        static void Three()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\3\\input.txt");
            int steps = lines.Length;
            int patternLength = lines[0].Length;

            int[][] slopes = new int[][] { new int[] { 3, 1 }, new int[] { 1, 1 }, new int[] { 5, 1 }, new int[] { 7, 1 }, new int[] { 1, 2 } };
            foreach (int[] slope in slopes)
            {
                int slopeX = slope[0];
                int slopeY = slope[1];
                Console.WriteLine(slopeX.ToString() + "." + slopeY.ToString());
                int x = 1 + slopeX;
                int treesEncountered = 0;
                for (int y = 1 + slopeY; y <= steps; y += slopeY)
                {
                    int charPosition = (x - 1) % patternLength;

                    if (lines[y - 1][charPosition] == '#')
                    {
                        //Console.WriteLine((charPosition + 1).ToString() + "." + y.ToString());
                        treesEncountered++;
                    }

                    x += slopeX;
                }
                Console.WriteLine(treesEncountered);
            }
        }

        static void Four()
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

        static void Five()
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

        static void Six()
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

        static void Seven()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\7\\input.txt");
            Dictionary<string, Dictionary<string, int>> graph = new Dictionary<string, Dictionary<string, int>>();
            Regex reg = new Regex("^([a-z ]+) bags contain (((\\d+) ([a-z ]+) bags?(, )?)+|no other bags)\\.$");

            foreach (string line in lines)
            {
                Match tupleMatch = reg.Match(line);
                Debug.Assert(tupleMatch.Success, "Tuple regex should match");

                graph.Add(tupleMatch.Groups[1].Value, new Dictionary<string, int>());
                for (int i = 0; i < tupleMatch.Groups[5].Captures.Count; i++)
                {
                    graph[tupleMatch.Groups[1].Value].Add(tupleMatch.Groups[5].Captures[i].Value, int.Parse(tupleMatch.Groups[4].Captures[i].Value));
                }
            }

            // Part One
            Dictionary<string, string[]> reverseGraph = new Dictionary<string, string[]>();
            foreach (KeyValuePair<string, Dictionary<string, int>> tuple in graph)
            {
                foreach (string target in tuple.Value.Keys)
                {
                    if (!reverseGraph.ContainsKey(target))
                    {
                        reverseGraph.Add(target, new string[] { });
                    }
                    reverseGraph[target] = reverseGraph[target].Append(tuple.Key).ToArray();
                }
            }

            string[] keys = reverseGraph["shiny gold"];
            string[] newKeys = keys;
            while (newKeys.Length > 0)
            {
                string[] newNewKeys = newKeys;
                newKeys = new string[] { };
                foreach (string key in newNewKeys)
                {
                    if (!reverseGraph.ContainsKey(key))
                    {
                        continue;
                    }
                    newKeys = newKeys.Concat(reverseGraph[key]).Distinct().ToArray();
                }
                keys = keys.Concat(newKeys).Distinct().ToArray();
            }
            Console.WriteLine(keys.Length);

            // Part Two
            Console.WriteLine("-----------------");
            Func<Dictionary<string, int>, int> countNested = null;
            countNested = weighedKeys =>
            {
                int count = 0;
                foreach (KeyValuePair<string, int> weighedKey in weighedKeys)
                {
                    int nestedKeysCount = countNested(graph[weighedKey.Key]);
                    Debug.WriteLine(weighedKey.Key + ": " + nestedKeysCount + " * " + weighedKey.Value);
                    count += weighedKey.Value + nestedKeysCount * weighedKey.Value;
                }

                return count;
            };
            Console.WriteLine(countNested(graph["shiny gold"]));
        }

        static void Eight()
        {
            string[] commands = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\8\\input.txt");

            // Part One
            List<int> commandsRun = new List<int>();
            int line = 0;
            int accumulator = 0;
            while (line >= 0 && line < commands.Length && !commandsRun.Contains(line))
            {
                commandsRun.Add(line);
                string[] op = commands[line].Split(' ');
                switch (op[0])
                {
                    case "acc":
                        accumulator += int.Parse(op[1]);
                        line++;
                        break;
                    case "jmp":
                        line += int.Parse(op[1]);
                        break;
                    case "nop":
                        line++;
                        break;
                    default:
                        Debug.Fail("Nein!");
                        break;
                }
            }
            Console.WriteLine(accumulator);

            // Part Two
            Console.WriteLine("-----------------");
            List<string> commandsOpsRun = new List<string>();
            commandsRun = new List<int>();
            line = 0;
            List<int> accumulatorStates = new List<int>();
            int? branchPoint = null;
            while (line >= 0 && line < commands.Length)
            {
                commandsRun.Add(line);
                string[] op = commands[line].Split(' ');
                commandsOpsRun.Add(op[0]);
                accumulator = accumulatorStates.Count > 0 ? accumulatorStates.Last() : 0;
                switch (op[0])
                {
                    case "acc":
                        accumulator += int.Parse(op[1]);
                        line++;
                        break;
                    case "jmp":
                        line += int.Parse(op[1]);
                        // Look at branch point and look back at previous commands run. Attempt to change jmp to nop or nop to jmp.
                        if (commandsRun.Contains(line))
                        {
                            if (branchPoint == null)
                            {
                                branchPoint = commandsRun.Count - 1;
                            }

                            // Rolling back to branchPoint.
                            accumulatorStates.RemoveRange((int)branchPoint, accumulatorStates.Count - (int)branchPoint);
                            commandsRun.RemoveRange((int)branchPoint, commandsRun.Count - (int)branchPoint);
                            commandsOpsRun.RemoveRange((int)branchPoint, commandsOpsRun.Count - (int)branchPoint);

                            // One more step back.
                            accumulatorStates.RemoveAt(accumulatorStates.Count - 1);
                            commandsRun.RemoveAt(commandsRun.Count - 1);
                            commandsOpsRun.RemoveAt(commandsOpsRun.Count - 1);

                            // Find first no-acc command in the log.
                            while (commandsOpsRun.Last() == "acc")
                            {
                                accumulatorStates.RemoveAt(accumulatorStates.Count - 1);
                                commandsRun.RemoveAt(commandsRun.Count - 1);
                                commandsOpsRun.RemoveAt(commandsOpsRun.Count - 1);
                            }

                            string[] branchOp = commands[commandsRun.Last()].Split(' ');
                            string actualCommandRun = commandsOpsRun.Last();

                            // Now actually run the opposite action.
                            if (actualCommandRun == "jmp")
                            {
                                line = commandsRun.Last() + 1;
                            }
                            else if (actualCommandRun == "nop")
                            {
                                line = commandsRun.Last() + int.Parse(branchOp[1]);
                            }

                            // Record all the states again.
                            commandsOpsRun.RemoveAt(commandsOpsRun.Count - 1);
                            commandsOpsRun.Add(actualCommandRun == "jmp" ? "nop" : "jmp");
                            // Don't forget to update branching point!
                            branchPoint = commandsRun.Count - 1;

                            accumulator = accumulatorStates.Last();
                        }
                        break;
                    case "nop":
                        line++;
                        break;
                    default:
                        Debug.Fail("Nein!");
                        break;
                }
                accumulatorStates.Add(accumulator);
            }
            Console.WriteLine(accumulator);
        }
    }
}
