using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021.Day3;

public class Day3 : IPuzzle
{
    object? IPuzzle.SolvePart1(string input)
    {
        var lines = input.Split("\r\n");

        string gamma = "";
        string epsilon = "";

        for (int i = 0; i < lines[0].Count(); i++)
        {
            int oneCount = 0, zeroCount = 0;
            for (int j = 0; j < lines.Length; j++)
            {
                if (lines[j][i] == '0')
                {
                    zeroCount++;
                }
                else oneCount++;

            }
            gamma += zeroCount < oneCount ? '1' : '0';
            epsilon += zeroCount < oneCount ? '0' : '1';
        }

        return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
    }

    object? IPuzzle.SolvePart2(string input)
    {
        int co2 = 0, oxy = 0;
        var lines = input.Split("\r\n");

        var nums = lines.ToList();
        int oneCount = 0, zeroCount = 0;
        int i = 0;
        while (nums.Count() > 1)
        {
            oneCount = 0;
            zeroCount = 0;
            for (int j = 0; j < nums.Count(); j++)
            {
                if (nums[j][i] == '0')
                    zeroCount++;
                else
                    oneCount++;
            }
            if (oneCount >= zeroCount)
            {
                nums = nums.Where(x => x[i] == '1').ToList();
            }
            else
            {
                nums = nums.Where(x => x[i] == '0').ToList();
            }

            i++;
        }

        oxy = Convert.ToInt32(nums.First(), 2);

        // co2

        nums = lines.ToList();
        oneCount = 0;
        zeroCount = 0;
        i = 0;
        while (nums.Count() > 1)
        {
            oneCount = 0;
            zeroCount = 0;
            for (int j = 0; j < nums.Count(); j++)
            {
                if (nums[j][i] == '0')
                    zeroCount++;
                else
                    oneCount++;
            }
            if (zeroCount <= oneCount)
            {
                nums = nums.Where(x => x[i] == '0').ToList();
            }
            else
            {
                nums = nums.Where(x => x[i] == '1').ToList();

            }

            i++;
        }
        co2 = Convert.ToInt32(nums.First(), 2);


        return co2 * oxy;
    }
}