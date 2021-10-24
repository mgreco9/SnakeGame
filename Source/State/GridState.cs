using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Source.Control;
using Snake.Source.Graphic;
using Snake.Source.Item;
using Snake.Source.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.State
{
    public class GridState : IGameState
    {

        private TimeSpan updateInterval = TimeSpan.FromMilliseconds(100);
        private TimeSpan increaseDecreaseInterval = TimeSpan.FromMilliseconds(10);
        private TimeSpan prevUpdateTimeSpan;

        private readonly InputManager inputManager;
        private readonly Drawer drawer;

        public Grid grid;
        public Snakey snakey;
        public Apple apple;

        public SnakeController snakeController;

        public bool gameOver { get; set; }

        public GridState()
        {
            inputManager = InputManager.Instance;
            drawer = Drawer.Instance;

            grid = new Grid();

            apple = new Apple(grid);

            snakey = new Snakey(grid, apple);

            snakeController = ControllerFactory.GetSnakeController(GameOptions.controller);
            snakeController.grid = grid;
            snakeController.snake = snakey;
            snakeController.apple = apple;
            snakeController.Initialize();
            snakey.Controller = snakeController;

            gameOver = false;
        }

        public State Update(GameTime gameTime)
        {
            // 1 - Retrieve inputs
            bool inputIncreaseTime = inputManager.inputIncreaseTime;
            bool inputDecreaseTime = inputManager.inputDecreaseTime;

            // 2 - Retrieve the elapsed time since the last game update
            TimeSpan currTime = gameTime.TotalGameTime;
            TimeSpan elapsedTime = currTime - prevUpdateTimeSpan;

            // 3 - If enough time has passed, update game
            SnakeAction snakeAction = SnakeAction.WAIT;
            if (elapsedTime >= updateInterval)
            {
                prevUpdateTimeSpan = currTime;
                snakeAction = snakey.Update();
            }

            // 4 - Change the required update time based on the user inputs
            if (inputIncreaseTime && updateInterval > TimeSpan.Zero)
                updateInterval -= increaseDecreaseInterval;
            if (inputDecreaseTime)
                updateInterval += increaseDecreaseInterval;

            // 5 - Depending on the snake action, return the next state
            if (snakeAction == SnakeAction.DIE)
                return State.GAME_OVER;
            if (snakeAction == SnakeAction.WIN)
                return State.WIN;

            return State.IN_GAME;
        }

        public void Draw()
        {
            drawer.Clear();

            snakey.Draw();
            apple.Draw();

            if (gameOver)
                drawer.DrawText("GAME OVER", Color.Red);
        }

    }
}
