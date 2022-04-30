using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TetrisConsole
{
    public class HighScoreManager
    {
        private readonly string highscoreFile;

        public HighScoreManager(string highscoreFile = "highscore.txt")
        {
            this.highscoreFile = highscoreFile;
            this.HighScore = this.GetHighScore();
        }
        public int HighScore { get; set; }

        public List<string> GetAllScore()
        {
            List<string> list = new List<string>();
            if (File.Exists(this.highscoreFile))
            {
                list = File.ReadAllLines(this.highscoreFile).ToList();
            }
            return list.TakeLast(10).ToList();
        }
        private int GetHighScore()
        {
            var highScore = 0;

            if (File.Exists(this.highscoreFile))
            {
                var allscore = File.ReadAllLines(this.highscoreFile);
                foreach (var item in allscore)
                {
                    var match = Regex.Match(item, @" - (?<score>[0-9]+)");
                    highScore = Math.Max(highScore, int.Parse(match.Groups["score"].Value));
                }
            }
            return highScore;
        }
        public void Add(int score)
        {
            string newName = "";
            Drawer.Write("New HighScore", 2, 1, ConsoleColor.White);
            Drawer.Write("Insert name:", 3, 1, ConsoleColor.White);
            newName = Console.ReadLine();
            File.AppendAllLines(this.highscoreFile, new List<string>
            { $"{newName} => {score}"});
        }
    }
}
