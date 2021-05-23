using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Control.AIControl
{
    class GoStraight : SnakeController
    {
        public override Direction getDirection()
        {
            return Direction.DOWN;
        }
    }
}
