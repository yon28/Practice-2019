using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{
    public class ViewGame : View<Game>
    {
        List<ViewTank> arrViewTank = new List<ViewTank>();
        ViewKolobok viewKolobok;
        List<ViewWall> viewWall = new List<ViewWall>();
        List<ViewApple> viewApple = new List<ViewApple>();
        List<ViewBullet> viewBullet = new List<ViewBullet>();
        KolobokController controller = new KolobokController();
        Panel panelMap;

        public ViewGame(Panel panelMap, Label lbScore)
        {
            this.panelMap = panelMap;
            panelMap.Controls.Add(lbScore);
        }

        private  KeyEventHandler keyPress;
        public virtual void OnKeyPress(Keys key)
        {
            if (keyPress != null)
                keyPress(viewKolobok.Model, new KeyEventArgs(key));
            if (key == Keys.Space)
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            ViewBullet viewBullettemp = new ViewBullet(panelMap);
            viewBullettemp.Model = Model.ArrBullet[Model.ArrBullet.Count - 1];
            viewBullettemp.Model.MapSize = new Point(panelMap.Width, panelMap.Height);
            viewBullettemp.Subscribe();

            viewBullet.Add(viewBullettemp);
        }

        // Подписка на событие нажатие клавиши
        public void SubscribeKeyPress()
        {
            this.keyPress += new KeyEventHandler(controller.OnKeyPress);
            controller.OnKeyPress(viewKolobok, new KeyEventArgs(Keys.Right));
        }

        // Отписка на событие нажатие клавиши
        public void UnsubscribeKeyPress()
        {
            this.keyPress -= new KeyEventHandler(controller.OnKeyPress);
        }

        // Обновить
        protected override void Update()
        {
            Refresh();
        }
        // Создание всех элементов игры,
        // связывание ссылок представлений объекта с реальными объектами игры

        private void Refresh()
        {
            arrViewTank = new List<ViewTank>();
            viewWall = new List<ViewWall>();
            viewApple = new List<ViewApple>();
            viewBullet = new List<ViewBullet>();
            viewKolobok = new ViewKolobok(panelMap);

            viewKolobok.Model = Model.Kolobok;
            viewKolobok.Model.MapSize = new Point(panelMap.Width, panelMap.Height);
            viewKolobok.Subscribe();

            for (int i = 0; i < Model.ArrTank.Count; i++)
            {
                ViewTank viewTanktemp = new ViewTank(panelMap);
                viewTanktemp.Model = Model.ArrTank[i];
                viewTanktemp.Model.MapSize = new Point(panelMap.Width, panelMap.Height);
                viewTanktemp.Subscribe();
                arrViewTank.Add(viewTanktemp);
            }

            for (int i = 0; i < Model.ArrWall.Count; i++)
            {
                ViewWall viewWalltemp = new ViewWall(panelMap);
                viewWalltemp.Model = Model.ArrWall[i];
                viewWalltemp.Model.MapSize = new Point(panelMap.Width, panelMap.Height);
                viewWall.Add(viewWalltemp);
            }

            for (int i = 0; i < Model.ArrApple.Count; i++)
            {
                ViewApple viewAppletemp = new ViewApple(panelMap);
                viewAppletemp.Model = Model.ArrApple[i];
                viewAppletemp.Model.MapSize = new Point(panelMap.Width, panelMap.Height);
                viewAppletemp.Subscribe();
                viewApple.Add(viewAppletemp);
            }

            for (int i = 0; i < Model.ArrBullet.Count; i++)
            {
                ViewBullet viewBullettemp = new ViewBullet(panelMap);
                viewBullettemp.Model = Model.ArrBullet[i];
                viewBullettemp.Model.MapSize = new Point(panelMap.Width, panelMap.Height);
                viewBullettemp.Subscribe();
                viewBullet.Add(viewBullettemp);
            }
        }
    }
}
