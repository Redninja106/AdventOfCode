using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day5;
internal class Day5 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        var passes = input.Split("\r\n");
        
        int highestID = 0;

        foreach (var pass in passes)
        {
            var id = GetId(pass);
            highestID = Math.Max(highestID, id);
        }

        return highestID;
    }

    public object? SolvePart2(string input)
    {
        var passes = input.Split("\r\n");

        List<int> ids = new(passes.Select(p=>GetId(p)));

        ids.Sort();

        int lastid = ids.First();
        for (int i = 1; i < ids.Count; i++)
        {
            if (ids[i] != lastid+1)
            {
                return ids[i]-1;
            }
            lastid = ids[i];
        }

        return null;
    }

    private int GetId(string pass)
    {
        var idstr = new string(pass.ToCharArray().Select(c => c == 'F' || c == 'L' ? '0' : '1').ToArray());
        return Convert.ToInt32(idstr, 2);
    }
}
