using Snake.Source.Item;
using Snake.Source.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Util
{
    class GridCoordinateComparer : IComparer<GridCoordinate>
    {
        static int WIDTH;

        public GridCoordinateComparer()
        {
            WIDTH = GridState.GRID_WIDTH;
        }

        public int Compare(GridCoordinate x, GridCoordinate y)
        {
            int value1 = (int)(x.col * WIDTH + x.row);
            int value2 = (int)(y.col * WIDTH + y.row);

            return value1 - value2;
        }
    }
}
