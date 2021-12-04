using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day7;

public class Day7 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        var rules = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        List<(string parent, string child)> sets = new(); 
        foreach (var rule in rules)
        {
            var phrases = rule.Split(new[] { "bags contain", "," }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        
            var color = phrases[0];

            if (phrases[1] == "no other bags.")
                continue;

            for (int i = 1; i < phrases.Length; i++)
            {
                var parts = phrases[i].Split(' ');

                int count = int.Parse(parts[0]);

                string childColor = parts[1..3].Merge(" ");

                sets.Add((color, childColor));
            }
        }

        var resultList = new List<string>();
        GetAnswer1(sets, ref resultList);

        return resultList.Count;
    }

    public void GetAnswer1(List<(string parent, string child)> sets, ref List<string> bags, string color = "shiny gold")
    {
        foreach (var set in sets)
        {
            if (set.child == color)
            {
                if (!bags.Contains(set.parent))
                {
                    bags.Add(set.parent);
                }
                GetAnswer1(sets, ref bags, set.parent);
            }
        }
    }

    public object? SolvePart2(string input)
    {
        var rules = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        List<(string parent, string child, int count)> sets = new();
        foreach (var rule in rules)
        {
            var phrases = rule.Split(new[] { "bags contain", "," }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var color = phrases[0];

            if (phrases[1] == "no other bags.")
            {
                sets.Add((color, "", 0));
                continue;
            }

            for (int i = 1; i < phrases.Length; i++)
            {
                var parts = phrases[i].Split(' ');

                int count = int.Parse(parts[0]);

                string childColor = parts[1..3].Merge(" ");

                sets.Add((color, childColor, count));
            }
        }

        int result = GetAnswer2(sets);

        return result-1;
    }

    public int GetAnswer2(List<(string parent, string child, int count)> sets, string color = "shiny gold")
    {
        int result = 1;

        foreach (var set in sets.Where(set => set.parent == color))
        {
            result += GetAnswer2(sets, set.child) * set.count;
        }

        return result;
    }
}