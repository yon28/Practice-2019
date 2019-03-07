using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Tanks
{
    public partial class GameForm : Form
    {
        public static string[] map = {
            "............**............",
            "............**............",
            "..********......********..",
            "..********......********..",
            "............**............",
            "............**............",
            "..********..**..********..",
            "..********..**..********..",
            "..........................",
            "..........................",
            "..******..**..******..****",
            "..******..**..******..****",
            "................**........",
            "................**........",
            "..******..**..******..****",
            "..******..**..******..****",
            "..........................",
            "..........................",
            "..********..**..********..",
            "..********..**..********..",
            "............**............",
            "............**............",
            "..********......********..",
            "..********......********..",
            "............**............",
            "............**............"
        };
        public static Random rand = new Random();
        public static ViewGame viewGame;
        public static Game game;
        public static ReportForm reportForm = new ReportForm();
        public static int CountWall = 0;

        public static int Speed
        {
            get;
            private set;
        }
        public static int X
        {
            get;
            private set;
        }
        public static int Y
        {
            get;
            private set;
        }
        public static int CountTank
        {
            get;
            private set;
        }
        public static int CountApples
        {
            get;
            private set;
        }

        public GameForm(int x_ = 520, int y_ = 520, int countTank_ = 5, int countApples_ = 5, int speed_ = 4)
        {
            X = x_;
            Y = y_;
            Speed = speed_;
            CountTank = countTank_;
            CountApples = countApples_;
            InitializeComponent();
            //Расстановка стен по массиву map
            for (int i = 0; i < map.Length; i++)
                for (int j = 0; j < map[0].Length; j++)
                    CountWall += map[i][j] == '*' ? 1 : 0;
            game = new Game();
            viewGame = new ViewGame(p_Map, lbScore);
            viewGame.SubscribeKeyPress();
            viewGame.Model = game;
            timer1.Interval = 500; //миллисекунд
            timer1.Tick += new EventHandler(RunFrame);
            timer1.Enabled = true;
            UpdateScore();
        }

        private void RunFrame(object sender, EventArgs e)
        {
            UpdateScore();
            game.UpdateReport();
            reportForm.UpdateReport();
            if (game.Kolobok.Position.X == -30)
            {
                lbGameOver.Visible = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            reportForm.Show(this);
            MoveChildForms();
            btnStart.Visible = false;
            viewGame.Model.Start();
            this.Activate();
        }

        private void UpdateScore()
        {
            if (lbScore != null)
            {
                lbScore.Text = game?.lbScore.Text;
            }
        }

        private void GameForm_Move(object sender, EventArgs e)
        {
            MoveChildForms();
        }

        private void MoveChildForms()
        {
            reportForm.Location = new Point(Location.X + Width + 10, Location.Y);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            viewGame.OnKeyPress(e.KeyCode);
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            viewGame.Model.Dispose();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        public void GameOver()
        {
            Application.Exit();
        }

    }
}