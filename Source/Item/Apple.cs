using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Source.Graphic;
using Snake.Source.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Item
{
    public class Apple
    {
        Grid gridWorld;

        Random rand;

        Drawer drawer;

        public GridCoordinate Position { get; set; }
        public int AppleSize { get; set; }

        public Apple(Grid grid)
        {
            gridWorld = grid;

            rand = new Random();

            drawer = Drawer.Instance;
        }

        public void ResetPosition(HashSet<GridCoordinate> freeSpace)
        {
            int idx = rand.Next(freeSpace.Count);
            Position = freeSpace.ElementAt(idx);
        }

        public void Draw()
        {
            Point p1 = gridWorld.gridCoordinateToPoint(Position);

            Rectangle point = FigureMaker.MakePoint(p1, AppleSize);

            drawer.DrawRectangle(point, Color.Red);
        }
    }
}
