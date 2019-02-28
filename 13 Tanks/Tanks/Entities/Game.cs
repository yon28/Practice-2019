using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Tanks
{

    class Game
    {
        List<Tank> arrTank = new List<Tank>();
        List<Wall> arrWall = new List<Wall>();
        List<Apple> arrApple = new List<Apple>();
        Kolobok packMan;

        public List<Apple> ArrApple
        {
            get { return arrApple; }
        }

        public List<Wall> ArrWall
        {
            get { return arrWall; }
        }

        public Kolobok Kolobok
        {
            get { return packMan; }
        }

        public List<Tank> ArrTank
        {
            get { return arrTank; }
        }


        List<Thread> arrThread = new List<Thread>();

        private Thread packManThread;

        private static int score = 0;

        public static int Score
        {
            get { return Game.score; }
            set { Game.score = value; }
        }


        public Game()
        {

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

            packMan = new Kolobok(rect2.Location);

            packManThread = new Thread(new ThreadStart(Kolobok.Run));
            SubscribePos();
        }

        public void Start()
        {

            foreach (Thread thread in arrThread)
            {
                thread.Start();
            }
            packManThread.Start();
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
            if (packMan != null && packMan.CollidesWith(rect)) return true;

            return false;
        }

        public void Dispose()
        {
            for (int i = 0; i < arrThread.Count; i++)
            {
                arrThread[i].Abort();
            }
            packManThread.Abort();
        }

        public void SubscribePos()
        {
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
                packMan.CheckPosition += new EventHandler(arrWall[j].OnCheckPosition);
            }
            for (int j = 0; j < arrTank.Count; j++)
            {
                packMan.CheckPosition += new EventHandler(arrTank[j].OnCheckPosition);
            }
            for (int j = 0; j < arrTank.Count; j++)
            {
                arrTank[j].CheckPosition += new EventHandler(packMan.OnCheckPosition);
            }
            for (int j = 0; j < arrApple.Count; j++)
            {
                packMan.CheckPosition += new EventHandler(arrApple[j].OnCheckPosition);
            }

            packMan.ReplaceNeeded += new EventHandler(packMan_ReplaceNeeded);
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

        void packMan_ReplaceNeeded(object sender, EventArgs e)
        {
            Rectangle rect2 = new Rectangle();
            do
            {
                rect2 = new Rectangle(GameForm.rand.Next(0, GameForm.x - (sender as Obj).Width - 2), GameForm.rand.Next(GameForm.y - (sender as Obj).Height - 2), (new Kolobok()).Width, (new Kolobok()).Height);
            } while (Collides(rect2));

            packMan.Position = rect2.Location;
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
