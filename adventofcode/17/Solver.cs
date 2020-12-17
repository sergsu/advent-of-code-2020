using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace adventofcode._17
{
    class Solver
    {
        const int CYCLES = 6;

        static public void Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\17\\input.txt");

            Console.WriteLine("112:" + Solve4D(Generate4D(".#.;..#;###".Split(';'), true)));
            Console.WriteLine("-----------------");
            Console.WriteLine(Solve4D(Generate4D(lines, true)));
            Console.WriteLine("848:" + Solve4D(Generate4D(".#.;..#;###".Split(';'))));
            Console.WriteLine("-----------------");
            Console.WriteLine(Solve4D(Generate4D(lines)));
        }

        private static bool[,,,] Generate4D(string[] lines, bool is3D = false)
        {
            bool[,,,] init = new bool[is3D ? 1 : 1 + CYCLES * 2, 1 + CYCLES * 2, lines.Length + CYCLES * 2, lines[0].Length + CYCLES * 2];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    init[is3D ? 0 : CYCLES, CYCLES, CYCLES + i , CYCLES + j] = lines[i][j] == '#';
                }
            }

            return init;
        }

        private static int Solve4D(bool[,,,] space)
        {
            int iterations = 0;
            bool[,,,] spaceShadow = (bool[,,,])space.Clone();
            do
            {
                bool[,,,] spaceCopy = (bool[,,,])spaceShadow.Clone();
                for (int f = 0; f <= spaceCopy.GetUpperBound(0); f++)
                {
                    for (int z = 0; z <= spaceCopy.GetUpperBound(1); z++)
                    {
                        for (int x = 0; x <= spaceCopy.GetUpperBound(2); x++)
                        {
                            for (int y = 0; y <= spaceCopy.GetUpperBound(3); y++)
                            {
                                bool isOccupied = spaceCopy[f, z, x, y];
                                int occupiedAround = CountOccupiedAdjacentSlots(spaceCopy, x, y, z, f);

                                if (isOccupied == false)
                                {
                                    spaceShadow[f, z, x, y] = occupiedAround == 3;
                                }
                                if (isOccupied == true)
                                {
                                    spaceShadow[f, z, x, y] = occupiedAround >= 2 && occupiedAround <= 3;
                                }
                            }
                        }
                    }
                }
                iterations++;

                // Debug
                //for (int z = 0; z <= spaceShadow.GetUpperBound(0); z++)
                //{
                //    Console.WriteLine("\n----");
                //    Console.WriteLine(z);
                //    Console.Write("----");
                //    for (int x = 0; x <= spaceShadow.GetUpperBound(1); x++)
                //    {
                //        Console.Write("\n");
                //        for (int y = 0; y <= spaceShadow.GetUpperBound(2); y++)
                //        {
                //            if (spaceShadow[z, x, y] == true)
                //            {
                //                Console.Write('#');
                //            }
                //            else
                //            {
                //                Console.Write('.');
                //            }
                //        }
                //    }
                //}
            } while (iterations < CYCLES);

            int occupiedSlots = 0;
            for (int f = 0; f <= spaceShadow.GetUpperBound(0); f++)
            {
                for (int z = 0; z <= spaceShadow.GetUpperBound(1); z++)
                {
                    for (int x = 0; x <= spaceShadow.GetUpperBound(2); x++)
                    {
                        for (int y = 0; y <= spaceShadow.GetUpperBound(3); y++)
                        {
                            if (spaceShadow[f, z, x, y] == true)
                            {
                                occupiedSlots++;
                            }
                        }
                    }
                }
            }

            return occupiedSlots;
        }

        private static int CountOccupiedAdjacentSlots(bool[,,,] space, int slotX, int slotY, int slotZ, int slotF)
        {
            int count = 0;
            for (int f = slotF - 1; f <= slotF + 1; f++)
            {
                for (int z = slotZ - 1; z <= slotZ + 1; z++)
                {
                    for (int x = slotX - 1; x <= slotX + 1; x++)
                    {
                        for (int y = slotY - 1; y <= slotY + 1; y++)
                        {
                            if (
                                !(x == slotX && y == slotY && z == slotZ && f == slotF)
                                && f >= 0 && f <= space.GetUpperBound(0)
                                && z >= 0 && z <= space.GetUpperBound(1)
                                && x >= 0 && x <= space.GetUpperBound(2)
                                && y >= 0 && y <= space.GetUpperBound(3)
                            )
                            {
                                if (space[f, z, x, y] == true)
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            return count;
        }
    }
}
