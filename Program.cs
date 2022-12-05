using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace MyProject;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var fileName = "day011test.txt";
        Console.WriteLine(Calories(fileName));


    }



    public static Double Calories(string fileName)
    {
        string path = Path.Combine(Environment.CurrentDirectory, @"sources\", fileName);

        List<string> lines = File.ReadAllLines(path).ToList();
        List<Double> results = new List<Double>();

        Double actual = 0;

        foreach (var line in lines)
        {
            if (Double.TryParse(line, out Double number))
            {
                if (number == 67997) {
                    Console.WriteLine(@"tady je to nejvyssi samostatne @number");
                }
                actual += number;
            } else {
                if (actual == 67997)
                {
                    Console.WriteLine(@"tady pridavam nejvyssi samostatne @actual");
                }
                results.Add(actual);
                actual = 0;
            }
        }

        results.Sort();

        return results[results.Count-1] + results[results.Count-2] + results[results.Count-3];
    }
}