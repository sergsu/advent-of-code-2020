using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;

namespace adventofcode._11
{
    class Solver
    {
        static public void Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\11\\input.txt");
            //string[] lines = "L.LL.LL.LL,LLLLLLL.LL,L.L.L..L..,LLLL.LL.LL,L.LL.LL.LL,L.LLLLL.LL,..L.L.....,LLLLLLLLLL,L.LLLLLL.L,L.LLLLL.LL".Split(",");

            bool?[][] slots = lines.ToList().ConvertAll<bool?[]>(line => line.ToCharArray().ToList().ConvertAll<bool?>(letter =>
            {
                if (letter == 'L')
                {
                    return false;
                }
                else
                {
                    return null;
                }
            }).ToArray()).ToArray();

            Solve(slots.Select(a => a.ToArray()).ToArray(), 1, CountOccupiedAdjacentSlots, 4);
            Console.WriteLine("-----------------");
            Solve(slots.Select(a => a.ToArray()).ToArray(), 3, CountOccupiedDiagonalSlots, 5);
        }

        private static void Solve(bool?[][] slots, int consolePos, Func<bool?[][], int, int, int> countOccupiedSlots, int occupancyTolerance)
        {
            int changedSlots = 0;
            int iterations = 0;
            bool?[][] slotsShadow = slots.Select(a => a.ToArray()).ToArray();
            do
            {
                bool?[][] slotsCopy = slotsShadow.Select(a => a.ToArray()).ToArray();
                changedSlots = 0;
                for (int i = 0; i < slotsCopy.Length; i++)
                {
                    for (int j = 0; j < slotsCopy[i].Length; j++)
                    {
                        bool? isOccupied = slotsCopy[i][j];
                        // Ignore blocked slots.
                        if (isOccupied == null)
                        {
                            continue;
                        }

                        int occupiedAround = countOccupiedSlots(slotsCopy, i, j);

                        // If slot is empty and there are no known occupied slots - occupy.
                        if (isOccupied == false && occupiedAround == 0)
                        {
                            slotsShadow[i][j] = true;
                            changedSlots++;
                        }
                        // If slot is occupied and there are X+ known occupied slots - empty.
                        if (isOccupied == true && occupiedAround >= occupancyTolerance)
                        {
                            slotsShadow[i][j] = false;
                            changedSlots++;
                        }
                    }
                }
                iterations++;
            } while (changedSlots != 0 && iterations < 50000);

            int occupiedSlots = 0;
            for (int i = 0; i < slotsShadow.Length; i++)
            {
                for (int j = 0; j < slotsShadow[i].Length; j++)
                {
                    if (slotsShadow[i][j] == true)
                    {
                        occupiedSlots++;
                    }
                }
            }
            Console.WriteLine("Occupied slots: " + occupiedSlots);
        }

        private static int CountOccupiedAdjacentSlots(bool?[][] slots, int slotI, int slotJ)
        {
            int count = 0;
            for (int i = slotI - 1; i <= slotI + 1; i++)
            {
                for (int j = slotJ - 1; j <= slotJ + 1; j++)
                {
                    if (!(i == slotI && j == slotJ) && i >= 0 && i < slots.Length && j >= 0 && j < slots[i].Length)
                    {
                        if (slots[i][j] == true)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private static int CountOccupiedDiagonalSlots(bool?[][] slots, int slotI, int slotJ)
        {
            int count = 0;
            int max = slots.Length;
            List<int[]> directions = new List<int[]>
                {
                    { new int[] { -1, -1 } },
                    { new int[] { -1, 0 } },
                    { new int[] { -1, 1 } },
                    { new int[] { 0, -1 } },
                    { new int[] { 0, 1 } },
                    { new int[] { 1, -1 } },
                    { new int[] { 1, 0 } },
                    { new int[] { 1, 1 } },
                };
            foreach (int[] direction in directions)
            {
                for (int i = 1; i <= max; i++)
                {
                    int x = slotI + i * direction[0];
                    int y = slotJ + i * direction[1];
                    if (x < 0 || y < 0 || slots.Length <= x || slots[x].Length <= y)
                    {
                        continue;
                    }
                    if (slots[x][y] == true)
                    {
                        count++;
                        break;
                    }
                    if (slots[x][y] == false)
                    {
                        break;
                    }
                }
            }

            return count;
        }
    }
}
