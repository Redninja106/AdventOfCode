using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

internal interface IPuzzleEx : IPuzzle
{
    object? SolvePart1(PuzzleInput input);
    object? SolvePart2(PuzzleInput input);
}