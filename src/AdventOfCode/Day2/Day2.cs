using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2;

internal class Day2 : IPuzzle
{
    public static void Solve(string input)
    {
        
    }

    public object? SolvePart1(string input)
    {
        var lines = input.Split(Environment.NewLine);

        int x = 0, depth = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            var val = int.Parse(parts[1]);
            switch (parts[0])
            {
                case "forward":
                    x += val;
                    break;
                case "down":
                    depth += val;
                    break;
                case "up":
                    depth -= val;
                    break;
                default:
                    throw new Exception();
            }
        }

        return x * depth;
    }

    public object? SolvePart2(string input)
    {
        var lines = input.Split(Environment.NewLine);

        int x = 0, depth = 0, aim = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            var val = int.Parse(parts[1]);
            switch (parts[0])
            {
                case "forward":
                    x += val;
                    depth += aim * val;
                    break;
                case "down":
                    aim += val;
                    break;
                case "up":
                    aim -= val;
                    break;
                default:
                    throw new Exception();
            }
        }

        return x * depth;
    }
}
