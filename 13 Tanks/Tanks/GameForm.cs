using System;
using System.Windows.Forms;

namespace Tanks
{
    public partial class GameForm : Form
    {
        public static Random rand = new Random();
        private readonly ViewGame viewGame;

        public static string[] map = {
            "......*......",
            ".****...****.",
            "......*......",
            ".****.*.****.",
            ".............",
            ".***.*.***.**",
            "........*....",
            ".***.*.***.**",
            ".............",
            ".****.*.****.",
            "......*......",
            ".****...****.",
            "......*......"
                              };

        public GameForm( int x_ ,  int y_ ,  int countTank_, int countApples_ )
        {
            x = x_;
            y = y_;
            countTank = countTank_;
            countApples = countApples_;
            InitializeComponent();
            //Расстановка стен по массиву map
            for (int i = 0; i < map.Length; i++)
                for (int j = 0; j < map[0].Length; j++)
                    countWall += map[i][j] == '*' ? 1 : 0;
            p_Map.Controls.Add(l_points);
            Game game = new Game();
            viewGame = new ViewGame(p_Map, l_points);
            viewGame.SubscribeKeyPress();
            viewGame.Model = game;
            viewGame.Model.Start();
        }

        public static int x
        {
            get;
            private set;
        }
        public static int y
        {
            get;
            private set;
        }
        public static int countTank
        {
            get;
            private set;
        }
        public static int countApples
        {
            get;
            private set;
        }

        public static int countWall = 0;

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            viewGame.OnKeyPress(e.KeyCode);
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
             viewGame.Model.Dispose();
        }

        // viewGame.Model.Dispose();


    }
}