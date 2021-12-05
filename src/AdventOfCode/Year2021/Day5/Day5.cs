using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021.Day5;

internal class Day5 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        var parts = input.Split("\r\n");

        var size = input.Split(new[] {",", " -> ", "\r\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).Max();

        var grid = new int[size+1, size+1];

        foreach (var part in parts)
        {
            var words = part.Split(new[] { " -> ", "," }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();

            var beginX = words[0];
            var beginY = words[1];
            var endX = words[2];
            var endY = words[3];

            if (beginX == endX)
            {
                if (beginY > endY)
                {
                    var temp = beginY;
                    beginY = endY;
                    endY = temp;
                }

                for (int y = beginY; y <= endY; y++)
                {
                    grid[beginX, y]++;
                }
            }
            if (beginY == endY)
            {
                if (beginX > endX)
                {
                    var temp = beginX;
                    beginX = endX;
                    endX = temp;
                }

                for (int x = beginX; x <= endX; x++)
                {
                    grid[x, beginY]++;
                }
            }
        }

        //for (int x = 0; x < grid.GetLength(0); x++)
        //{
        //    for (int y = 0; y < grid.GetLength(1); y++)
        //    {
        //        Console.Write(grid[x, y].ToString() + " ");
        //    }
        //    Console.WriteLine();
        //}

        int count = 0;

        foreach (var val in grid)
        {
            if (val > 1) count++;
        }

        return count;
    }

    public object? SolvePart2(string input)
    {
        var parts = input.Split("\r\n");

        var size = input.Split(new[] { ",", " -> ", "\r\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).Max();

        var grid = new int[size + 1, size + 1];

        foreach (var part in parts)
        {
            var words = part.Split(new[] { " -> ", "," }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();

            var beginX = words[0];
            var beginY = words[1];
            var endX = words[2];
            var endY = words[3];

            
            if (beginX == endX)
            {
                if (beginY > endY)
                {
                    var temp = beginY;
                    beginY = endY;
                    endY = temp;
                }

                for (int y = beginY; y <= endY; y++)
                {
                    grid[beginX, y]++;
                }
            }
            else if (beginY == endY)
            {
                if (beginX > endX)
                {
                    var temp = beginX;
                    beginX = endX;
                    endX = temp;
                }

                for (int x = beginX; x <= endX; x++)
                {
                    grid[x, beginY]++;
                }
            }
            else
            {
                for (int x = Math.Min(beginX, endX); x <= Math.Max(beginX, endX); x++)
                {
                    var m = (beginY - endY) / (beginX - endX);
                    int y = m * (x - beginX) + beginY;

                    grid[x, y]++;
                }
            }
        }

     

        int count = 0;

        foreach (var val in grid)
        {
            if (val > 1) count++;
        }

        return count;
    }
}