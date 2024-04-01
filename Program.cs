using System;
using System.Linq;

class MinesweeperGame
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Minesweeper!");
        Console.WriteLine("Enter the size of the board (n x n): ");
        int size = int.Parse(Console.ReadLine());

        char[,] board = GenerateBoard(size);

        bool gameOver = false;

        while (!gameOver)
        {
            PrintBoard(board);

            Console.WriteLine("Enter row and column to open (e.g., 1 2): ");
            string[] input = Console.ReadLine().Split(' ');
            int row = int.Parse(input[0]) - 1;
            int col = int.Parse(input[1]) - 1;

            if (row < 0 || row >= size || col < 0 || col >= size)
            {
                Console.WriteLine("Invalid input. Please enter valid row and column.");
                continue;
            }

            if (board[row, col] == '*')
            {
                Console.WriteLine("Game Over! You hit a mine.");
                gameOver = true;
            }
            else
            {
                OpenCell(board, row, col);
                gameOver = CheckWin(board);
            }
        }

        Console.WriteLine("Game Over!");
    }

    static char[,] GenerateBoard(int size)
    {
        char[,] board = new char[size, size];
        Random rand = new Random();

        // Place mines randomly
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board[i, j] = rand.Next(10) == 0 ? '*' : '.';
            }
        }

        return board;
    }

    static void PrintBoard(char[,] board)
    {
        int size = board.GetLength(0);
        Console.WriteLine("  " + string.Join(" ", Enumerable.Range(1, size).Select(i => i.ToString())));

        for (int i = 0; i < size; i++)
        {
            Console.Write((i + 1) + " ");
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == '\0')
                    Console.Write(". ");
                else
                    Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void OpenCell(char[,] board, int row, int col)
    {
        if (board[row, col] != '.')
            return;

        int size = board.GetLength(0);
        int minesCount = 0;

        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                if (i >= 0 && i < size && j >= 0 && j < size && board[i, j] == '*')
                    minesCount++;
            }
        }

        if (minesCount > 0)
            board[row, col] = char.Parse(minesCount.ToString());
        else
        {
            board[row, col] = ' ';
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < size && j >= 0 && j < size && !(i == row && j == col))
                        OpenCell(board, i, j);
                }
            }
        }
    }

    static bool CheckWin(char[,] board)
    {
        int size = board.GetLength(0);

        foreach (char cell in board)
        {
            if (cell == '.')
                return false;
        }

        return true;
    }
}
