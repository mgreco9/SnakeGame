using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Util
{
    public class FigureMaker
    {
        public static Rectangle MakePoint(Point p1, int thickness)
        {
            Point rectangleSize = new Point(thickness);

            Point rectangleOrigin = new Point
            {
                X = p1.X - thickness / 2,
                Y = p1.Y - thickness / 2
            };

            Rectangle point = new Rectangle(rectangleOrigin, rectangleSize);

            return point;
        }

        public static Rectangle MakeLine(Point p1, Point p2, int thickness)
        {
            Point rectangleSize = new Point
            {
                X = Math.Abs(p2.X - p1.X) + thickness,
                Y = Math.Abs(p2.Y - p1.Y) + thickness,
            };

            Point rectangleOrigin = new Point();

            if(p1.X <= p2.X && p1.Y <= p2.Y)
            {
                rectangleOrigin.X = p1.X - thickness / 2;
                rectangleOrigin.Y = p1.Y - thickness / 2;
            }
            else
            {
                rectangleOrigin.X = p2.X - thickness / 2;
                rectangleOrigin.Y = p2.Y - thickness / 2;
            }

            Rectangle line = new Rectangle(rectangleOrigin, rectangleSize);

            return line;
        }

    }
}
