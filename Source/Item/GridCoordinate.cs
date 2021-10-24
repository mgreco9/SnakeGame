using Snake.Source.Option;
using Snake.Source.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Item
{
    public class GridCoordinate : Tuple<int, int>
    {
        public int Row
        {
            get
            {
                return Item1;
            }
        }
        public int Col
        {
            get
            {
                return Item2;
            }
        }

        public GridCoordinate(int row, int col)
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
            int hashCode = (int)(GameOptions.NB_ROW * Col + Row);

            return hashCode;
        }

        public override String ToString()
        {
            return $"({Row},{Col})";
        }
    }
}
