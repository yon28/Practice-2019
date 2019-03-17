using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{
    public class ViewGame : View<Game>
    {
        public List<ViewTank> arrViewTank;
        ViewKolobok viewKolobok;
        List<ViewWall> viewWall;
        List<ViewWater> viewWater;
        List<ViewApple> viewApple;
        public List<ViewBullet> viewBullet = new List<ViewBullet>();
        KolobokController kolobokcontroller = new KolobokController();
        public Panel panelMap;

        public ViewGame(Panel panelMap, Label lbScore)
        {
            this.panelMap = panelMap;
        }

        private KeyEventHandler keyPress;
        public void SubscribeKeyPress()
        {
            keyPress += new KeyEventHandler(kolobokcontroller.OnKeyPress);
            kolobokcontroller.OnKeyPress(viewKolobok, new KeyEventArgs(Keys.Right));
        }
        public virtual void OnKeyPress(Keys key)
        {
            if (keyPress != null)
                keyPress(viewKolobok.Model, new KeyEventArgs(key));
            if (key == Keys.Space)
            {
              
            }
        }

        public void ViewShoot(Dynamic dynamic)
        {
            if (Model.ArrBullet.Count != 0)
            {
                ViewBullet viewBullettemp = new ViewBullet(dynamic, panelMap);
                viewBullettemp.Model = Model.ArrBullet[Model.ArrBullet.Count - 1];
                viewBullettemp.Model.MapSize = new Point(panelMap.Width, panelMap.Height);
                viewBullettemp.Subscribe();
                viewBullet.Add(viewBullettemp);
            }
        }

        public void UnsubscribeKeyPress()
        {
             keyPress -= new KeyEventHandler(kolobokcontroller.OnKeyPress);
        }

        // Обновить
        // Создание и связывание ссылок представлений  всех элементов игры с реальными объектами 
        protected override void Update()
        {
            arrViewTank = new List<ViewTank>();
            viewWall = new List<ViewWall>();
            viewWater = new List<ViewWater>();
            viewApple = new List<ViewApple>();
            viewKolobok = new ViewKolobok(panelMap);
            for (int i = 0; i < Model.ArrWall.Count; i++)
            {
                ViewWall viewWalltemp = new ViewWall(panelMap);
                viewWalltemp.Model = Model.ArrWall[i];
                viewWalltemp.Model.MapSize = new Point(panelMap.Width, panelMap.Height);
                viewWalltemp.Subscribe();
                viewWall.Add(viewWalltemp);
            }
            for (int i = 0; i < Model.ArrWater.Count; i++)
            {
                ViewWater viewWatertemp = new ViewWater(panelMap);
                viewWatertemp.Model = Model.ArrWater[i];
                viewWatertemp.Model.MapSize = new Point(panelMap.Width, panelMap.Height);
                viewWatertemp.Subscribe();
                viewWater.Add(viewWatertemp);
            }
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
            for (int i = 0; i < Model.ArrApple.Count; i++)
            {
                ViewApple viewAppletemp = new ViewApple(panelMap);
                viewAppletemp.Model = Model.ArrApple[i];
                viewAppletemp.Model.MapSize = new Point(panelMap.Width, panelMap.Height);
                viewAppletemp.Subscribe();
                viewApple.Add(viewAppletemp);
            }
        }
    }
}
