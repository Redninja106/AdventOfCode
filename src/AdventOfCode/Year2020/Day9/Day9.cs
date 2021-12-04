using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day9;

internal class Day9 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        var numbers = input.Split("\r\n").Select(long.Parse).ToArray();
        const int scan = 25;

        for (int i = scan; i < numbers.Length; i++)
        {
            bool valid = false;
            for (int j = 1; j <= scan; j++)
            {
                for (int k = 1; k <= scan; k++)
                {
                    if (numbers[i] == numbers[i-j] + numbers[i-k])
                        valid = true;
                }
            }

            if (!valid)
                return numbers[i];
        }

        return false;
    }

    public object? SolvePart2(string input)
    {
        var numbers = input.Split("\r\n").Select(long.Parse).ToArray();
        const long sumTarget = 1212510616;

        List<long> results;
        for (int i = 0; i < numbers.Length; i++)
        {
            results = new();
            for (int j = 0; results.Sum() < sumTarget; j++)
            {
                results.Add(numbers[i + j]);
            }

            if (results.Sum() == sumTarget)
                return results.Min() + results.Max();
        }

        return "no range found";
    }
}
