using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Control.KeyboardControl
{
    class KeyboardController : SnakeController
    {
        private InputManager inputManager;

        public KeyboardController()
        {
            inputManager = InputManager.Instance;
        }

        public override Direction GetDirection()
        {
            return inputManager.inputDirection;
        }

        public override void Draw()
        {

        }
    }
}
