using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{
    class ViewObj : View<Obj>
    {
        protected PictureBox pictBox = new PictureBox();

        public ViewObj(Panel map)
        {
            map.Controls.Add(pictBox);
            pictBox.Height = Model.Height;
            pictBox.Width = Model.Width;
        }

        public void Subscribe()
        {
            Model.PositionChanged += new EventHandler(OnPositionChanged);
            OnPositionChanged(this, new EventArgs());
        }

        protected void Unsubscribe()
        {
             Model.PositionChanged -= new EventHandler(OnPositionChanged);
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
        private void SetImage(Point p)
        {
            if (pictBox.InvokeRequired)
            {
                SetImageCallback d = new SetImageCallback(SetImage);
                pictBox.Invoke(d, new object[] { Model.Position });
            }
            else
            {
                ChangePicture();
                pictBox.Location = p;
            }
        }

        // Изменить картинку в соответтствии с его направлением
        protected virtual void ChangePicture() { }

        // Обновить
        protected override void Update()
        {
            Show();
        }
    }
}
