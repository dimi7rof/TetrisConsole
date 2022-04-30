using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TetrisConsole;

namespace TetrisConsole
{
    public class StartScreen
    {
        public int Height { get; set; }
        public int NewHeight { get; set; }
        public int Widgh { get; set; }
        public int NewWidgh { get; set; }
        public string showNext = "Yes";
        public bool showN = true;
        public StartScreen()
        {
            Height = 20;
            Widgh = 10;
            NewHeight = 20;
            NewWidgh = 10;
        }
        public void Run()
        {
            Console.Title = "ToDiTetris";
            Console.WindowHeight = 32;
            Console.WindowWidth = 32;
            Console.BufferHeight = 32;
            Console.BufferWidth = 32;
            Console.CursorVisible = false;
            Settings settings = new Settings(Height, Widgh);
            int pointer = 5;
            while (pointer < 50)
            {
                if (pointer >= 12)
                {
                    pointer = 5;
                }
                else if (pointer <= 4)
                {
                    pointer = 11;
                }
                DrawBorderStart();
                Drawer.Write("Play", 5, 12, ConsoleColor.White);
                Drawer.Write("Highscore", 7, 12, ConsoleColor.White);
                Drawer.Write("Settings", 9, 12, ConsoleColor.White);
                Drawer.Write("Exit", 11, 12, ConsoleColor.White);
                Drawer.Write("=>", pointer, 9, ConsoleColor.White);
                pointer = PressKey(pointer);
                Thread.Sleep(100);
                Console.Clear();
                if (pointer == 17) // highscore
                {
                    pointer = RunHighScore(pointer);
                }
                if (pointer == 19) // Settings
                {
                    pointer = RunSettings();
                }
                if (pointer == 21) // Exit
                {
                    Environment.Exit(0);
                }
            }
        }
        private int RunSettings()
        {
            int set = 5;
            while (set != 11)
            {
                DrawBorderStart();
                Drawer.Write($"Console Height:", 5, 6, ConsoleColor.White);
                Drawer.Write($"{NewHeight}", 5, 22, ConsoleColor.Red);
                Drawer.Write($"Console Widgh:", 7, 6, ConsoleColor.White);
                Drawer.Write($"{NewWidgh}", 7, 22, ConsoleColor.Red);
                Drawer.Write($"Show next:", 9, 6, ConsoleColor.White);
                Drawer.Write($"{showNext}", 9, 22, ConsoleColor.Red);
                Drawer.Write("=>", set, 2, ConsoleColor.White);
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (set == 5) NewHeight--;
                        if (set == 7) NewWidgh--;
                        if (set == 9) ChangeNextState();
                    }
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        if (set == 5 )  NewHeight++;
                        if (set == 7)  NewWidgh++;
                        if (set == 9) ChangeNextState();
                    }
                    if (key.Key == ConsoleKey.Enter)  set += 2;
                }
                Thread.Sleep(100);
                Console.Clear();
            }
            return 5;
        }
        private void ChangeNextState()
        {
            if (showN)
            {
                showNext = "No";
                showN = false;
            }
            else
            {
                showNext = "Yes";
                showN = true;
            }
        }
        private int ChangeHeight(int set)
        {
            int temp = 5;
            while (temp != 20)
            {
                DrawBorderStart();
                Drawer.Write($"Console Height:", 5, 6, ConsoleColor.White);
                Drawer.Write($"{NewHeight}", 5, 22, ConsoleColor.Red);
                Drawer.Write($"Console Widgh:", 7, 6, ConsoleColor.White);
                Drawer.Write($"{NewWidgh}", 7, 22, ConsoleColor.Gray);
                Drawer.Write($"Show next:", 9, 6, ConsoleColor.White);
                Drawer.Write($"{showNext}", 9, 22, ConsoleColor.Gray);
                Drawer.Write($"back", 11, 6, ConsoleColor.White);
                Drawer.Write("=>", set, 2, ConsoleColor.White);
                Thread.Sleep(100);
                Console.Clear();
            }
            return 5;
        }
        private int RunHighScore(int pointer)
        {
            int intScore = 5;
            while (intScore <= 10)
            {
                DrawBorderStart();
                intScore = PressKey(intScore);
                HighScoreManager hsm = new HighScoreManager();
                List<string> list = hsm.GetAllScore().TakeLast(10).ToList();
                int index = 5;
                int place = 1;
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    Drawer.Write($"{place}: {list[i]}", index, 3);
                    index++;
                    place++;
                }
                Thread.Sleep(100);
                Console.Clear();
            }
            return 5;
        }
        private int GetHighScore()
        {
            var highScore = 0;
            if (File.Exists("highscore.txt"))
            {
                var allscore = File.ReadAllLines("highscore.txt");
                List<int> highList = new List<int>();
                Regex reg = new Regex(@" => (?<score>[0-9]+)");
                // var matches = reg.Match(allscore);
                // foreach (var item in allscore)
                // {
                //     var match = Regex.Match(item, @" => (?<score>[0-9]+)");
                //     highScore = Math.Max(highScore, int.Parse(match.Groups["score"].Value));
                // }
            }
            return highScore;
        }
        public int PressKey(int pointer)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.DownArrow)
                {
                    return pointer += 2;
                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    return pointer -= 2;
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    if (pointer == 5)
                    {
                        return 50;
                    }
                    if (pointer == 7)
                    {
                        return 17;
                    }
                    if (pointer == 9)
                    {
                        return 19;
                    }
                    if (pointer == 11)
                    {
                        return 21;
                    }
                }
            }
            return pointer;
        }
        private void DrawBorderStart()
        {
            string line = "╔";
            line += new string('═', 30);
            // line += "╦";
            // line += new string('═', settings.InfoCols);
            line += "╗";

            Console.WriteLine(line);
            for (int i = 0; i < 30 - 1; i++)
            {
                string middle = "║";
                middle += new string(' ', 30);
                // middle += "║";
                // middle += new string(' ', settings.InfoCols);
                middle += "║";
                Console.WriteLine(middle);
            }
            string line2 = "╚";
            line2 += new string('═', 30);
            // line2 += "╩";
            // line2 += new string('═', settings.InfoCols);
            line2 += "╝";
            Console.WriteLine(line2);

        }
    }
}
