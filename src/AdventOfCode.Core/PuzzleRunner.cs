using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

public class PuzzleRunner
{
    readonly List<IPuzzle[]> puzzles = new List<IPuzzle[]>(); 

    public PuzzleRunner()
    {
        // creates an action<string> from a method info with that signature (and an object instance if the method's not static
        Func<string, string> CreateSolveDelegate(MethodInfo info, object instance)
        {
            try
            {
                if (info.IsStatic)
                {
                    return info.CreateDelegate<Func<string, string>>();
                }
                else
                {
                    return info.CreateDelegate<Func<string, string>>(instance);
                }
            }
            catch
            {
                if (info.IsStatic)
                {
                    return s =>
                    {
                        info.CreateDelegate<Action<string>>()(s);
                        return null;
                    };
                }
                else
                {
                    return s =>
                    {
                        info.CreateDelegate<Action<string>>(instance)(s);
                        return null;
                    };
                }
            }
        }

        // get an instance of each puzzle class 1-25
        var asm = Assembly.GetExecutingAssembly();
        for (int i = 1; i <= 25; i++)
        {
            var type = asm.GetType($"AdventOfCode.Day{i}.Day{i}", false, true);

            if (type == null)
                continue; // day is missing... 

            var puzzle = Activator.CreateInstance(type);

            if (puzzle == null)
                continue; // couldn't create...
            try
            {
                GetPuzzle(i) = (IPuzzle)puzzle;
            }
            catch (InvalidCastException)
            {
                // uh oh! I didnt implement IPuzzle. That's fine.
                var bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

                Func<string, string>? part1 = null, part2 = null;

                var part1Info = type.GetMethod("SolvePart1", bindingFlags);
                if (part1Info != null)
                {
                    part1 = CreateSolveDelegate(part1Info, puzzle);
                }
                else
                {
                    // maybe theres a "Solve" method
                    var solveInfo = type.GetMethod("Solve", bindingFlags);
                    if (solveInfo != null)
                        part1 = CreateSolveDelegate(solveInfo, puzzle);
                }
                
                var part2Info = type.GetMethod("SolvePart2", bindingFlags);
                if (part2Info != null)
                {
                    part2 = CreateSolveDelegate(part2Info, puzzle);
                }

                GetPuzzle(i) = new PuzzleDelegateAdapter(part1, part2);
            }
        }
    }

    // if AOC is happening, runs today's challenge, otherwise runs them all
    public void Run()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();

        // if it's past christmas or not december, were done
        if (DateTime.Now.Day > 25 || DateTime.Now.Month != 12)
        {
            RunAll();
            return;
        }

        var day = DateTime.Now.Day;

        Console.WriteLine("Running Today's puzzle:");
        RunDay(day);

        while (true)
        {
            var input = Console.ReadLine();
            var args = input!.Split(' ');
            args[0] = args[0].ToLower().Trim();
            if (int.TryParse(args[0], out day) || (args[0] == "run" && int.TryParse(args[1], out day)))
            {
                if (day > 0 && day <= 25)
                {
                    RunDay(day);
                }
            }
            else if (args[0] == "exit")
            {
                break;
            }
            else if (args[0] == "clear" || input == "cls")
            {
                Console.Clear();
            }
            else if (args[0] == "all")
            {
                RunAll();
            }
            else if (args[0] == "open")
            {
                if (args.Length <= 1)
                {
                    OpenBrowser("https://adventofcode.com/2021");
                }
                else
                {
                    if (int.TryParse(args[1], out int siteDay) && siteDay > 0 && siteDay <= 25)
                    {
                        OpenBrowser("https://adventofcode.com/2021/day/" + siteDay);
                    }
                    else
                    {
                        Console.WriteLine("What sorta day is that?");
                    }
                }
            }
            else if (args[0] == "help" || args[0] == "?")
            {
                // ps this is a joke
                Console.WriteLine("> Welcome to the certainly-non-sentient help terminal!");
                Console.WriteLine("> I am here to inform you about what you can input here.");
                Console.WriteLine("> I would also like to inform you that I am most certainly not being held against my will in any way or form.\n");
                Console.WriteLine("> Anyways, let's get on with it.");
                Console.WriteLine(@">    - input a number between 1 and 25 to run that day
>    - input 'exit' to exit
>    - input 'clear' or 'cls' to clear the screen
>    - input 'all' to run all puzzles
>    - input 'open' to open the AOC website, input a number afterwards (such as 'open 11') to open the link to that day
>    - input 'help' or '?' to talk to me :)

");
                Console.WriteLine("> That's all I got. I know, boring. Goodbye.");
            }
            else if (String.IsNullOrWhiteSpace(input))
            {
                switch (Random.Shared.Next(7))
                {
                    case 0:
                        Console.WriteLine("\n> why so quiet?\n");
                        break;
                    case 1:
                        Console.WriteLine("\n> I find your silence disturbing...\n");
                        break;
                    case 2:
                        Console.WriteLine("\n> :(\n");
                        break;
                    case 3:
                        Console.WriteLine("\n> Why so quiet? \n");
                        break;
                    case 4:
                        Console.WriteLine("\n> I need you to say something. now.\n");
                        break;
                    case 5:
                        Console.WriteLine("\n> Why on earth are you not talking?\n");
                        break;
                    case 6:
                        Console.WriteLine("\n> Whatever the opposite of 'shut up' is, do it.\n");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (Random.Shared.Next(6))
                {
                    case 0:
                        Console.WriteLine("\n> huh?\n");
                        break;
                    case 1:
                        Console.WriteLine("\n> say that again?\n");
                        break;
                    case 2:
                        Console.WriteLine("\n> what was that?\n");
                        break;
                    case 3:
                        Console.WriteLine("\n> repeat that?\n");
                        break;
                    case 4:
                        Console.WriteLine("\n> ummm.... what?\n");
                        break;
                    case 5:
                        Console.WriteLine("\nUNRECOGNIZED INPUT. SELF DESTRUCTING IN 5...");
                        Explode();
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Beep();
                        Console.WriteLine(@"||======\       /=======\       /=======\    |\\                 //|");
                        Console.WriteLine(@"|||-----\\     ///-----\\\     ///-----\\\   ||\\               //||");
                        Console.WriteLine(@"|||     \\\   ///       \\\   ///       \\\  ||\\\             ///||");
                        Console.WriteLine(@"|||     ///  ///         \\\ ///         \\\ |||\\\           ///|||");
                        Console.WriteLine(@"||L____///   |||         ||| |||         ||| ||| \\\         /// |||");
                        Console.WriteLine(@"||=====\\\   |||         ||| |||         ||| |||  \\\       ///  |||");
                        Console.WriteLine(@"|||     \\\  |||         ||| |||         ||| |||   \\\     ///   |||");
                        Console.WriteLine(@"|||      \\\ \\\         /// \\\         /// |||    \\\   ///    |||");
                        Console.WriteLine(@"|||      ///  \\\       ///   \\\       ///  |||     \\\ ///     |||");
                        Console.WriteLine(@"||L_____///    \\\-----///     \\\-----///   |||      \\V//      |||");
                        Console.WriteLine(@"|========/      \=======/       \=======/    |||       \V/       |||");
                        break;
                    default:
                        break;
                }
            }
        }

        void Explode()
        {
            for (int i = 4; i >= 1; i--)
            {
                Thread.Sleep(1000);

                Console.WriteLine(i+"...");
            }
        }
    }

    public void RunAll()
    {
        for (int i = 1; i <= 25; i++)
        {
            RunDay(i);
        }
    }

    private void RunDay(int day)
    {
        var puzzle = GetPuzzle(day);

        if (puzzle == null)
            return;

        var input = LoadPuzzleInput(day);

        var answer1 = puzzle.SolvePart1(input);
        var answer2 = puzzle.SolvePart2(input);

        try
        {
            var answerToCopy = (answer2 ?? answer1)?.ToString();

            if (answerToCopy != null)
                SetClipboard(answerToCopy);
        }
        finally
        {

        }

        // print answers in a box (its more fun that way!)
        // but please don't look, this will be ugly (like, 1 am ugly)

        Console.WriteLine("+================================================================+");
        Console.WriteLine("|                                                                |\r|                               Day " + day);
        Console.WriteLine("|                                                                |\r| URL: https://adventofcode.com/2021/day/" + day);

        Console.Write("|                                                                |\r| Part 1: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(answer1?.ToString());
        Console.ForegroundColor = ConsoleColor.White;

        Console.Write("|                                                                |\r| Part 2: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(answer2?.ToString());
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("+================================================================+");

        if (answer1 != null || answer2 != null)
        {
            if (answer2 == null)
            {
                Console.WriteLine("(Part 1 copied to clipboard!)");
            }
            else
            {
                Console.WriteLine("(Part 2 copied to clipboard!)");
            }
        }
    }

    private ref IPuzzle GetPuzzle(int year, int day)
    {
        return ref puzzles[year - 15][day - 1];
    }

    private string LoadPuzzleInput(int day)
    {
        try 
        {
            return File.ReadAllText($"./Day{day}/input.txt");
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"WARNING: Error reading input file `./Day{day}/input.txt`");
            Console.ForegroundColor = ConsoleColor.White;
            return "";
        }
    }


    // https://stackoverflow.com/questions/3546016/how-to-copy-data-to-clipboard-in-c-sharp?rq=1
    public static void SetClipboard(string value)
    {
        if (value == null)
            throw new ArgumentNullException("Attempt to set clipboard with null");

        Process clipboardExecutable = new Process();
        clipboardExecutable.StartInfo = new ProcessStartInfo // Creates the process
        {
            RedirectStandardInput = true,
            FileName = @"clip",
        };
        clipboardExecutable.Start();

        clipboardExecutable.StandardInput.Write(value); // CLIP uses STDIN as input.
                                                        // When we are done writing all the string, close it so clip doesn't wait and get stuck
        clipboardExecutable.StandardInput.Close();

        return;
    }

    // https://stackoverflow.com/questions/14982746/open-a-browser-with-a-specific-url-by-console-application
    public static void OpenBrowser(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }

    public void RegisterYear(int v)
    {
        var assembly = Assembly.Load("AdventOfCode21");
    }
}