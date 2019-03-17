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
            Name = name;
            PointX = point.X;
            PointY = point.Y;
    
        }
    }
}

