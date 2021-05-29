using Snake.Source.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Item
{
    public class GridCoordinate : Tuple<uint, uint>
    {
        public uint Row
        {
            get
            {
                return Item1;
            }
        }
        public uint Col
        {
            get
            {
                return Item2;
            }
        }

        public GridCoordinate(uint row, uint col)
            :base(row,col)
        {
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            GridCoordinate other = (GridCoordinate)obj;
            return (Row == other.Row) && (Col == other.Col);
        }

        public override int GetHashCode()
        {
            int hashCode = (int)(GridState.GRID_WIDTH * Col + Row);

            return hashCode;
        }

        public override String ToString()
        {
            return $"({Row},{Col})";
        }
    }
}
