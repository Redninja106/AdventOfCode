using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day4;

internal class Day4 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        var passports = input.Split("\r\n\r\n");
        int validPassports = 0;

        foreach (var passport in passports)
        {
            var parts = passport.Split(':', ' ', '\n');
            Dictionary<string, string> passportDictionary = new();

            for (int i = 0; i < parts.Length; i+=2)
            {
                passportDictionary.Add(parts[i].Trim(), parts[i + 1].Trim());
            }

            if (passportDictionary.ContainsKey("byr") &&
                passportDictionary.ContainsKey("iyr") &&
                passportDictionary.ContainsKey("eyr") &&
                passportDictionary.ContainsKey("hgt") &&
                passportDictionary.ContainsKey("hcl") &&
                passportDictionary.ContainsKey("ecl") &&
                passportDictionary.ContainsKey("pid"))
                validPassports++;
        }

        return validPassports;
    }

    public object? SolvePart2(string input)
    {
        var passports = input.Split("\r\n\r\n");
        int validPassports = 0;

        foreach (var passportStr in passports)
        {
            var parts = passportStr.Split(':', ' ', '\n');
            Dictionary<string, string> passport = new();

            for (int i = 0; i < parts.Length; i += 2)
            {
                passport.Add(parts[i].Trim(), parts[i + 1].Trim());
            }

            //if (passportDictionary.ContainsKey("byr") &&
            //    passportDictionary.ContainsKey("iyr") &&
            //    passportDictionary.ContainsKey("eyr") &&
            //    passportDictionary.ContainsKey("hgt") &&
            //    passportDictionary.ContainsKey("hcl") &&
            //    passportDictionary.ContainsKey("ecl") &&
            //    passportDictionary.ContainsKey("pid"))
            //    validPassports++;

            if (ValidatePassport(passport))
            {
                validPassports++;
            }
        }

        return validPassports;
    }

    private bool ValidatePassport(Dictionary<string,string> passport)
    {
        bool valid = passport.ContainsKey("byr") &&
                passport.ContainsKey("iyr") &&
                passport.ContainsKey("eyr") &&
                passport.ContainsKey("hgt") &&
                passport.ContainsKey("hcl") &&
                passport.ContainsKey("ecl") &&
                passport.ContainsKey("pid");
        foreach (var (key, value) in passport)
        {
            int.TryParse(value, out int ivalue);

            switch (key)
            {
                case "byr":
                    valid &= ivalue >= 1920 && ivalue <= 2002;
                    break;
                case "iyr":
                    valid &= ivalue >= 2010 && ivalue <= 2020;
                    break;
                case "eyr":
                    valid &= ivalue >= 2020 && ivalue <= 2030;
                    break;
                case "hgt":
                    if (int.TryParse(value, out _))
                    {
                        valid = false;
                        break;
                    }

                    var height = int.Parse(value[..^2]);
                    if (value.EndsWith("cm"))
                        valid &= height >= 150 && height <= 193;
                    else
                        valid &= height >= 59 && height <= 76;
                    break;
                case "hcl":
                    if (value.Length != 7)
                    {
                        valid = false;
                        break;
                    }
                    valid &= value[0] == '#';
                    for (int i = 1; i < 7; i++)
                    {
                        valid &= char.IsNumber(value[i]) || (value[i] >= 'a' && value[i] <= 'f');
                    }
                    break;
                case "ecl":
                    valid &= value == "amb" ||
                        value == "blu" ||
                        value == "brn" ||
                        value == "gry" ||
                        value == "grn" ||
                        value == "hzl" ||
                        value == "oth";
                    break;
                case "pid":
                    valid &= value.Length == 9 && int.TryParse(value, out _);
                    break;
                default:
                    break;
            }
        }

        return valid;
    }
}