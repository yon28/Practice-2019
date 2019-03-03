using System.Drawing;

namespace Tanks
{
    public class ReportLine
    {
        public string Name { get; set; }
        public int PointX { get; set; }
        public  int PointY { get; set; }
       

        public ReportLine(string name,Point point)
        {
            this.Name = name;
            this.PointX = point.X;
            this.PointY = point.Y;
    
        }
    }
}

