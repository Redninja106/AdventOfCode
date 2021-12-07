using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021.Day7;

internal class Day7 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        var crabs = input.SplitSafe(",").Select(int.Parse).ToArray();

        var crabPositions = new int[crabs.Max() + 1];

        for (int i = 0; i < crabs.Length; i++)
        {
            crabPositions[crabs[i]]++;
        }

        List<int> costs = new List<int>();
        for (int i = 0; i < crabPositions.Length; i++)
        {
            int cost = 0;
            for (int j = 0; j < crabPositions.Length; j++)
            {
                cost += Math.Abs(i - j) * crabPositions[j];
            }
            costs.Add(cost);
        }

        return costs.Min();
    }

    public object? SolvePart2(string input)
    {
        var crabs = input.SplitSafe(",").Select(int.Parse).ToArray();

        var crabPositions = new int[crabs.Max() + 1];

        for (int i = 0; i < crabs.Length; i++)
        {
            crabPositions[crabs[i]]++;
        }

        List<int> costs = new List<int>();
        for (int i = 0; i < crabPositions.Length; i++)
        {
            int cost = 0;
            for (int j = 0; j < crabPositions.Length; j++)
            {
                for (int k = 1; k <= Math.Abs(i - j); k++)
                {
                    cost += k * crabPositions[j];
                }
            }
            costs.Add(cost);
        }

        return costs.Min();
    }
}