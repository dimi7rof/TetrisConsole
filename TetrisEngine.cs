using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TetrisConsole
{
    public class TetrisEngine
    {
        public int TetrisRows { get; set; }
        public int TetrisCols { get; set; }
        public bool ShowNextFigure { get; set; }
        static bool[,] currentFigure = null;
        static bool[,] nextFigure = null;
        static Random random = new Random();
        static HighScoreManager highScoreManager = new HighScoreManager();
        static List<bool[,]> figures = new List<bool[,]>()
        {
            //I O T S Z J L
            new bool[,] //I
            {
                {true, true, true, true }
            },
            new bool[,]  //O
            {
                { true, true},
                { true, true}
            },
            new bool[,] //T
            {
                { false, true, false},
                { true, true, true}
            },
            new bool[,]  // S
            {
                {false, true, true },
                {true, true, false }
            },
            new bool[,] // Z
            {
                {true, true, false },
                {false, true,true }
            },
            new bool[,] //J
                        {
                 {true, false,false },
                 {true, true, true}
                        },
            new bool[,] //L
            {
                {false, false, true },
                {true,true,true }
            }
        };
        public TetrisEngine(int tetrisRows, int tetrisCols, bool showN)
        {
            TetrisRows = tetrisRows;
            TetrisCols = tetrisCols;
            ShowNextFigure = showN;
        }
        public void Run(Settings settings)
        {
            Console.Title = "ToDiTetris";
            Console.WindowHeight = settings.ConsoleRows;
            Console.WindowWidth = settings.ConsoleCols;
            Console.BufferHeight = settings.ConsoleRows;
            Console.BufferWidth = settings.ConsoleCols;
            Console.CursorVisible = false;
            nextFigure = figures[random.Next(0, figures.Count)];
            currentFigure = figures[random.Next(0, figures.Count)];
            while (true)
            {
                settings.Frame++;
                settings.UpdateLEvel();
                PressingKey(settings);
                if (settings.Frame % (settings.FramesToMove - settings.Level) == 0)
                {
                    settings.Frame = 0;
                    settings.CurrentFigureRow++;
                }
                Drawer.DrawBorder(settings);
                Drawer.DrawInfo(settings, highScoreManager.HighScore, ShowNextFigure, nextFigure);
                Drawer.DrawField(settings);
                if (Colosion(currentFigure, settings))
                {
                    AddCurrentFigureToField(settings);
                    int lines = CheckForFullLines(settings);
                    settings.Score += settings.ScorePerLines[lines] * settings.Level;
                    if (ShowNextFigure)
                    {
                        currentFigure = nextFigure;
                        nextFigure = figures[random.Next(0, figures.Count)];
                    }
                    else currentFigure = figures[random.Next(0, figures.Count)];
                    settings.CurrentFigureCol = settings.TetrisCols / 2 - 1;
                    settings.CurrentFigureRow = 0;
                    if (Colosion(currentFigure, settings))
                    {
                        if (settings.Score > highScoreManager.HighScore)
                        {
                            highScoreManager.Add(settings.Score);
                        }
                        Drawer.GameOver(settings.Score, settings);
                        Thread.Sleep(2000);
                        break;
                    }
                }
                Drawer.DrawCurrentFigure(currentFigure, settings);
                Thread.Sleep(40);
            }
        }
        private static void PressingKey(Settings settings)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                {
                    RotateCurrentFigure(settings);
                }
                if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                {
                    if (settings.CurrentFigureCol >= 1) // && !settings.Field[settings.CurrentFigureRow, settings.CurrentFigureCol -1]
                    {
                        settings.CurrentFigureCol--;
                    }
                }
                if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    if (settings.CurrentFigureCol < settings.TetrisCols - currentFigure.GetLength(1))
                    {
                        settings.CurrentFigureCol++;
                    }
                }
                if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    settings.CurrentFigureRow++;
                    settings.Score += settings.Level;
                    settings.Frame = 1;
                }
                if (key.Key == ConsoleKey.Escape) Environment.Exit(0);
                if (key.Key == ConsoleKey.M) MusicPlayer.Play();
            }
        }
        private static void RotateCurrentFigure(Settings settings)
        {
            bool[,] newFigure = new bool[currentFigure.GetLength(1), currentFigure.GetLength(0)];
            for (int row = 0; row < currentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < currentFigure.GetLength(1); col++)
                {
                    newFigure[col, currentFigure.GetLength(0) - row - 1] = currentFigure[row, col];
                }
            }
            if (!Colosion(newFigure, settings))
            {
                currentFigure = newFigure;
            }
        }
        private static int CheckForFullLines(Settings settings)
        {
            int lines = 0;
            for (int row = 0; row < settings.Field.GetLength(0); row++)
            {
                bool rowIsFull = true;
                for (int col = 0; col < settings.Field.GetLength(1); col++)
                {
                    if (settings.Field[row, col] == false)
                    {
                        rowIsFull = false;
                        break;
                    }
                }
                if (rowIsFull)
                {
                    for (int rowToMove = row; rowToMove >= 1; rowToMove--)
                    {
                        for (int col = 0; col < settings.Field.GetLength(1); col++)
                        {
                            settings.Field[rowToMove, col] = settings.Field[rowToMove - 1, col];
                        }
                    }
                    lines++;
                    Drawer.DrawField(settings, row);
                    Thread.Sleep(40);
                }
            }
            return lines;
        }
        static bool Colosion(bool[,] figure, Settings settings)
        {
            if (settings.CurrentFigureCol > settings.TetrisCols - figure.GetLength(1))
            {
                return true;
            }
            if (settings.CurrentFigureRow + figure.GetLength(0) == settings.TetrisRows)
            {
                return true;
            }

            for (int row = 0; row < figure.GetLength(0); row++)
            {
                for (int col = 0; col < figure.GetLength(1); col++)
                {
                    if (figure[row, col] && settings.Field[row + settings.CurrentFigureRow + 1, col + settings.CurrentFigureCol])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static void AddCurrentFigureToField(Settings settings)
        {
            for (int row = 0; row < currentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < currentFigure.GetLength(1); col++)
                {
                    if (currentFigure[row, col])
                    {
                        settings.Field[settings.CurrentFigureRow + row, settings.CurrentFigureCol + col] = true;

                    }
                }
            }
        }
    }
}
