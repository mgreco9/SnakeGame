using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Source.Control;
using Snake.Source.Graphic;
using Snake.Source.Option;
using Snake.Source.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Item
{
    public enum SnakeAction
    {
        WAIT,
        MOVE,
        EAT,
        DIE,
        WIN
    }

    public class Snakey
    {
        readonly Grid grid;
        readonly Apple apple;

        readonly Drawer drawer;
        public Color Color { get; set; }

        public SnakeController Controller { get; set; }
        public Direction CurrDirection { get; set; }
        public LinkedList<GridCoordinate> Body { get; set; }
        public int BodySize { get; set; }

        public GridCoordinate Head
        {
            get
            {
                return Body.First.Value;
            }
        }

        public GridCoordinate Tail
        {
            get
            {
                return Body.Last.Value;
            }
        }

        public Snakey(Grid grid, Apple apple)
        {
            this.grid = grid;
            this.apple = apple;

            drawer = Drawer.Instance;
            Color = Color.White;
            BodySize = (int)grid.CellSize - 5;

            InitializeSnakePosition();
        }

        private void InitializeSnakePosition()
        {
            Body = new LinkedList<GridCoordinate>();

            int middleRow = GameOptions.NB_ROW / 2;
            int middleCol = GameOptions.NB_COL / 2;

            
            for (int i  = 0; i<2; i++)
            {
                GridCoordinate bodyPart = new GridCoordinate(middleRow, middleCol - i);

                Body.AddLast(bodyPart);
                grid.RemoveFreeCell(bodyPart);
            }

            CurrDirection = Direction.RIGHT;
        }

        public SnakeAction Update()
        {
            // 1 - Get next move
            Direction inputDirection = Controller.GetDirection();

            // 2 - Check if legal move, if not go straight
            bool isLegalMove = CheckIfLegalMove(inputDirection);
            if (isLegalMove)
                CurrDirection = inputDirection;

            // 3 - Compute next position
            GridCoordinate nextPosition = ComputeNextPosition();

            // 4 - Check if game over
            bool isGameOver = CheckIfGameOver(nextPosition);
            if (isGameOver)
                return SnakeAction.DIE;

            // 5 - Move head
            Move(nextPosition);

            // 6 - Check if win
            bool eatApple = CheckIfEatApple(nextPosition);
            if (eatApple && CheckIfWin())
            {
                apple.Remove();
                return SnakeAction.WIN;
            }

            // 7 - Check if eat apple
            if (eatApple)
            {
                apple.ResetPosition();
                return SnakeAction.EAT;
            }

            // 8 - Remove tail if didn't eat apple
            RemoveTail();

            return SnakeAction.MOVE;
        }

        /// <summary>
        /// Determine if the given command is legal
        /// </summary>
        /// <param name="inputDirection"> The command input direction provided</param>
        /// <returns> Flag showing if the move is legal </returns>
        private bool CheckIfLegalMove(Direction inputDirection)
        {
            // The snake cannot turn back in one command
            if (CurrDirection == Direction.UP && inputDirection == Direction.DOWN)
                return false;
            if (CurrDirection == Direction.DOWN && inputDirection == Direction.UP)
                return false;
            if (CurrDirection == Direction.LEFT && inputDirection == Direction.RIGHT)
                return false;
            if (CurrDirection == Direction.RIGHT && inputDirection == Direction.LEFT)
                return false;

            return true;
        }

        /// <summary>
        /// Compute the next position of the head according to the current direction
        /// </summary>
        /// <returns> The new position of the head </returns>
        private GridCoordinate ComputeNextPosition()
        {
            GridCoordinate headPosition = Head;
            GridCoordinate nextPosition;

            switch (CurrDirection)
            {
                case Direction.UP:
                    nextPosition = new GridCoordinate(headPosition.Row - 1, headPosition.Col);
                    break;
                case Direction.RIGHT:
                    nextPosition = new GridCoordinate(headPosition.Row, headPosition.Col + 1);
                    break;
                case Direction.DOWN:
                    nextPosition = new GridCoordinate(headPosition.Row + 1, headPosition.Col);
                    break;
                case Direction.LEFT:
                    nextPosition = new GridCoordinate(headPosition.Row, headPosition.Col - 1);
                    break;
                default:
                    throw new Exception("illegal direction");
            }
            
            return nextPosition;
        }

        /// <summary>
        /// Determine if the snake's head impacts with the wall or its body
        /// </summary>
        /// <param name="nextPosition"> The next computed position of the head </param>
        /// <returns> Flag showing if Game Over </returns>
        private bool CheckIfGameOver(GridCoordinate nextPosition)
        {
            // if next position is part of the body, game over
            if (Body.Contains(nextPosition) && !nextPosition.Equals(Tail))
                return true;

            // if goes out of grid bounds, game over
            if (nextPosition.Row < 0)
                return true;
            if (nextPosition.Col < 0)
                return true;
            if (nextPosition.Row >= grid.Height)
                return true;
            if (nextPosition.Col >= grid.Width)
                return true;

            return false;
        }

        /// <summary>
        /// Determine if the head next position coïncide with the apple's position
        /// </summary>
        /// <param name="nextPosition"> The next computed position of the head </param>
        /// <returns> Flag showing if the snake is on the apple </returns>
        private bool CheckIfEatApple(GridCoordinate nextPosition)
        {
            GridCoordinate applePosition = apple.Position;

            if (nextPosition.Equals(applePosition))
                return true;

            return false;
        }

        private bool CheckIfWin()
        {
            if (Body.Count == grid.GridSize)
                return true;

            return false;
        }

        private void Move(GridCoordinate nextPosition)
        {
            Body.AddFirst(nextPosition);
            grid.RemoveFreeCell(nextPosition);
        }

        private void RemoveTail()
        {
            GridCoordinate tailPosition = Tail;

            Body.RemoveLast();
            grid.AddFreeCell(tailPosition);
        }

        public void Draw()
        {
            // Draw path to follow
            Controller.Draw();

            // Draw a line between each part of the body
            Point p1;
            Point p2;
            Rectangle line;

            GridCoordinate prevCell = Head;
            GridCoordinate currCell;

            LinkedListNode<GridCoordinate> currentNode = Body.First;

            while(currentNode.Next != null)
            {
                currentNode = currentNode.Next;
                currCell = currentNode.Value;

                p1 = grid.GridCoordinateToPoint(prevCell);
                p2 = grid.GridCoordinateToPoint(currCell);

                line = FigureMaker.MakeLine(p1, p2, BodySize);

                drawer.DrawRectangle(line, Color);

                prevCell = currCell;
            }
        }
    }
}
