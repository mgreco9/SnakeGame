using Snake.Source.Control.AIControl;
using Snake.Source.Control.KeyboardControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Control
{
    public enum ControllerType
    {
        KEYBOARD,
        GO_STRAIGHT,
        CLOSEST_CELL_TO_APPLE,
        A_STAR,
        HAMILTONIAN,
        HAMILTONIAN_A_STAR
    }

    public class ControllerFactory
    {
        public static SnakeController GetSnakeController(ControllerType controllerType)
        {
            SnakeController controller = null;

            switch (controllerType)
            {
                case ControllerType.KEYBOARD:
                    controller = new KeyboardController();
                    break;
                case ControllerType.GO_STRAIGHT:
                    controller = new GoStraight();
                    break;
                case ControllerType.CLOSEST_CELL_TO_APPLE:
                    controller = new ClosestCellToApple();
                    break;
                case ControllerType.A_STAR:
                    controller = new AStarPathfinder();
                    break;
                case ControllerType.HAMILTONIAN:
                    controller = new HamiltonianPath();
                    break;
                case ControllerType.HAMILTONIAN_A_STAR:
                    controller = new HamiltonianAStar();
                    break;
            }

            return controller;
        }
    }
}
