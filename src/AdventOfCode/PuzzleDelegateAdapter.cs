using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

internal class PuzzleDelegateAdapter : IPuzzle
{
    private readonly Func<string, object>? solvePart1;
    private readonly Func<string, object>? solvePart2;

    public PuzzleDelegateAdapter(Func<string, object>? solvePart1, Func<string, object>? solvePart2)
    {
        this.solvePart1 = solvePart1;
        this.solvePart2 = solvePart2;
    }

    public object? SolvePart1(string input)
    {
        return solvePart1?.Invoke(input);
    }

    public object? SolvePart2(string input)
    {
        return solvePart2?.Invoke(input);
    }
}