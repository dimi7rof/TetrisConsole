using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using TetrisConsole;

namespace TetrisConsole
{
    public static class Program
    {
        
        public static void Main()
        {
            while (true)
            {
                StartScreen startScreen = new StartScreen();
                startScreen.Run();

                Settings settings = new Settings(startScreen.NewHeight, startScreen.NewWidgh);
                TetrisEngine tetrisEngine = new TetrisEngine(startScreen.NewHeight, startScreen.NewWidgh, startScreen.showN);
                tetrisEngine.Run(settings);
            }
        }
    }
}
