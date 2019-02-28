
using System.Drawing;

namespace Tanks
{
    public class Renderer
    {

        /*  Game game;
          Bitmap[] Kolobok;


          public Renderer(Game game)
          {
              this.game = game;
              InitializeImages();

          }

          protected void InitializeImages()
          {
              string path = "C:/Users/olga/Desktop/13 Tanks/Tank/Resources/";
              Kolobok = new Bitmap[1];
              Kolobok[0] = ResizeImage(new Bitmap(path + "U.png"), 20, 20);
          }

          public static Bitmap ResizeImage(Bitmap picture, int width, int height)
          {
              Bitmap resizedPicture = new Bitmap(width, height);
              using (Graphics graphics = Graphics.FromImage(resizedPicture))
              {
                  graphics.DrawImage(picture, 0, 0, width, height);
              }
              return resizedPicture;
          }

          private int cell = 0;
          private int frame = 0;

          public void Animate()
          {
              frame++;
              if (frame >= 6)
                  frame = 0;
              switch (frame)
              {
                  case 0: cell = 0; break;
                  default: cell = 0; break;
              }
          }

          public void PaintField(Graphics g)
          {
              using (Pen brownPen = new Pen(Color.Brown, 6.0F))
              {
                  g.FillRectangle(Brushes.Black, 0, 0, 520, 280);
                 foreach (Wall brick in game.Wall)
                  {
                        g.DrawImageUnscaled(brick, brick.Location.X, brick.Location.Y);
                  }
                  foreach (Tank bee in game.Tanks)
                  {
                       //   g.DrawImageUnscaled();
                  }
                  g.DrawImageUnscaled(Kolobok[cell],10,10); 
              }
          }*/
    }
}
