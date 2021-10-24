using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.State
{
    public enum State
    {
        IN_GAME,
        GAME_OVER,
        WIN
    }

    public class StateFactory
    {
        public static IGameState GetGameState(State state)
        {
            IGameState gameState = null;

            switch (state)
            {
                case State.IN_GAME:
                    gameState = new GridState();
                    break;
                case State.GAME_OVER:
                    gameState = new GameOverState();
                    break;
                case State.WIN:
                    gameState = new WinState();
                    break;
            }

            return gameState;
            ;
        }

    }
}
