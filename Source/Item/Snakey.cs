using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Source.Control;
using Snake.Source.Graphic;
using Snake.Source.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Item
{
    public class Snakey
    {
        Grid gridWorld;

        Drawer drawer;

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

        GridCoordinate Tail
        {
            get
            {
                return Body.Last.Value;
            }
        }

        public Snakey(Grid grid)
        {
            gridWorld = grid;

            drawer = Drawer.Instance;
        }

        public void Update()
        {
            // 1 - Get next move
            Direction inputDirection = Controller.getDirection();

            // 2 - Check if legal move, if not go straight
            bool isLegalMove = CheckIfLegalMove(inputDirection);
            if (isLegalMove)
                CurrDirection = inputDirection;

            // 3 - Compute next position
            GridCoordinate nextPosition = ComputeNextPosition();

            // 4 - Check if game over
            bool isGameOver = CheckIfGameOver(nextPosition);
            if (isGameOver)
            {
                gridWorld.GameOver();
            }

            // 5 - Check if eat apple
            bool eatApple = CheckIfEatApple(nextPosition);
            if (eatApple)
            {
                gridWorld.ResetApplePosition();
            }

            // 6 - Move head
            Move(nextPosition);

            // 7 - Remove tail if didn't eat apple
            if(!eatApple)
                RemoveTail();
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
            GridCoordinate nextPosition = new GridCoordinate();

            switch (CurrDirection)
            {
                case Direction.UP:
                    nextPosition.row = headPosition.row-1;
                    nextPosition.col = headPosition.col;
                    break;
                case Direction.RIGHT:
                    nextPosition.row = headPosition.row;
                    nextPosition.col = headPosition.col+1;
                    break;
                case Direction.DOWN:
                    nextPosition.row = headPosition.row+1;
                    nextPosition.col = headPosition.col;
                    break;
                case Direction.LEFT:
                    nextPosition.row = headPosition.row;
                    nextPosition.col = headPosition.col-1;
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
            if (nextPosition.row < 0)
                return true;
            if (nextPosition.col < 0)
                return true;
            if (nextPosition.row >= gridWorld.Height)
                return true;
            if (nextPosition.col >= gridWorld.Width)
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
            GridCoordinate applePosition = gridWorld.apple.Position;

            if (nextPosition.Equals(applePosition))
                return true;

            return false;
        }

        /// <summary>
        /// Move the snake's head onto the next position
        /// </summary>
        /// <param name="nextPosition"> The next computed position of the head </param>
        private void Move(GridCoordinate nextPosition)
        {
            Body.AddFirst(nextPosition);

            gridWorld.RemoveFreeCell(nextPosition);
        }

        /// <summary>
        /// Remove the tail of the snake to move its body
        /// </summary>
        private void RemoveTail()
        {
            GridCoordinate tailPosition = Tail;
            Body.RemoveLast();

            gridWorld.AddFreeCell(tailPosition);
        }

        public void Draw()
        {
            // Draw a line between each part of the body
            Point p1;
            Point p2;
            Rectangle line;

            GridCoordinate prevCell = Head;
            GridCoordinate currCell = null;

            LinkedListNode<GridCoordinate> currentNode = Body.First;

            while(currentNode.Next != null)
            {
                currentNode = currentNode.Next;
                currCell = currentNode.Value;

                p1 = gridWorld.gridCoordinateToPoint(prevCell);
                p2 = gridWorld.gridCoordinateToPoint(currCell);

                line = FigureMaker.MakeLine(p1, p2, BodySize);

                drawer.DrawRectangle(line, Color.White);

                prevCell = currCell;
            }
        }
    }
}
