using System;
using System.IO;
using System.Linq;

namespace adventofcode._12
{
    class Solver
    {
        static public void Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\12\\input.txt");
            //string[] lines = "F10,N3,F7,R90,F11".Split(",");

            Solve(lines);
        }

        private static void Solve(string[] lines)
        {
            int[] currentPosition = new int[2] { 0, 0 };
            int[] currentRelativePosition = new int[2] { 10, 1 };
            int[] shipPosition = new int[2] { 0, 0 };
            int currentAngle = 0;
            foreach (string line in lines)
            {
                char command = line[0];
                char direction = '?';

                // Part One
                switch (command)
                {
                    case 'E':
                    case 'N':
                    case 'W':
                    case 'S':
                        direction = command;
                        break;
                    case 'L':
                        currentAngle = (360 + currentAngle + int.Parse(line[1..])) % 360;
                        break;
                    case 'R':
                        currentAngle = (360 + currentAngle - int.Parse(line[1..])) % 360;
                        break;
                    case 'F':
                        switch (currentAngle)
                        {
                            case 0:
                                direction = 'E';
                                break;
                            case 90:
                                direction = 'N';
                                break;
                            case 180:
                                direction = 'W';
                                break;
                            case 270:
                                direction = 'S';
                                break;
                        }
                        break;
                }

                switch (direction)
                {
                    case 'E':
                        currentPosition[0] += int.Parse(line[1..]);
                        break;
                    case 'N':
                        currentPosition[1] += int.Parse(line[1..]);
                        break;
                    case 'W':
                        currentPosition[0] -= int.Parse(line[1..]);
                        break;
                    case 'S':
                        currentPosition[1] -= int.Parse(line[1..]);
                        break;
                }

                // Part Two
                switch (command)
                {
                    case 'E':
                        currentRelativePosition[0] += int.Parse(line[1..]);
                        break;
                    case 'N':
                        currentRelativePosition[1] += int.Parse(line[1..]);
                        break;
                    case 'W':
                        currentRelativePosition[0] -= int.Parse(line[1..]);
                        break;
                    case 'S':
                        currentRelativePosition[1] -= int.Parse(line[1..]);
                        break;
                    case 'L':
                    case 'R':
                        int rotationAngle = (360 + int.Parse(line[1..]) * (command == 'L' ? 1 : -1)) % 360;
                        int[] newCurrentRelativePosition = currentRelativePosition.ToArray();
                        switch (rotationAngle)
                        {
                            case 90:
                                newCurrentRelativePosition[0] = -currentRelativePosition[1];
                                newCurrentRelativePosition[1] = currentRelativePosition[0];
                                break;
                            case 180:
                                newCurrentRelativePosition[0] = -currentRelativePosition[0];
                                newCurrentRelativePosition[1] = -currentRelativePosition[1];
                                break;
                            case 270:
                                newCurrentRelativePosition[0] = currentRelativePosition[1];
                                newCurrentRelativePosition[1] = -currentRelativePosition[0];
                                break;
                        }
                        currentRelativePosition = newCurrentRelativePosition;
                        break;
                    case 'F':
                        shipPosition[0] += currentRelativePosition[0] * int.Parse(line[1..]);
                        shipPosition[1] += currentRelativePosition[1] * int.Parse(line[1..]);
                        break;
                }
            }

            Console.WriteLine(Math.Abs(currentPosition[0]) + Math.Abs(currentPosition[1]));
            Console.WriteLine("-----------------");
            Console.WriteLine(Math.Abs(shipPosition[0]) + Math.Abs(shipPosition[1]));
        }
    }
}
