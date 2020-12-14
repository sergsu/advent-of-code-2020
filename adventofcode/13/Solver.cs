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
            //lines = "1;67,7,59,61".Split(";");
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
            Dictionary<int, int> sortedPeriods = periods
                .Select((period, index) => new { period, index })
                .Where(x => x.period > 0)
                .ToDictionary(x => x.period, x => periods.Count - x.index)
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);

            long prod = sortedPeriods.Aggregate((long)1, (acc, period) => acc * period.Key);
            long sum = 0;
            foreach (KeyValuePair<int, int> period in sortedPeriods)
            {
                long p = prod / period.Key;

                long modularMultiplicativeInverse = 1;
                long b = p % period.Key;
                for (long i = 1; i < period.Key; i++)
                {
                    if ((b * i) % period.Key == 1)
                    {
                        modularMultiplicativeInverse = i;
                        break;
                    }
                }

                sum += period.Value * modularMultiplicativeInverse * p;
            }

            Console.WriteLine(sum % prod - periods.Count);
        }
    }
}
