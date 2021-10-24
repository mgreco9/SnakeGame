using Microsoft.Xna.Framework;
using Snake.Source.Control;
using Snake.Source.Graphic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.State
{
    class WinState : IGameState
    {
        private readonly InputManager inputManager;
        private readonly Drawer drawer;
        public WinState()
        {
            inputManager = InputManager.Instance;
            drawer = Drawer.Instance;
        }

        public State Update(GameTime gameTime)
        {
            bool inputReset = inputManager.inputReset;

            if (inputReset)
                return State.IN_GAME;

            return State.WIN;
        }

        public void Draw()
        {
            drawer.DrawText("You Win!", Color.Green);
        }
    }
}
