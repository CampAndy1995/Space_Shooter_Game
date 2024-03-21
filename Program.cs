using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static int playerX = 10;
    static int playerY = 20;
    static List<int[]> enemies = new List<int[]>();
    static int score = 0;
    static bool gameOver = false;

    static void Main()
    {
        Console.CursorVisible = false;

        Thread inputThread = new Thread(ReadInput);
        inputThread.Start();

        Thread enemyThread = new Thread(SpawnEnemies);
        enemyThread.Start();

        while (!gameOver)
        {
            UpdateGame();
            Render();
            Thread.Sleep(100);
        }

        Console.Clear();
        Console.WriteLine("Game Over! Final Score: " + score);
    }

    static void ReadInput()
    {
        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    playerX--;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    playerX++;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    playerY--;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    playerY++;
                }
            }
        }
    }

    static void SpawnEnemies()
    {
        Random rand = new Random();
        while (!gameOver)
        {
            int[] enemy = { rand.Next(0, Console.WindowWidth), 0 };
            enemies.Add(enemy);
            Thread.Sleep(500);
        }
    }

    static void UpdateGame()
    {
        foreach (int[] enemy in enemies)
        {
            enemy[1]++;
            if (enemy[1] == playerY && enemy[0] == playerX)
            {
                gameOver = true;
            }
        }
        score += enemies.RemoveAll(enemy => enemy[1] >= Console.WindowHeight);
    }

    static void Render()
    {
        Console.Clear();

        Console.SetCursorPosition(playerX, playerY);
        Console.Write("^");

        foreach (int[] enemy in enemies)
        {
            Console.SetCursorPosition(enemy[0], enemy[1]);
            Console.Write("*");
        }

        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Score: " + score);
    }
}

