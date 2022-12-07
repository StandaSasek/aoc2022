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
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;

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
        //Console.WriteLine(Cleaning2("day04.txt"));
        //Console.WriteLine(Crates("day05crates.txt", "day05moves.txt"));
        Console.WriteLine(Crates2("day05crates.txt", "day05moves.txt"));


    }

    public static string Crates2(string cratesPiles, string movesFile)
    {
        List<string> lines = ReadLines(cratesPiles);
        List<List<char>> crates = CratesToPiles(lines);
        List<string> movesLines = ReadLines(movesFile);
        List<int> moves = new List<int>();

        foreach (var line in movesLines)
        {
            List<string> zones = line.Split(' ').ToList();

            for (int i = 1; i < zones.Count; i += 2)
            {
                if (Int16.TryParse(zones[i], out short number))
                {
                    moves.Add(number);
                }
            }
        }

        for (int i = 0; i < moves.Count; i++)
        {
            int cratesToMove = moves[i];
            int from = moves[++i] - 1;
            int to = moves[++i] - 1;
            
            if (crates[from].Any())
            {
                var indexFrom = crates[from].Count - cratesToMove;
                crates[to].AddRange(crates[from].GetRange(indexFrom, crates[from].Count - indexFrom));
                crates[from].RemoveRange(indexFrom, cratesToMove);
            }
        }

        var topCrates = "";

        foreach (var column in crates)
        {
            topCrates += column[column.Count - 1];
        }

        return topCrates;
    }

    public static string Crates(string cratesPiles, string movesFile)
    {
        List<string> lines = ReadLines(cratesPiles);
        List<List<char>> crates = CratesToPiles(lines);
        List<string> movesLines = ReadLines(movesFile);
        List<int> moves = new List<int>();

        foreach (var line in movesLines)
        {
            List<string> zones = line.Split(' ').ToList();

            for (int i = 1; i < zones.Count; i += 2)
            {
                if (Int16.TryParse(zones[i], out short number))
                {
                    moves.Add(number);
                }
            }
        }

        for (int i = 0; i < moves.Count; i++)
        {
            int cratesToMove = moves[i];
            int from = moves[++i] - 1;
            int to = moves[++i] - 1;

            for (int j = 0; j < cratesToMove; j++)
            {
                int topCrateIndex = crates[from].Count - 1;

                if (crates[from].Any())
                {
                    char crate = crates[from][topCrateIndex];
                    crates[from].RemoveAt(topCrateIndex);
                    crates[to].Add(crate);
                }
            }
        }

        var topCrates = "";

        foreach (var column in crates)
        {
            topCrates += column[column.Count - 1];
        }

        return topCrates;
    }

    private static List<List<char>> CratesToPiles(List<string> lines)
    {
        List<List<char>> crates = new List<List<char>>();
        int columns = ((lines[0].Length - 3) / 4) + 1;

        for (int i = 0; i < columns; i++)
        {
            crates.Add(new List<char>());
        }

        for (int i = 0; i < lines.Count - 1; i++)
        {
            int counter = 0;
            int columnIndex = 0;

            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == ' ')
                {
                    counter++;
                }
                else if (counter == 1)
                {
                    columnIndex++;
                    counter = 0;
                    crates[columnIndex].Add(lines[i][j + 1]);
                    j = j + 2;
                    continue;
                }
                else if (counter % 4 == 0)
                {
                    columnIndex += counter / 4;
                    counter = 0;
                    crates[columnIndex].Add(lines[i][j + 1]);
                    j = j + 2;
                }
                else if ((counter % 4) == 1)
                {
                    columnIndex += ((counter - 1) / 4) + 1;
                    counter = 0;
                    crates[columnIndex].Add(lines[i][j + 1]);
                    j = j + 2;
                }
            }
        }

        foreach (var column in crates)
        {
            column.Reverse();
        }

        return crates;
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