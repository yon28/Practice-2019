using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Tanks
{
    public class Game
    {
        public List<Thread> arrThread = new List<Thread>();
        private Thread kolobokThread;
        public List<ReportLine> report = new List<ReportLine>();
        public List<Bullet> arrBullet = new List<Bullet>();
        List<Tank> arrTank = new List<Tank>();
        List<Wall> arrWall = new List<Wall>();
        List<Apple> arrApple = new List<Apple>();
        List<Water> arrWater = new List<Water>();
        Kolobok kolobok;

        public List<Wall> ArrWall { get => arrWall; } 
        public Kolobok Kolobok { get  =>  kolobok; } 
        public List<Tank> ArrTank { get  =>  arrTank; } 
        public List<Apple> ArrApple { get  =>  arrApple; } 
        public List<Bullet> ArrBullet { get  =>  arrBullet; } 
        public List<Water> ArrWater { get => arrWater; }

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
            var scoreArgs = e as ScoreChangeEventArgs;
            lbScore.Text = Score.ToString();
        }

        public void UpdateReport()
        {
            report.Clear();
            report.Add(new ReportLine($"Kolobok", Kolobok.Position));
            for (int i = 0; i < ArrTank.Count; i++)
            {
                var line = new ReportLine($"Tank {i}", ArrTank[i].Position);
                if (!(line.PointX == -30 || line.PointY == -30)) { report.Add(line); }
            }
            for (int i = 0; i < ArrApple.Count; i++)
            {
                var line = new ReportLine($"Apple {i}", ArrApple[i].Position);
                if (!(line.PointX == -30 || line.PointY == -30)) { report.Add(line); }
            }
            if (ArrBullet != null)
            {
                for (int i = 0; i < ArrBullet.Count; i++)
                {
                    var line = new ReportLine($"Bullet {i}", ArrBullet[i].Position);
                    if (!(line.PointX == -30 || line.PointY == -30)) { report.Add(line); }
                }
            }
        }

        public Game()
        {
            lbScore.Text = "0";

            for (int i = 0; i < GameForm.CountWall; i++)
            {
                Rectangle rect = new Rectangle();
                arrWall.Add(new Wall(rect.Location));
            }

            for (int i = 0; i < GameForm.CountWater; i++)
            {
                Rectangle rect = new Rectangle();
                arrWater.Add(new Water(rect.Location));
            }
            PlaceWallsAndWater();

            for (int i = 0; i < GameForm.CountTank; i++)
            {
                Rectangle rect = new Rectangle();
                do
                {
                    rect = new Rectangle(GameForm.rand.Next(0, GameForm.X), GameForm.rand.Next(0, GameForm.Y / 2), (new Tank()).Width, (new Tank()).Height);
                } while (Collides(rect));
                arrTank.Add(new Tank(rect.Location));
                Thread thread = new Thread(new ThreadStart(arrTank[i].Run));
                arrThread.Add(thread);
            }

            Rectangle rect2 = new Rectangle();
            do
            {
                rect2 = new Rectangle(GameForm.rand.Next(0, GameForm.X), GameForm.rand.Next(GameForm.Y * 3 / 4, GameForm.Y), (new Kolobok()).Width, (new Kolobok()).Height);
            } while (Collides(rect2));
            kolobok = new Kolobok(rect2.Location);
            kolobokThread = new Thread(new ThreadStart(Kolobok.Run));

            for (int i = 0; i < GameForm.CountApples; i++)
            {
                Rectangle rect = new Rectangle();
                do
                {
                    rect = new Rectangle(GameForm.rand.Next(0, GameForm.X), GameForm.rand.Next(0, GameForm.Y), (new Apple()).Width, (new Apple()).Height);
                } while (Collides(rect));
                arrApple.Add(new Apple(rect.Location));
            }
            SubscribePos();
        }

        private void PlaceWallsAndWater()
        {
            int cur = 0;
            int cur2 = 0;
            for (int i = 0; i < GameForm.map.Length; i++)
            {
                for (int j = 0; j < GameForm.map[0].Length; j++)
                {
                    if (GameForm.map[i][j] == '*')
                    {
                        arrWall[cur].Position = new Point(i * arrWall[cur].Width, j * arrWall[cur].Height);
                        cur++;
                    }

                    if (GameForm.map[i][j] == '8')
                    {
                        arrWater[cur2].Position = new Point(i * arrWater[cur2].Width, j * arrWater[cur2].Height);
                        cur2++;
                    }
                }
            }
        }

        private bool Collides(Rectangle rect)
        {
            if (rect.Left < 0 || rect.Right >= GameForm.X || rect.Top < 0 || rect.Bottom >= GameForm.Y)
            {
                return true;
            } 
            for (int i = 0; i < arrTank.Count; i++)
            {
                if (arrTank[i].CollidesWith(rect)) { return true; }
            }
            for (int i = 0; i < arrWall.Count; i++)
            {
                if (arrWall[i].CollidesWith(rect)) { return true; }
            }
            for (int i = 0; i < arrWater.Count; i++)
            {
                if (arrWater[i].CollidesWith(rect)) { return true; }
            }
            for (int i = 0; i < arrApple.Count; i++)
            {
                if (arrApple[i].CollidesWith(rect)) { return true; }
            }
            if (kolobok != null && kolobok.CollidesWith(rect)) { return true; }

            return false;
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
            UnSubscribePosAll();
            for (int i = 0; i < arrThread.Count; i++)
            {
                arrThread[i].Abort();
            }
            kolobokThread.Abort();
            GameForm.reportForm.reportThread.Abort();
        }

        public void SubscribeBulletPos(Bullet bullet)
        {
            if (bullet is BulletK)
            {
                for (int j = 0; j < arrTank.Count; j++)
                {
                    bullet.CheckPosition += new EventHandler(arrTank[j].OnCheckPosition);
                }
            }
            else
            {
                bullet.CheckPosition += new EventHandler(Kolobok.OnCheckPosition);
            }
            for (int j = 0; j < arrWall.Count; j++)
            {
                bullet.CheckPosition += new EventHandler(arrWall[j].OnCheckPosition);
            }
        }

        public void SubscribePos()
        {
            ScoreChanged += OnScoreChanged;
            //Танки
            for (int i = 0; i < arrTank.Count; i++)
            {
                for (int j = 0; j < arrTank.Count; j++)
                {
                    if (i != j)
                    {
                        arrTank[i].CheckPosition += new EventHandler(arrTank[j].OnCheckPosition);
                    }
                }
                //Танк со стеной
                for (int j = 0; j < arrWall.Count; j++)
                {
                    arrTank[i].CheckPosition += new EventHandler(arrWall[j].OnCheckPosition);
                }
                //Танк  с водой
                for (int j = 0; j < arrWater.Count; j++)
                {
                    arrTank[i].CheckPosition += new EventHandler(arrWater[j].OnCheckPosition);
                }
                //Танк с колобком
                arrTank[i].CheckPosition += new EventHandler(kolobok.OnCheckPosition);
            }

            //Колобок со стеной
            for (int j = 0; j < arrWall.Count; j++)
            {
                kolobok.CheckPosition += new EventHandler(arrWall[j].OnCheckPosition);
            }
            //Колобок с водой
            for (int j = 0; j < arrWater.Count; j++)
            {
                kolobok.CheckPosition += new EventHandler(arrWater[j].OnCheckPosition);
            }
            //Колобок с танком
            for (int j = 0; j < arrTank.Count; j++)
            {
                kolobok.CheckPosition += new EventHandler(arrTank[j].OnCheckPosition);
            }
            //Колобок с яблоком
            for (int j = 0; j < arrApple.Count; j++)
            {
                kolobok.CheckPosition += new EventHandler(arrApple[j].OnCheckPosition);
            }

            //
            kolobok.ReplaceNeeded += new EventHandler(kolobok_ReplaceNeeded);
            for (int j = 0; j < arrApple.Count; j++)
            {
                arrApple[j].ReplaceNeeded += new EventHandler(apple_ReplaceNeeded);
            }
            for (int j = 0; j < arrWall.Count; j++)
            {
                arrWall[j].ReplaceNeeded += new EventHandler(wall_ReplaceNeeded);
            }
        }

        //Обработчик события перемещение объекта в заданные координаты
        void wall_ReplaceNeeded(object sender, EventArgs e)
        {
            Rectangle rect2 = new Rectangle();
            rect2 = new Rectangle(-40/*-new Wall().Height*/, 0, new Wall().Height, new Wall().Height);
            (sender as Wall).Position = rect2.Location;
        }

        //Обработчик события перемещение объекта в заданные координаты
        void apple_ReplaceNeeded(object sender, EventArgs e)
        {
            Rectangle rect2 = new Rectangle();
            do
            {
                rect2 = new Rectangle(GameForm.rand.Next(0, GameForm.X - (sender as Obj).Width - 2), GameForm.rand.Next(GameForm.Y - (sender as Obj).Height - 2), (new Apple()).Width, (new Apple()).Height);
            } while (Collides(rect2));

            (sender as Apple).Position = rect2.Location;
        }

        // Обработчик события перемещение объекта в заданные координаты
        public void kolobok_ReplaceNeeded(object sender, EventArgs e)
        {
            Rectangle rect2 = new Rectangle();
            do
            {
                rect2 = new Rectangle(GameForm.rand.Next(0, GameForm.X - (sender as Obj).Width - 2), GameForm.rand.Next(GameForm.Y - (sender as Obj).Height - 2), (new Kolobok()).Width, (new Kolobok()).Height);
            } while (Collides(rect2));
            kolobok.Position = rect2.Location; //несколько жизней
        }

        // Отписка каждого с каждым от событий пересечения
        public void UnSubscribePosAll()
        {
            for (int i = 0; i < arrTank.Count; i++)
            {
                UnSubscribePos(arrTank[i]);
            }
            for (int i = 0; i < arrBullet.Count; i++)
            {
                UnSubscribePos(arrBullet[i]);
            }
            for (int i = 0; i < arrApple.Count; i++)
            {
                UnSubscribePos(arrApple[i]);
            }
            for (int i = 0; i < arrWall.Count; i++)
            {
                UnSubscribePos(arrWall[i]);
            }
        }
        // Отписка одного от событий пересечения
        public void UnSubscribePos(Obj obj)       /*view отписать*/
        {
            if (obj is Tank)
            {//Танки
                for (int j = 0; j < arrTank.Count; j++)
                {
                    if (obj != arrTank[j])/**/
                    {
                        ((Dynamic)obj).CheckPosition -= new EventHandler(arrTank[j].OnCheckPosition);
                    }
                }
                //Танк со стеной
                for (int j = 0; j < arrWall.Count; j++)
                {
                    ((Dynamic)obj).CheckPosition -= new EventHandler(arrWall[j].OnCheckPosition);
                }
                //Танк  с водой
                for (int j = 0; j < arrWater.Count; j++)
                {
                    ((Dynamic)obj).CheckPosition -= new EventHandler(arrWater[j].OnCheckPosition);
                }
                //Танк с колобком
                ((Dynamic)obj).CheckPosition -= new EventHandler(kolobok.OnCheckPosition);
            }

            if (obj is Kolobok)
            {
                //Колобок со стеной
                for (int j = 0; j < arrWall.Count; j++)
                {
                    kolobok.CheckPosition -= new EventHandler(arrWall[j].OnCheckPosition);
                }
                //Колобок с водой
                for (int j = 0; j < arrWater.Count; j++)
                {
                    kolobok.CheckPosition -= new EventHandler(arrWater[j].OnCheckPosition);
                }
                //Колобок с танком
                for (int j = 0; j < arrTank.Count; j++)
                {
                    kolobok.CheckPosition -= new EventHandler(arrTank[j].OnCheckPosition);
                }
                //Колобок с яблоком
                for (int j = 0; j < arrApple.Count; j++)
                {
                    kolobok.CheckPosition -= new EventHandler(arrApple[j].OnCheckPosition);
                }
                kolobok.ReplaceNeeded -= new EventHandler(kolobok_ReplaceNeeded);
            }

            if (obj is Bullet)
            {
                if (obj is BulletK)
                {
                    for (int j = 0; j < arrTank.Count; j++)
                    {
                        ((Dynamic)obj).CheckPosition -= new EventHandler(arrTank[j].OnCheckPosition);
                    }
                }
                else
                {
                    ((Dynamic)obj).CheckPosition -= new EventHandler(Kolobok.OnCheckPosition);
                }

                for (int j = 0; j < arrWall.Count; j++)
                {
                    ((Dynamic)obj).CheckPosition -= new EventHandler(arrWall[j].OnCheckPosition);
                }
            }
            if (obj is Apple)
            {
                obj.ReplaceNeeded -= new EventHandler(apple_ReplaceNeeded);
            }
            if (obj is Wall)
            {
                obj.ReplaceNeeded -= new EventHandler(wall_ReplaceNeeded);
            }
        }
    }
}
