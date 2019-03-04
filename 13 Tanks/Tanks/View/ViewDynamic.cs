using System;
using System.Drawing;
using System.Windows.Forms;
namespace Tanks
{
    // Представление движущегося объекта
    public class ViewDynamic : View<Dynamic>
    {
        protected PictureBox pictBox = new PictureBox();
        public ViewDynamic(Panel map)
        {
            map.Controls.Add(pictBox);
            pictBox.Height = Model.Height;
            pictBox.Width = Model.Width;
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
           if( this.Model!=null) Show();
        }

        // Нарисовать картинку объекта
        protected void Show()
        {
            SetImage(Model.Position);
        }
        delegate void SetImageCallback(Point p);

        // Подвинуть картинку элемента в соответствии с его координатами
        private void SetImage(Point p)
        {
            if (this.pictBox.InvokeRequired)
            {
                SetImageCallback d = new SetImageCallback(SetImage);
                pictBox.Invoke(d, new object[] { Model.Position });
            }
            else
            {
                ChangePicture();
                this.pictBox.Location = p;
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
