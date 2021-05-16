using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Source.Graphic;
using Snake.Source.State;
using Snake.Source.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Item
{
    public class Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public float CellSize { get; set; }

        private GridState state;

        SortedSet<GridCoordinate> freeSpace; // Cells with no snake

        public Snake snake;
        public Apple apple;

        public Grid(GridState state)
        {
            this.state = state;
        }

        /// <summary>
        /// Initialize the current free cells, snake position and the apple position
        /// </summary>
        public void InitializeGrid()
        {
            snake = new Snake(this);
            apple = new Apple(this);

            snake.BodySize = (int)(CellSize - 5);
            apple.AppleSize = (int)(CellSize - 5);

            freeSpace = new SortedSet<GridCoordinate>(new GridCoordinateComparer());
            LinkedList<GridCoordinate> snakeBody = new LinkedList<GridCoordinate>();

            for(uint row = 0; row < Height; row++)
            {
                for(uint col = 0; col < Width; col++)
                {
                    freeSpace.Add(new GridCoordinate(row, col));
                }
            }

            for(uint col = (uint)(Width /2 - 5); col <= Width/2; col++)
            {
                GridCoordinate coord = new GridCoordinate((uint)(Height / 2), col);
                freeSpace.Remove(coord);
                snakeBody.AddFirst(coord);
            }

            snake.Body = snakeBody;

            apple.ResetPosition(freeSpace);
        }

        public void Update()
        {
            snake.Update();
        }

        public void Draw()
        {
            snake.Draw();
            apple.Draw();
        }

        public void AddFreeCell(GridCoordinate cellToAdd)
        {
            freeSpace.Add(cellToAdd);
        }

        public void RemoveFreeCell(GridCoordinate cellToRemove)
        {
            freeSpace.Remove(cellToRemove);
        }

        public void ResetApplePosition()
        {
            apple.ResetPosition(freeSpace);
        }

        public void GameOver()
        {
            state.gameOver = true;
        }

        /// <summary>
        /// Transform the cell index into a position in the game window
        /// </summary>
        /// <param name="gridCoordinate"> The cell index </param>
        /// <returns> The corresponding point of the cell in the game window </returns>
        public Point gridCoordinateToPoint(GridCoordinate gridCoordinate)
        {
            Point point = new Point
            {
                X = (int)(gridCoordinate.col * CellSize + CellSize / 2),
                Y = (int)(gridCoordinate.row * CellSize + CellSize / 2)
            };

            return point;
        }
    }
}
