using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Tanks
{
    public class Game
    {
        public List<ReportLine> report = new List<ReportLine>();
        public void UpdateReport()
        {
            report.Clear();
            for (int i = 0; i < ArrApple.Count; i++)
            {
                report.Add(new ReportLine($"Apple {i}", ArrApple[i].Position));
            }
            for (int i = 0; i < ArrApple.Count; i++)
            {
                report.Add(new ReportLine($"Tank {i}", ArrTank[i].Position));
            }
            report.Add(new ReportLine($"Kolobok", Kolobok.Position));
        }

        public void Shoot()
        {
            Rectangle rect = new Rectangle(kolobok.Position.X, kolobok.Position.Y, (new Bullet()).Width, (new Bullet()).Height);
            var bullet = new Bullet(rect.Location);
            bullet.DirectionNow = kolobok.DirectionNow;
            arrBullet.Add(bullet);
            Thread thread = new Thread(new ThreadStart(bullet.Run));
            arrThread.Add(thread);
        }

        public Game()
        {
            lbScore.Text = "0";

            for (int i = 0; i < GameForm.countWall; i++)
            {
                Rectangle rect = new Rectangle();
                arrWall.Add(new Wall(rect.Location));
            }
            PlaceWalls();

            for (int i = 0; i < GameForm.countTank; i++)
            {
                Rectangle rect = new Rectangle();
                do
                {
                    rect = new Rectangle(GameForm.rand.Next(0, GameForm.x), GameForm.rand.Next(0, GameForm.y), (new Tank()).Width, (new Tank()).Height);
                } while (Collides(rect));

                arrTank.Add(new Tank(rect.Location));
                Thread thread = new Thread(new ThreadStart(arrTank[i].Run));
                arrThread.Add(thread);
            }

            for (int i = 0; i < GameForm.countApples; i++)
            {
                Rectangle rect = new Rectangle();
                do
                {
                    rect = new Rectangle(GameForm.rand.Next(0, GameForm.x), GameForm.rand.Next(0, GameForm.y), (new Apple()).Width, (new Apple()).Height);
                } while (Collides(rect));

                arrApple.Add(new Apple(rect.Location));
            }

            Rectangle rect2 = new Rectangle();
            do
            {
                rect2 = new Rectangle(GameForm.rand.Next(0, GameForm.x), GameForm.rand.Next(0, GameForm.y), (new Kolobok()).Width, (new Kolobok()).Height);
            } while (Collides(rect2));

            kolobok = new Kolobok(rect2.Location);

            kolobokThread = new Thread(new ThreadStart(Kolobok.Run));

            SubscribePos();
        }

        private void PlaceWalls()
        {
            int cur = 0;
            for (int i = 0; i < GameForm.map.Length; i++)
                for (int j = 0; j < GameForm.map[0].Length; j++)
                    if (GameForm.map[i][j] == '*')
                    {
                        arrWall[cur].Position = new Point(i * arrWall[cur].Width, j * arrWall[cur].Height);
                        cur++;
                    }
        }

        private bool Collides(Rectangle rect)
        {
            if (rect.Left < 0 || rect.Right >= GameForm.x || rect.Top < 0 || rect.Bottom >= GameForm.y)
                return true;
            for (int i = 0; i < arrTank.Count; i++)
            {
                if (arrTank[i].CollidesWith(rect)) return true;
            }
            for (int i = 0; i < arrWall.Count; i++)
            {
                if (arrWall[i].CollidesWith(rect)) return true;
            }
            for (int i = 0; i < arrApple.Count; i++)
            {
                if (arrApple[i].CollidesWith(rect)) return true;
            }
            if (kolobok != null && kolobok.CollidesWith(rect)) return true;

            return false;
        }

        public void SubscribePos()
        {
            ScoreChanged += OnScoreChanged;

            for (int i = 0; i < arrTank.Count; i++)
            {
                for (int j = 0; j < arrTank.Count; j++)
                {
                    if (i != j)
                    {
                        arrTank[i].CheckPosition += new EventHandler(arrTank[j].OnCheckPosition);
                    }
                }
            }
            for (int i = 0; i < arrTank.Count; i++)
            {
                for (int j = 0; j < arrWall.Count; j++)
                {
                    arrTank[i].CheckPosition += new EventHandler(arrWall[j].OnCheckPosition);
                }
            }
            for (int j = 0; j < arrWall.Count; j++)
            {
                kolobok.CheckPosition += new EventHandler(arrWall[j].OnCheckPosition);
            }
            for (int j = 0; j < arrTank.Count; j++)
            {
                kolobok.CheckPosition += new EventHandler(arrTank[j].OnCheckPosition);
            }
            for (int j = 0; j < arrTank.Count; j++)
            {
                arrTank[j].CheckPosition += new EventHandler(kolobok.OnCheckPosition);
            }

            for (int j = 0; j < arrApple.Count; j++)
            {
                kolobok.CheckPosition += new EventHandler(arrApple[j].OnCheckPosition);
            }

            kolobok.ReplaceNeeded += new EventHandler(kolobok_ReplaceNeeded);
            for (int j = 0; j < arrApple.Count; j++)
            {
                arrApple[j].ReplaceNeeded += new EventHandler(apple_ReplaceNeeded);
            }
        }

        //Обработчик события перемещение объекта в заданные координаты

        void apple_ReplaceNeeded(object sender, EventArgs e)
        {
            Rectangle rect2 = new Rectangle();
            do
            {
                rect2 = new Rectangle(GameForm.rand.Next(0, GameForm.x - (sender as Obj).Width - 2), GameForm.rand.Next(GameForm.y - (sender as Obj).Height - 2), (new Apple()).Width, (new Apple()).Height);
            } while (Collides(rect2));

            (sender as Apple).Position = rect2.Location;
        }

        // Обработчик события перемещение объекта в заданные координаты
        public void kolobok_ReplaceNeeded(object sender, EventArgs e)
        {
            Rectangle rect2 = new Rectangle();
            do
            {
                rect2 = new Rectangle(GameForm.rand.Next(0, GameForm.x - (sender as Obj).Width - 2), GameForm.rand.Next(GameForm.y - (sender as Obj).Height - 2), (new Kolobok()).Width, (new Kolobok()).Height);
            } while (Collides(rect2));
            kolobok.Position = rect2.Location; //несколько жизней

        }

        public void Start()
        {
            foreach (Thread thread in arrThread)
            {
                thread.Start();
            }
            kolobokThread.Start();
        }

        public void Dispose()
        {
            for (int i = 0; i < arrThread.Count; i++)
            {
                arrThread[i].Abort();
            }
            kolobokThread.Abort();
        }

        // Отписка каждого с каждым от событий пересечения
        private void UnSubscribePos()
        {
            for (int i = 0; i < arrTank.Count; i++)
            {
                for (int j = 0; j < arrTank.Count; j++)
                {
                    if (i != j)
                    {
                        arrTank[i].CheckPosition -= new EventHandler(arrTank[i].OnCheckPosition);
                    }
                }
            }

        }

        List<Thread> arrThread = new List<Thread>();
        private Thread kolobokThread;
        List<Tank> arrTank = new List<Tank>();
        List<Wall> arrWall = new List<Wall>();
        List<Apple> arrApple = new List<Apple>();
        List<Bullet> arrBullet = new List<Bullet>();
        Kolobok kolobok;

        public List<Wall> ArrWall
        {
            get { return arrWall; }
        }

        public Kolobok Kolobok
        {
            get { return kolobok; }
        }

        public List<Tank> ArrTank
        {
            get { return arrTank; }
        }

        public List<Apple> ArrApple
        {
            get { return arrApple; }
        }

        public List<Bullet> ArrBullet
        {
            get { return arrBullet; }
        }

        public Label lbScore = new Label();
        private int score = 0;
        public int Score
        {
            get { return score; }
            set
            {
                if (score != value)
                {
                    score = value;
                    if (ScoreChanged != null) ScoreChanged(this, EventArgs.Empty);
                }
            }
        }
        public event EventHandler ScoreChanged;

        public void OnScoreChanged(object sender, EventArgs e)
        {
            ScoreChangeEventArgs scoreArgs = e as ScoreChangeEventArgs;
            lbScore.Text = this.Score.ToString();
        }
    }
}
