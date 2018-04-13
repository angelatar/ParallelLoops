using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLoops
{
    class Program
    {
        static void Main(string[] args)
        {
            ParallelLoop.ParallelFor(1, 9, (i) => Console.WriteLine(i));

            Console.WriteLine();

            List<char> testList = new List<char>();
            string a = "abcdefghijklmnopqrstuvwxyz";
            foreach (var i in a)
                testList.Add((char)i);
            ParallelLoop.ParallelForEach<char>(testList, (i) => Console.WriteLine(i));

            Console.WriteLine();
            ParallelLoop.ParallelForEachWithOptions<char>(testList,new ParallelOptions() { MaxDegreeOfParallelism=5} ,(i) => Console.WriteLine(i));
            



        }
    }
}
