using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{
    class ViewObj : View<Obj>
    {
        protected PictureBox picBox = new PictureBox();

        public ViewObj(Panel map)
        {
            map.Controls.Add(picBox);
            picBox.Height = Model.Height;
            picBox.Width = Model.Width;
        }

        // Подписка на событие изменение координат
        public void Subscribe()
        {
            this.Model.PositionChanged += new EventHandler(OnPositionChanged);
            OnPositionChanged(this, new EventArgs());
        }

        // Отписка от события изменение координат
        private void Unsubscribe()
        {
            this.Model.PositionChanged -= new EventHandler(OnPositionChanged);
        }

        // Обработчик события изменение координат
        private void OnPositionChanged(object sender, EventArgs e)
        {
            Show();
        }

        // Нарисовать картинку объекта
        private void Show()
        {
            SetImage(Model.Position);
        }
        delegate void SetImageCallback(Point p);

        // Подвинуть картинку элемента в соответствии с его координатами

        // <param name="p"></param>
        private void SetImage(Point p)
        {
            if (this.picBox.InvokeRequired)
            {
                SetImageCallback d = new SetImageCallback(SetImage);
                picBox.Invoke(d, new object[] { Model.Position });
            }
            else
            {
                ChangePicture();
                this.picBox.Location = p;
            }
        }

        // Изменить картинку в соответтствии с его направлением

        protected virtual void ChangePicture() { }


        // Обновить.

        protected override void Update()
        {
            Show();
        }
    }
}
