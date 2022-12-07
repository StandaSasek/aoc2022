using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using System.Text;

namespace MyProject;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //Console.WriteLine(Calories1("day01.txt"));
        //Console.WriteLine(Calories2("day01.txt"));
        //Console.WriteLine(Scissors("day02.txt"));
        //Console.WriteLine(Scissors2("day02.txt"));
        //Console.WriteLine(Rucksack("day03.txt"));
        //Console.WriteLine(Rucksack2("day03.txt"));
        //Console.WriteLine(Cleaning("day04.txt"));
        Console.WriteLine(Cleaning2("day04.txt"));


    }

    public static int Cleaning2(string fileName)
    {
        List<string> lines = ReadLines(fileName);
        int result = 0;

        foreach (var line in lines)
        {
            List<string> zones = line.Split(',').ToList();

            List<string> numbers1 = zones[0].Split('-').ToList();
            int oneFrom = Int16.Parse(numbers1[0]);
            int oneTo = Int16.Parse(numbers1[1]);

            List<string> numbers2 = zones[1].Split('-').ToList();
            int twoFrom = Int16.Parse(numbers2[0]);
            int twoTo = Int16.Parse(numbers2[1]);

            if (oneFrom <= twoFrom
                && twoFrom <= oneTo
                || oneFrom <= twoTo
                && twoTo <= oneTo
                || twoFrom <= oneFrom
                && twoTo <= oneTo
                || twoFrom <= twoTo
                && twoTo <= oneTo)
            {
                result++;
            }
        }

        return result;
    }

    public static int Cleaning(string fileName)
    {
        List<string> lines = ReadLines(fileName);
        int result = 0;

        foreach (var line in lines)
        {
            List<string> zones = line.Split(',').ToList();

            List<string> numbers1 = zones[0].Split('-').ToList();
            int oneFrom = Int16.Parse(numbers1[0]);
            int oneTo = Int16.Parse(numbers1[1]);

            List<string> numbers2 = zones[1].Split('-').ToList();
            int twoFrom = Int16.Parse(numbers2[0]);
            int twoTo = Int16.Parse(numbers2[1]);

            if (oneFrom <= twoFrom 
                && oneTo >= twoTo
                || twoFrom <= oneFrom
                && twoTo >= oneTo)
            {
                result++;
            }
        }

        return result;
    }

    public static int Rucksack2(string fileName)
    {
        List<string> lines = ReadLines(fileName);
        int result = 0;
        List<int> badges = new List<int>();

        for (int i = 0; i < lines.Count; i += 3)
        {
            List<int> elfOne = getLetters(lines[i]);
            List<int> elfTwo = getLetters(lines[i + 1]);
            List<int> elfThree = getLetters(lines[i + 2]);
            List<int> commonNumbers = new List<int>();

            foreach (var number in elfOne)
            {
                if (elfTwo.Contains(number))
                {
                    if (elfThree.Contains(number))
                    {
                        commonNumbers.Add(number);
                    }
                }
            }

            result += commonNumbers.Distinct().Sum();
        }

        return result;
    }
        
    public static int Rucksack(string fileName)
    {
        List<string> lines = ReadLines(fileName);
        int result = 0;

        foreach (var item in lines)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(item);
            List<int> letters = getAscii(asciiBytes);

            foreach (var letter in asciiBytes)
            {
                if (letter > 96)
                {
                    letters.Add(letter - 96);
                }
                else
                {
                    letters.Add(letter - 64 + 26);
                }
            }

            int size = letters.Count / 2;
            List<int> leftComparnment = letters.GetRange(0, size);
            List<int> rightComparnment = letters.GetRange(size, size);
            List<int> commonNumbers = new List<int>();

            foreach (var number in leftComparnment)
            {
                if (rightComparnment.Contains(number))
                {
                    commonNumbers.Add(number);
                }
            }

            result += commonNumbers.Distinct().Sum();
        }

        return result;
    }

    public static int Scissors2(string fileName)
    {
        List<string> lines = ReadLines(fileName);

        int result = 0;
        // A = Rock
        // B = Paper
        // C = Scissors
        // X = Rock 1
        // Y = Paper 2
        // Z = Scissors 3
        // Lost 0
        // Draw 3
        // Win 6

        foreach (var line in lines)
        {
            switch (line[2])
            {
                case 'X':
                    switch (line[0])
                    {
                        case 'A':
                            result += 3;
                            break;
                        case 'B':
                            result += 1;
                            break;
                        case 'C':
                            result += 2;
                            break;
                        default:
                            break;
                    }
                    break;
                case 'Y':
                    switch (line[0])
                    {
                        case 'A':
                            result += 4;
                            break;
                        case 'B':
                            result += 5;
                            break;
                        case 'C':
                            result += 6;
                            break;
                        default:
                            break;
                    }
                    break;
                case 'Z':
                    switch (line[0])
                    {
                        case 'A':
                            result += 8;
                            break;
                        case 'B':
                            result += 9;
                            break;
                        case 'C':
                            result += 7;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        return result;
    }

    public static int Scissors(string fileName)
    {
        List<string> lines = ReadLines(fileName);

        int result = 0;
        int myPlay = 0;
        int hisPlay = 0;
        // A = Rock
        // B = Paper
        // C = Scissors
        // X = Rock 1
        // Y = Paper 2
        // Z = Scissors 3
        // Lost 0
        // Draw 3
        // Win 6

        foreach (var line in lines)
        {
            switch(line[2])
            {
                case 'X':
                    myPlay = 1;
                    break;
                case 'Y':
                    myPlay = 2;
                    break;
                case 'Z':
                    myPlay = 3;
                    break;
                default:
                    break;
            }

            result += myPlay;

            switch (line[0])
            {
                case 'A':
                    hisPlay = 1;
                    break;
                case 'B':
                    hisPlay = 2;
                    break;
                case 'C':
                    hisPlay = 3;
                    break;
                default:
                    break;
            }

            if (myPlay == hisPlay) {
                result += 3;
            } 
            else if (myPlay == 1 && hisPlay == 3 
                || myPlay == 2 && hisPlay == 1
                || myPlay == 3 && hisPlay == 2)
            {
                result += 6;
            } 
            else 
            {
                result += 0;
            }

        }

        return result;
    }

    public static Double Calories2(string fileName)
    {
        List<string> lines = ReadLines(fileName);
        List<Double> results = new List<Double>();

        Double actual = 0;

        foreach (var line in lines)
        {
            if (Double.TryParse(line, out Double number))
            {
                actual += number;
            }
            else
            {
                results.Add(actual);
                actual = 0;
            }
        }

        results.Sort();

        return results[results.Count - 1] + results[results.Count - 2] + results[results.Count - 3];
    }

    public static int Calories1(string fileName)
    {
        List<string> lines = ReadLines(fileName);
        int maximum = 0;
        int actual = 0;

        foreach (var line in lines)
        {
            if (Int16.TryParse(line, out short number))
            {
                actual += number;
            }
            else if (String.IsNullOrEmpty(line))
            {
                if (maximum < actual)
                {
                    maximum = actual;
                }
                actual = 0;
            }
        }

        return maximum;
    }

    private static List<string> ReadLines(string fileName)
    {
        string path = Path.Combine(Environment.CurrentDirectory, @"sources\", fileName);
        return File.ReadAllLines(path).ToList();
    }

    private static List<int> getLetters(string line)
    {
        byte[] asciiBytes = Encoding.ASCII.GetBytes(line);
        List<int> letters = getAscii(asciiBytes);

        return letters;
    }

    private static List<int> getAscii(byte[] asciiBytes)
    {
        List<int> letters = new List<int>();

        foreach (var letter in asciiBytes)
        {
            if (letter > 96)
            {
                letters.Add(letter - 96);
            }
            else
            {
                letters.Add(letter - 64 + 26);
            }
        }

        return letters;
    }
}