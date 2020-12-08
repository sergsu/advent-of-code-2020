using System;
using System.IO;

namespace adventofcode._3
{
    class Solver
    {
        static public void Run()
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
    }
}
