using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021.Day6
{
    internal class Day6 : IPuzzle
    {
        public object? SolvePart1(string input)
        {
            int days = 80;

            var values = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            for (int d = 1; d <= days; d++)
            {
                var count = values.Count;
                for (int i = 0; i < count; i++)
                {
                    values[i]--;
                    if (values[i] < 0)
                    {
                        values.Add(8);
                        values[i] = 6;
                    }
                }
            }

            return values.Count;
        }

        public object? SolvePart2(string input)
        {
            int days = 256;

            var values = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            var fish = new long[9];
            for (int i = 0; i < values.Count; i++)
            {
                fish[values[i]]++;
            }

            for (int d = 0; d < days; d++)
            {
                var newFishCount = fish[0];

                for (int i = 1; i < fish.Length; i++)
                {
                    fish[i - 1] = fish[i];
                }
                fish[6] += newFishCount;
                fish[8] = newFishCount;

                //var index = d % 9;
                //fish[(index + 8) % 9] += fish[index];
                //fish[(index + 6) % 9] += fish[index];
                //fish[index] = 0;
            }

            return fish.Sum(x=>x);
        }
    }
}