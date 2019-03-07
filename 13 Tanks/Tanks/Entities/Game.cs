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
            var scoreArgs = e as ScoreChangeEventArgs;
            lbScore.Text = this.Score.ToString();
        }

        public void UpdateReport()
        {
            report.Clear();
            for (int i = 0; i < ArrApple.Count; i++)
            {
                report.Add(new ReportLine($"Apple {i}", ArrApple[i].Position));
            }
            for (int i = 0; i < ArrTank.Count; i++)
            {
                report.Add(new ReportLine($"Tank {i}", ArrTank[i].Position));
            }
            report.Add(new ReportLine($"Kolobok", Kolobok.Position));
            if (ArrBullet != null)
            {
                for (int i = 0; i < ArrBullet.Count; i++)
                {
                    report.Add(new ReportLine($"Bullet {i}", ArrBullet[i].Position));
                }
            }
        }

        public void Shoot()
        {
            Rectangle rect = new Rectangle(kolobok.Position.X, kolobok.Position.Y, (new BulletK()).Width, (new BulletK()).Height);
            var bullet = new BulletK(rect.Location);
            bullet.IdentifyDirection((int)kolobok.DirectionNow);//
            bullet.Name = "";
            BulletAdd(bullet);
        }

        public void ShootTank(Dynamic tank)
        {
            Rectangle rect = new Rectangle(tank.Position.X, tank.Position.Y, (new BulletT()).Width, (new BulletT()).Height);
            var bullet = new BulletT(rect.Location);
            bullet.IdentifyDirection((int)tank.DirectionNow);
            bullet.Name = DateTime.Now.ToString();/**/
            BulletAdd(bullet);
        }

        private void BulletAdd(Bullet bullet)
        {
            arrBullet.Add(bullet);
            SubscribeBulletPos(bullet);
            Thread thread = new Thread(new ThreadStart(bullet.Run));
            thread.Name = bullet.Name;
            arrThread.Add(thread);
            thread.Start();
        }

        public Game()
        {
            lbScore.Text = "0";

            for (int i = 0; i < GameForm.CountWall; i++)
            {
                Rectangle rect = new Rectangle();
                arrWall.Add(new Wall(rect.Location));
            }
            PlaceWalls();

            for (int i = 0; i < GameForm.CountTank; i++)
            {
                Rectangle rect = new Rectangle();
                do
                {
                    rect = new Rectangle(GameForm.rand.Next(0, GameForm.X), GameForm.rand.Next(0, GameForm.Y/2), (new Tank()).Width, (new Tank()).Height);
                } while (Collides(rect));
                arrTank.Add(new Tank(rect.Location));
                Thread thread = new Thread(new ThreadStart(arrTank[i].Run));
                arrThread.Add(thread);
            }

            Rectangle rect2 = new Rectangle();
            do
            {
                rect2 = new Rectangle(GameForm.rand.Next(0, GameForm.X), GameForm.rand.Next(GameForm.Y*3/4, GameForm.Y), (new Kolobok()).Width, (new Kolobok()).Height);
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
            if (rect.Left < 0 || rect.Right >= GameForm.X || rect.Top < 0 || rect.Bottom >= GameForm.Y)
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

        public void SubscribeBulletPos(Bullet bullet)
        {
            if (bullet is BulletK)
            {
                for (int j = 0; j < arrTank.Count; j++)
                {   bullet.CheckPosition += new EventHandler(arrTank[j].OnCheckPosition);
                    arrTank[j].CheckPosition += new EventHandler(bullet.OnCheckPosition);
                }
            }
            else
            {
                bullet.CheckPosition += new EventHandler(Kolobok.OnCheckPosition);
                Kolobok.CheckPosition += new EventHandler(bullet.OnCheckPosition);
            }

            for (int j = 0; j < arrWall.Count; j++)
            {
                bullet.CheckPosition += new EventHandler(arrWall[j].OnCheckPosition);
                arrWall[j].CheckPosition += new EventHandler(bullet.OnCheckPosition);
            }
        }

        public void UnSubscribeBulletPos(Bullet bullet)
        {
            if (bullet is BulletK)
            {
                for (int j = 0; j < arrTank.Count; j++)
                {
                  //  bullet.CheckPosition -= new EventHandler(arrTank[j].OnCheckPosition);
                    arrTank[j].CheckPosition -= new EventHandler(bullet.OnCheckPosition);
                }
            }
            else
            {
                bullet.CheckPosition -= new EventHandler(Kolobok.OnCheckPosition);
                Kolobok.CheckPosition -= new EventHandler(bullet.OnCheckPosition);
            }
            for (int j = 0; j < arrWall.Count; j++)
            {
                bullet.CheckPosition -= new EventHandler(arrWall[j].OnCheckPosition);
                arrWall[j].CheckPosition -= new EventHandler(bullet.OnCheckPosition);
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
            }
            //Танк со стеной
            for (int i = 0; i < arrTank.Count; i++)
            {
                for (int j = 0; j < arrWall.Count; j++)
                {
                    arrTank[i].CheckPosition += new EventHandler(arrWall[j].OnCheckPosition);
                }
            }
            //Колобок со стеной
            for (int j = 0; j < arrWall.Count; j++)
            {
                kolobok.CheckPosition += new EventHandler(arrWall[j].OnCheckPosition);
            }
            //Колобок с танком
            for (int j = 0; j < arrTank.Count; j++)
            {
                kolobok.CheckPosition += new EventHandler(arrTank[j].OnCheckPosition);
            }
            //Танк с колобком
            for (int j = 0; j < arrTank.Count; j++)
            {
                arrTank[j].CheckPosition += new EventHandler(kolobok.OnCheckPosition);
            }
            //Колобок с яблоком
            for (int j = 0; j < arrApple.Count; j++)
            {
                kolobok.CheckPosition += new EventHandler(arrApple[j].OnCheckPosition);
            }

            //
            kolobok.ReplaceNeeded += new EventHandler(kolobok_ReplaceNeeded);
            //
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
            //UnSubscribePos();
            //Dispose();
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

    }
}
