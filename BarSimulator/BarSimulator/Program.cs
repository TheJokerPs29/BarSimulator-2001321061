using System;
using System.Collections.Generic;
using System.Threading;

namespace BarSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Drink vodka = new Drink("Vodka", 45,20);
            Drink whiskey = new Drink("Whiskey", 70,20);


            Bar bar = new Bar();
            bar.SetMenu(new List<Drink>() { vodka, whiskey });
            List<Thread> studentThreads = new List<Thread>();
            for (int i = 1; i < 100; i++)
            {
                var student = new Student(i.ToString(), bar);
                var thread = new Thread(student.PaintTheTownRed);
                thread.Start();
                studentThreads.Add(thread);
            }

            foreach (var t in studentThreads) t.Join();
            Console.WriteLine();
            Console.WriteLine("The party is over.");
            Console.ReadLine();
        }
    }
}
