using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day10;

internal class Day10 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        var adapters = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
        adapters.Sort();
        int oneCount = 1, threeCount = 1;
        for (int i = 0; i < adapters.Count-1; i++)
        {
            if (adapters[i + 1] - adapters[i] == 1)
                oneCount++;
            if (adapters[i + 1] - adapters[i] == 3)
                threeCount++;
        }

        return oneCount * threeCount;
    }

    public object? SolvePart2(string input)
    {
        var adapters = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
        adapters.Sort();
        adapters.Insert(0, 0);

        int count = 0;
        for (int i = 1; i < adapters.Count-1; i++)
        {
            if (adapters[i+1] - adapters[i - 1] <= 3)
            {
                count++;
            }
        }
        
        return count;
    }
}