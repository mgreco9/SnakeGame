using Snake.Source.Control.AIControl;
using Snake.Source.Control.KeyboardControl;
using Snake.Source.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Control
{
    public abstract class SnakeController
    {
        public Apple apple { get; set; }
        public Snakey snake { get; set; }
        public Grid grid { get; set; }

        public abstract Direction getDirection();
    }

    public class SnakeFactory
    {
        public static SnakeController getSnakeController(string controllerName)
        {
            SnakeController controller = null;

            switch (controllerName)
            {
                case "GoStraight":
                    controller = new GoStraight();
                    break;
                case "ClosestCellToApple":
                    controller = new ClosestCellToApple();
                    break;
                case "AStarPathfinder":
                    controller = new AStarPathfinder();
                    break;
                default:
                    controller = new KeyboardController();
                    break;

            }

            return controller;
        }
    }
}
