using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021.Day1;
internal class Day1 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        // parse input into list of ints
        var inputValues = input.Split(Environment.NewLine).Select(str => int.Parse(str.Trim())).ToList();

        int totalIncreased = 0;
        for (int i = 1; i < inputValues.Count; i++)
        {
            // if the current one is greater then the last
            if (inputValues[i] > inputValues[i - 1])
            {
                // increment the counter
                totalIncreased++;
            }
        }

        // and we have the solution
        return totalIncreased;
    }

    public object? SolvePart2(string input)
    {
        var inputValues = input.Split(Environment.NewLine).Select(str => int.Parse(str)).ToList();

        int totalIncreased = 0;
        for (int i = 0; i < inputValues.Count - 3; i++)
        {
            // if current window is less then current
            if (inputValues[i] + inputValues[i + 1] + inputValues[i + 2] < inputValues[i + 1] + inputValues[i + 2] + inputValues[i + 3])
            {
                // increment counter
                totalIncreased++;
            }
        }

        return totalIncreased;
    }
}