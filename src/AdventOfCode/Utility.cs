using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;
public static class Utility
{
    //public static char[,] ArrayResize2D(char[,] array, int width, int height)
    //{
    //    var result = new char[width, height];
    //}

    // parses a string into a 2d char array, assuming each row ends with a newline, and optionally applied a mutator func to each char
    public static char[,] ParseCharGridInput(string input, Func<char, char> mutator = null)
    {
        if (mutator == null)
            mutator = c => c;

        var lines = input.Split(Environment.NewLine);
        int height = lines.Length, width = lines.Max(s => s.Length);
        var result = new char[width, height];

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                result[j, i] = lines[i][j];
            }
        }

        return result;
    }

    public static string Merge(this IEnumerable<string> strings)
    {
        return strings.Aggregate("", (a, s) => a += s);
    }

    public static string Merge(this IEnumerable<string> strings, string separator)
    {
        return strings.Aggregate("", (a, s) => a += s + separator)[..^separator.Length];
    }

    public static string[] SplitSafe(this string s, string separator)
    {
        return s.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
}