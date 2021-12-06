using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2019.Day1
{
    internal class Day1 : IPuzzle
    {
        public object? SolvePart1(string input)
        {
            var mods = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);

            int sum = 0;
            foreach (var mod in mods)
            {
                sum += mod / 3 - 2;
            }

            return sum;
        }

        public object? SolvePart2(string input)
        {
            var mods = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);

            int sum = 0;
            foreach (var mod in mods)
            {
                int fuel = mod / 3 - 2;
                while (fuel > 0)
                {
                    sum += fuel;
                    fuel = fuel / 3 - 2;
                }
            }

            return sum;
        }
    }
}