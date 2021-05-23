using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Source.Control;
using Snake.Source.Graphic;
using Snake.Source.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.State
{
    public class GridState : IGameState
    {
        public const int GRID_WIDTH = 40;
        public const int GRID_HEIGHT = 40;
        public const float CELL_SIZE = MainGame.SCREEN_WIDTH/GRID_WIDTH;

        private TimeSpan updateInterval = TimeSpan.FromMilliseconds(100);
        private TimeSpan increaseDecreaseInterval = TimeSpan.FromMilliseconds(10);
        private TimeSpan prevUpdateTimeSpan;

        InputManager inputManager;
        Drawer drawer;

        public Grid grid;

        public bool gameOver { get; set; }

        public GridState(SnakeController controller)
        {
            inputManager = InputManager.Instance;
            drawer = Drawer.Instance;

            grid = new Grid(this)
            {
                Width = GRID_WIDTH,
                Height = GRID_HEIGHT,
                CellSize = CELL_SIZE,
            };

            grid.snake = new Snakey(grid)
            {
                BodySize = (int)(CELL_SIZE - 5),
                Controller = controller
            };

            grid.apple = new Apple(grid)
            {
                AppleSize = (int)(CELL_SIZE - 5)
            };

            controller.snake = grid.snake;
            controller.apple = grid.apple;

            grid.InitializeGrid();

            gameOver = false;
        }

        void IGameState.Update(GameTime gameTime)
        {
            //Update the grid only on certain interval
            TimeSpan currTime = gameTime.TotalGameTime;
            TimeSpan elapsedTime = currTime - prevUpdateTimeSpan;

            bool inputReset = inputManager.inputReset;

            bool inputIncreaseTime = inputManager.inputIncreaseTime;
            bool inputDecreaseTime = inputManager.inputDecreaseTime;

            if(!gameOver && elapsedTime >= updateInterval)
            {
                prevUpdateTimeSpan = currTime;
                grid.Update();
            }

            if (gameOver && inputReset)
            {
                grid.InitializeGrid();
                gameOver = false;
            }

            if (inputIncreaseTime && updateInterval > TimeSpan.Zero)
                updateInterval -= increaseDecreaseInterval;
            if (inputDecreaseTime)
                updateInterval += increaseDecreaseInterval;
        }

        void IGameState.Draw()
        {
            grid.Draw();

            if (gameOver)
                drawer.DrawText("GAME OVER", Color.Red);
        }

        private void DrawGameOver()
        {

        }

    }
}
