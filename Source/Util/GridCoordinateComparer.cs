using Snake.Source.Item;
using Snake.Source.Option;
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
        public int Compare(GridCoordinate x, GridCoordinate y)
        {
            int value1 = (int)(x.Col * GameOptions.NB_COL + x.Row);
            int value2 = (int)(y.Col * GameOptions.NB_COL + y.Row);

            return value1 - value2;
        }
    }
}
