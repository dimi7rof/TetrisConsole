using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisConsole
{
  public static class Drawer
    {
        public static void DrawInfo( Settings settings, int score, bool showNext, bool[,] nextFigure)
        {
            Write("Level:", 1, settings.TetrisCols +3);
            Write(settings.Level.ToString(), 2, settings.TetrisCols + 3);
            Write("Score:", 4, settings.TetrisCols + 3);
            Write(settings.Score.ToString(), 5, settings.TetrisCols + 3);
            Write("Best:", 7, settings.TetrisCols + 3);
            Write(score.ToString(), 8, settings.TetrisCols + 3);
            if (showNext)
            {
                Write("Next:", 11, settings.TetrisCols + 3);
                for (int row = 0; row < nextFigure.GetLength(0) ; row++)
                {
                    for (int col = 0; col < nextFigure.GetLength(1); col++)
                    {
                        if (nextFigure[row, col])
                        {
                            Write("X", row + 13, col + settings.TetrisCols + 4);
                        }
                    }
                }
            }
            //Write("Position:", 10, 3 + tetrisCols);
            //Write($"{currentFigureRow}, {currentFigureCol}", 11, 3 + tetrisCols);
            //Write("Frame:", 17, 3 + tetrisCols);
            //Write(frame.ToString(), 18, 3 + tetrisCols);
        }
        public static void  Write(string text, int row = 0, int col = 0, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(col, row);
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void DrawBorder( Settings settings )
        {
            string line = "╔";
            line += new string('═', settings.TetrisCols);
            line += "╦";
            line += new string('═', settings.InfoCols);
            line += "╗";

            Write(line);
            for (int i = 0; i < settings.TetrisRows - 1; i++)
            {
                string middle = "║";
                middle += new string(' ', settings.TetrisCols);
                middle += "║";
                middle += new string(' ', settings.InfoCols);
                middle += "║";
                Console.WriteLine(middle);
            }
            string line2 = "╚";
            line2 += new string('═', settings.TetrisCols);
            line2 += "╩";
            line2 += new string('═', settings.InfoCols);
            line2 += "╝";
            Write(line2, settings.TetrisRows, 0);
        }
        public static void DrawCurrentFigure(bool[,] currentFigure, Settings settings)
        {
            for (int row = 0; row < currentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < currentFigure.GetLength(1); col++)
                {
                    if (currentFigure[row, col])
                    {
                        Write("X", row + 1 + settings.CurrentFigureRow, col +  1 + settings.CurrentFigureCol, ConsoleColor.Green);
                    }
                }
            }
        }
        public static void DrawField( Settings settings, int red = 0)
        {
            for (int row = 1; row < settings.Field.GetLength(0); row++)
            {
                string line = "";
                for (int col = 0; col < settings.Field.GetLength(1); col++)
                {
                    if (settings.Field[row, col])
                    {
                        line += "X";
                    }
                    else
                    {
                        line += " ";
                    }
                }
                if (row == red)
                {
                    Write(line, row, 1, ConsoleColor.Red);
                }
                else
                {
                    Write(line, row, 1);
                }

            }
        }
        public static void GameOver(int score, Settings settings)
        {
            var scoreAsStr = score.ToString();
            scoreAsStr = new string(' ', 7 - scoreAsStr.Length) + scoreAsStr + "   ";
            Write("╔══════════╗", 5, 5, ConsoleColor.Red);
            Write("║GAME  OVER║", 6, 5, ConsoleColor.Red);
            Write($"║{scoreAsStr}║", 7, 5, ConsoleColor.Red);
            Write("╚══════════╝", 8, 5, ConsoleColor.Red);
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }
        public static void PauseGame()
        {
            Write("╔══════════╗", 4, 5, ConsoleColor.Red);
            Write("║   Pause  ║", 5, 5, ConsoleColor.Red);
            Write("║          ║", 6, 5, ConsoleColor.Red);
            Write("║Press any ║ ", 7, 5, ConsoleColor.Red);
            Write("║   key to ║ ", 8, 5, ConsoleColor.Red);
            Write("║coutiniue ║ ", 9, 5, ConsoleColor.Red);
            Write("╚══════════╝", 10, 5, ConsoleColor.Red);
        }
    }
}
