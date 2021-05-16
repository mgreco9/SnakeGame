using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Item
{
    public class GridCoordinate
    {
        public uint row;
        public uint col;

        public GridCoordinate()
        {

        }

        public GridCoordinate(uint row, uint col)
        {
            this.row = row;
            this.col = col;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            GridCoordinate other = (GridCoordinate)obj;
            return (row == other.row) && (col == other.col);
        }

        public override String ToString()
        {
            return $"({row},{col})";
        }
    }
}
