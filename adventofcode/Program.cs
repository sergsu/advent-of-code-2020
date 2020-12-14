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
            Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

            //Console.WriteLine("++++++++++ 1 ++++++++++");
            //adventofcode._1.Solver.Run();
            //Console.WriteLine("++++++++++ 2 ++++++++++");
            //adventofcode._2.Solver.Run();
            //Console.WriteLine("++++++++++ 3 ++++++++++");
            //adventofcode._3.Solver.Run();
            //Console.WriteLine("++++++++++ 4 ++++++++++");
            //adventofcode._4.Solver.Run();
            //Console.WriteLine("++++++++++ 5 ++++++++++");
            //adventofcode._5.Solver.Run();
            //Console.WriteLine("++++++++++ 6 ++++++++++");
            //adventofcode._6.Solver.Run();
            //Console.WriteLine("++++++++++ 7 ++++++++++");
            //adventofcode._7.Solver.Run();
            //Console.WriteLine("++++++++++ 8 ++++++++++");
            //adventofcode._8.Solver.Run();
            //Console.WriteLine("++++++++++ 9 ++++++++++");
            //adventofcode._9.Solver.Run();
            //Console.WriteLine("++++++++++ 10 ++++++++++");
            //adventofcode._10.Solver.Run();
            //Console.WriteLine("++++++++++ 11 ++++++++++");
            //adventofcode._11.Solver.Run();
            //Console.WriteLine("++++++++++ 12 ++++++++++");
            //adventofcode._12.Solver.Run();
            //Console.WriteLine("++++++++++ 13 ++++++++++");
            //adventofcode._13.Solver.Run();
            Console.WriteLine("++++++++++ 14 ++++++++++");
            adventofcode._14.Solver.Run();

            watch.Stop();
            Console.WriteLine("++++++++++ End ++++++++++");
            Console.WriteLine("Time spent: " + watch.ElapsedMilliseconds.ToString() + " ms");
        }
    }
}
