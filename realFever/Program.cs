using System;
using System.Collections.Generic;
using Microsoft.SolverFoundation.Services;
using realFever.Solvers;

namespace realFever
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1: Run model (Google)");
            Console.WriteLine("2: Run model (Google) and refresh data");
            Console.WriteLine("3: Print all data to excel");
            int result;
            if (int.TryParse(Console.ReadLine(), out result))
            {
                if (result == 1)
                    new GoogleSolver();
                else if (result == 2)
                    new GoogleSolver(true);
                else if (result == 3)
                    PlayerList.printTestData();
                else
                    Console.WriteLine("Nop");
            }

        }
    }
}
