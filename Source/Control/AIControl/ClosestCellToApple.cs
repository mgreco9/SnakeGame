using Snake.Source.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Control.AIControl
{
    class ClosestCellToApple : SnakeController
    {
        public override Direction getDirection()
        {
            return computeDirection();
        }

        private Direction computeDirection()
        {
            GridCoordinate snake_loc = snake.Head;
            GridCoordinate apple_loc = apple.Position;

            if (snake_loc.Col < apple_loc.Col)
                return tryToGoRight();
            if (snake_loc.Col > apple_loc.Col)
                return tryToGoLeft();
            if (snake_loc.Row < apple_loc.Row)
                return tryToGoDown();
            if (snake_loc.Row > apple_loc.Row)
                return tryToGoUp();

            return Direction.DOWN;
        }

        private Direction tryToGoRight()
        {
            if (snake.CurrDirection == Direction.LEFT)
                return Direction.UP;

            return Direction.RIGHT;
        }

        private Direction tryToGoLeft()
        {
            if (snake.CurrDirection == Direction.RIGHT)
                return Direction.DOWN;

            return Direction.LEFT;
        }

        private Direction tryToGoDown()
        {
            if (snake.CurrDirection == Direction.UP)
                return Direction.LEFT;

            return Direction.DOWN;
        }

        private Direction tryToGoUp()
        {
            if (snake.CurrDirection == Direction.DOWN)
                return Direction.RIGHT;

            return Direction.UP;
        }
    }
}
