using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

using System.Windows.Forms;

namespace Tanks
{
    class ViewGame : View<Game>
    {

        List<ViewTank> arrViewTank = new List<ViewTank>();
        ViewKolobok viewKolobok;
        List<ViewWall> viewWall = new List<ViewWall>();
        List<ViewApple> viewApple = new List<ViewApple>();
        KolobokController controller = new KolobokController();
        Panel panelMap;
        Label point;

        public ViewGame(Panel panelMap, Label point)
        {
            this.panelMap = panelMap;
            this.point = point;
            panelMap.Controls.Add(point);
        }

        private event KeyEventHandler keyPress;
        public virtual void OnKeyPress(Keys key)
        {
            if (keyPress != null)
                keyPress(viewKolobok.Model, new KeyEventArgs(key));
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

        // Обновить.
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
        }
    }
}
