using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2019.Day2
{
    internal class Day2 : IPuzzle
    {
        public object? SolvePart1(string input)
        {
            var values = input.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
            values[1] = 12;
            values[2] = 2;

            int pointer = 0;
            while (values[pointer] != 99)
            {
                ref int a = ref values[values[pointer+1]];
                ref int b = ref values[values[pointer+2]];
                ref int c = ref values[values[pointer+3]];
                if (values[pointer] == 1)
                {
                    c = a + b;
                }
                else
                {
                    c = a * b; 
                }
                
                pointer += 4;
            }

            return values[0];
        }

        public object? SolvePart2(string input)
        {
            long expected = 19690720;
            var values = input.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var memory = values.ToArray();

                    memory[1] = noun;
                    memory[2] = verb;

                    int pointer = 0;


                    while (memory[pointer] != 99)
                    {
                        ref var a = ref memory[memory[pointer + 1]];
                        ref var b = ref memory[memory[pointer + 2]];
                        ref var c = ref memory[memory[pointer + 3]];
                        if (memory[pointer] == 1)
                        {
                            c = a + b;
                        }
                        else
                        {
                            c = a * b;
                        }

                        pointer += 4;
                    }

                    if (memory[0] == expected)
                    {
                        return 100 * noun + verb;
                    }
                }
            }

            return -1;
        }
    }
}