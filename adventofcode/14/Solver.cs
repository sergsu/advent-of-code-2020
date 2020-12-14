using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace adventofcode._14
{
    class Solver
    {
        static public void Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Sergey\\source\\repos\\adventofcode\\adventofcode\\14\\input.txt");
            //lines = "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X;mem[8] = 11;mem[7] = 101;mem[8] = 0".Split(";");
            //lines = "mask = 000000000000000000000000000000X1001X;mem[42] = 100;mask = 00000000000000000000000000000000X0XX;mem[26] = 1".Split(";");

            SolveOne(lines);
            Console.WriteLine("-----------------");
            SolveTwo(lines);
        }

        private static void SolveOne(string[] lines)
        {
            Dictionary<int, long> mem = new Dictionary<int, long>();
            string currentMask = "";
            foreach (string line in lines)
            {
                if (line.Substring(0, 7) == "mask = ")
                {
                    currentMask = line[7..];
                }
                else
                {
                    string[] memInstruction = line.Split("] = ");
                    mem[int.Parse(memInstruction[0][4..])] = ApplyMask(currentMask, long.Parse(memInstruction[1]));
                }
            }

            Console.WriteLine(mem.Values.Sum());
        }

        private static void SolveTwo(string[] lines)
        {
            Dictionary<long, long> mem = new Dictionary<long, long>();
            string currentMask = "";
            foreach (string line in lines)
            {
                if (line.Substring(0, 7) == "mask = ")
                {
                    currentMask = line[7..];
                }
                else
                {
                    string[] memInstruction = line.Split("] = ");
                    List<long> memAddresses = ApplyMemoryMask(currentMask, long.Parse(memInstruction[0][4..]));
                    foreach (long memAddress in memAddresses)
                    {
                        mem[memAddress] = long.Parse(memInstruction[1]);
                    }
                }
            }

            Console.WriteLine(mem.Values.Sum());
        }

        private static long ApplyMask(string mask, long number)
        {
            char[] numberBits = Convert.ToString(number, 2).ToCharArray();
            numberBits = new string(numberBits).PadLeft(36, '0').ToCharArray();
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                {
                    continue;
                }
                else
                {
                    numberBits[i] = mask[i];
                }
            }

            return Convert.ToInt64(new string(numberBits), 2);
        }

        private static List<long> ApplyMemoryMask(string mask, long number)
        {
            char[] numberBits = Convert.ToString(number, 2).ToCharArray();
            numberBits = new string(numberBits).PadLeft(36, '0').ToCharArray();
            List<int> floatingPositions = new List<int>();
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                {
                    floatingPositions.Add(i);
                }
                else if (mask[i] == '1')
                {
                    numberBits[i] = mask[i];
                }
            }

            List<long> memAddresses = new List<long>();
            IEnumerable<int> combinations = Enumerable.Range(0, (int)BigInteger.Pow(2, floatingPositions.Count));
            foreach (int combination in combinations)
            {
                char[] combinationBits = Convert.ToString(combination, 2).PadLeft(floatingPositions.Count, '0').ToCharArray();
                char[] numberBitsCopy = numberBits.ToArray();
                for (int i = 0; i < floatingPositions.Count; i++)
                {
                    numberBitsCopy[floatingPositions[i]] = combinationBits[i];
                }
                memAddresses.Add(Convert.ToInt64(new string(numberBitsCopy), 2));
            }

            return memAddresses;
        }
    }
}
