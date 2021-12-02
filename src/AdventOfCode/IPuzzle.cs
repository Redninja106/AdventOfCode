using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

internal interface IPuzzle
{
    object? SolvePart1(string input);
    object? SolvePart2(string input);
}