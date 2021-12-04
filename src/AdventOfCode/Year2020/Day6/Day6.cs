using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day6;

internal class Day6 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        int count = 0;
        var groups = input.Split("\r\n\r\n").Select(s=>s.ToCharArray());
        foreach (var group in groups)
        {
            for (int i = 'a'; i <= 'z'; i++)
            {
                if (group.Contains((char)i))
                {
                    count++;
                }
            }
        }
        return count;
    }

    public object? SolvePart2(string input)
    {
        int count = 0;
        var groups = input.Split("\r\n\r\n");
        foreach (var group in groups)
        {
            for (int i = 'a'; i <= 'z'; i++)
            {
                bool cond = true;
                foreach (var person in group.Split("\r\n"))
                {
                    cond &= person.Contains((char)i);
                }

                if (cond)
                    count++;

            }
        }
        return count;
    }
}