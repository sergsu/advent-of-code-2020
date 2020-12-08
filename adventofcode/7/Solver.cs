using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode._7
{
    class Solver
    {
        static public void Run()
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
    }
}
