using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

internal interface IPuzzle
{
    
    object? SolvePart1(string input) => this is IPuzzleEx ? ((IPuzzleEx)this).SolvePart1(new PuzzleInput(input)) : null;
    object? SolvePart2(string input) => this is IPuzzleEx ? ((IPuzzleEx)this).SolvePart2(new PuzzleInput(input)) : null;
}