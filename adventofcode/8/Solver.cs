using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace adventofcode._8
{
    class Solver
    {
        static public void Run()
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
