using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021.Day4;

internal class Day4 : IPuzzle
{
    public object? SolvePart1(string input)
    {
        var numbers = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).First().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        // parse boards
        List<int[,]> boards = new();

        foreach (var boardStr in input.Split("\r\n\r\n").ToArray()[1..])
        {
            var b = new int[5, 5];

            var lines = boardStr.Split("\r\n");
            for (int i = 0; i < 5; i++)
            {
                var parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x=>int.Parse(x)).ToArray();

                for (int j = 0; j < parts.Length; j++)
                {
                    b[i,j] = parts[j];
                }
            }

            boards.Add(b);
        }

        

        // draw numbers
        int num = 0;
        int[,] winningBoard = null;
        int[] drawnNumbers = null;

        bool CheckRow(int[,] board, int row)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!drawnNumbers.Contains(board[row, i]))
                    return false;
            }
            return true;
        }

        bool CheckColumn(int[,] board, int column)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!drawnNumbers.Contains(board[i, column]))
                    return false;
            }
            return true;
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            drawnNumbers = numbers[..i];
            num = numbers[i];


            foreach (var b in boards)
            {
                bool won = false;

                for (int k = 0; k < 5; k++)
                {
                    won |= CheckRow(b,k);
                    won |= CheckColumn(b,k);
                }

                if (won)
                    winningBoard = b;
            }

            if (winningBoard != null)
                break;
        }

        // get total of winning board
        int sum = 0;
        foreach (var val in winningBoard)
        {
            if (!drawnNumbers.Contains(val))
                sum += val;
        }

        return sum * drawnNumbers.Last();
    }

    public object? SolvePart2(string input)
    {
        var numbers = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).First().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        // parse boards
        List<int[,]> boards = new();

        foreach (var boardStr in input.Split("\r\n\r\n").ToArray()[1..])
        {
            var b = new int[5, 5];

            var lines = boardStr.Split("\r\n");
            for (int i = 0; i < 5; i++)
            {
                var parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                for (int j = 0; j < parts.Length; j++)
                {
                    b[i, j] = parts[j];
                }
            }

            boards.Add(b);
        }

        // draw numbers
        int num = 0;
        int[,] winningBoard = null;
        int[] drawnNumbers = null;

        bool CheckRow(int[,] board, int row)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!drawnNumbers.Contains(board[row, i]))
                    return false;
            }
            return true;
        }

        bool CheckColumn(int[,] board, int column)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!drawnNumbers.Contains(board[i, column]))
                    return false;
            }
            return true;
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            bool done = false;
            drawnNumbers = numbers[..i];
            num = numbers[i];

            for (int j = 0; j < boards.Count; j++)
            {
                var b = boards[j];
                bool won = false;

                for (int k = 0; k < 5; k++)
                {
                    won |= CheckRow(b, k);
                    won |= CheckColumn(b, k);
                }

                if (won)
                {
                    if (boards.Count != 1)
                    {
                        boards.Remove(b);
                        j--;
                    }
                    else
                    {
                        done = true;
                        break;
                    }
                }
            }

            if (done)
            {
                break;
            }
        }

        // get total of winning board
        int sum = 0;
        foreach (var val in boards.First())
        {
            if (!drawnNumbers.Contains(val))
                sum += val;
        }

        return sum * drawnNumbers.Last();
    }
}