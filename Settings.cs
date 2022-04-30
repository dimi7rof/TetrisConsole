using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisConsole
{
    public class Settings
    {
        public Settings(int tetrisRows, int tetrisCols)
        {
            this.TetrisRows = tetrisRows;
            this.TetrisCols = tetrisCols;
            this.InfoCols = 10;
            this.ConsoleRows = 2 + tetrisRows;
            this.ConsoleCols = 3 + InfoCols + tetrisCols;
            this.Score = 0;
            this.Frame = 1;
            this.FramesToMove = 16;
            this.CurrentFigureRow = 0;
            this.CurrentFigureCol = TetrisCols / 2 - 1;
            this.Level = 1;
            this.Field = new bool[tetrisRows, tetrisCols];
        }
        public int[] ScorePerLines = { 0, 40, 100, 300, 1200 };
        public int TetrisRows { get; private set; }
        public int TetrisCols { get; private set; }
        public int InfoCols { get; private set; }
        public int ConsoleRows { get; private set; }
        public int ConsoleCols { get; private set; }
        public int Score { get; set; } 
        public int Frame { get;  set; }
        public int FramesToMove { get; private set; }
        public int CurrentFigureRow { get;  set; }
        public int CurrentFigureCol { get;  set; }
        public int Level { get; private set; }
        
        public bool[,] Field { get; private set; }

        public void UpdateLEvel()
        {
            if (this.Score <= 0)
            {
                this.Level = 1;
                return;
            }
            this.Level = (int)Math.Log10(Score) - 1;
            if (this.Level < 1)
            {
                this.Level = 1;
            }
            if (this.Level > 10)
            {
                this.Level = 10;
            }
        }
    }
}
