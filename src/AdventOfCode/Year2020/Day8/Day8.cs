using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day8;

internal class Day8 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        var instructions = input.Split("\r\n");
        
        List<int> instructionhistory = new();

        int pointer = 0, accumulator = 0;
        while (true)
        {
            if (instructionhistory.Contains(pointer))
            {
                break;
            }

            var parts = instructions[pointer].Split(" ");
            instructionhistory.Add(pointer);
            
            switch (parts[0])
            {
                case "nop":
                    pointer++;
                    break;
                case "acc":
                    accumulator += int.Parse(parts[1]);
                    pointer++;
                    break;
                case "jmp":
                    pointer += int.Parse(parts[1]);
                    break;
                default:
                    break;
            }
        }

        return accumulator;
    }

    public bool ProgramExits(string[] instructions, out int accumulator)
    {
        List<int> instructionhistory = new();

        int pointer = 0;
        accumulator = 0;
        
        while (true)
        {
            if (pointer >= instructions.Length)
                return true;

            if (instructionhistory.Contains(pointer))
                return false;

            var parts = instructions[pointer].Split(" ");
            instructionhistory.Add(pointer);

            switch (parts[0])
            {
                case "nop":
                    pointer++;
                    break;
                case "acc":
                    accumulator += int.Parse(parts[1]);
                    pointer++;
                    break;
                case "jmp":
                    pointer += int.Parse(parts[1]);
                    break;
                default:
                    break;
            }
        }
    }

    public object? SolvePart2(string input)
    {
        var instructions = input.Split("\r\n");

        for (int i = 0; i < instructions.Length; i++)
        {
            if (instructions[i].StartsWith("nop"))
            {
                var localInstructions = new string[instructions.Length];
                instructions.CopyTo(localInstructions, 0);
                localInstructions[i] = localInstructions[i].Replace("nop", "jmp");

                if (ProgramExits(localInstructions, out int accumulator))
                    return accumulator;
            }
            else if (instructions[i].StartsWith("jmp"))
            {
                var localInstructions = new string[instructions.Length];
                instructions.CopyTo(localInstructions, 0);
                localInstructions[i] = localInstructions[i].Replace("jmp", "nop");

                if (ProgramExits(localInstructions, out int accumulator))
                    return accumulator;
            } 
        }

        return -1;
    }
}