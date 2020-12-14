using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace adventofcode._13
{
    class Solver
    {
        static public void Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\13\\input.txt");
            //lines = "939;7,13,x,x,59,x,31,19".Split(";");
            //lines = "1;17,x,13,19".Split(";");
            //lines = "1;67,x,7,59,61".Split(";");
            //lines = "1;67,7,x,59,61".Split(";");
            //lines = "1;1789,37,47,1889".Split(";");

            int time = int.Parse(lines[0]);
            List<int> periods = lines[1].Split(',').ToList().ConvertAll<int>(line => line == "x" ? 0 : int.Parse(line));
            SolveOne(time, periods);
            Console.WriteLine("-----------------");
            SolveTwo(time, periods);
        }

        private static void SolveOne(int time, List<int> periods)
        {
            Dictionary<int, int> periodWaitTimes = new Dictionary<int, int>();
            foreach (int period in periods.Where(x => x > 0).ToList())
            {
                periodWaitTimes[period] = (period - (time % period)) % period;
            }

            KeyValuePair<int, int> min = periodWaitTimes.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value).First();
            Console.WriteLine(min.Key * min.Value);
        }

        private static void SolveTwo(long time, List<int> periods)
        {
            long max = periods.Max();
            int maxPosition = periods.IndexOf((int)max);
            bool found = false;

            for (long timeToCheck = time + ((max - (time % max)) % max) - maxPosition; timeToCheck < long.MaxValue; timeToCheck += max * 1000000 * 24)
            {
                Console.SetCursorPosition(0, 3);
                Console.WriteLine(timeToCheck);
                List<long> timesToCheck = Enumerable.Range(0, 24).Select(x => timeToCheck + x * max * 1000000).ToList();

                Parallel.ForEach(timesToCheck, timeToCheck =>
                {
                    for (long innerTimeToCheck = timeToCheck; innerTimeToCheck < timeToCheck + max * 1000000; innerTimeToCheck += max)
                    {
                        bool valid = true;
                        for (int i = 0; i < periods.Count; i++)
                        {
                            if (periods[i] == 0)
                            {
                                continue;
                            }
                            if ((innerTimeToCheck + i) % periods[i] > 0)
                            {
                                valid = false;
                                break;
                            }
                        }

                        if (valid)
                        {
                            Console.WriteLine(innerTimeToCheck);
                            found = true;
                            break;
                        }
                    }
                });
                if (found)
                {
                    break;
                }
            }
        }
    }
}
