using Snake.Source.Item;
using System;
using System.Collections.Generic;
using Snake.Source.Util.PathFinding;
using Microsoft.Xna.Framework;
using Snake.Source.Util;

namespace Snake.Source.Control.AIControl
{
    class AStarPathfinder : SnakeController
    {
        Stack<GridCoordinate> pathToFollow = new Stack<GridCoordinate>();
        private readonly AStarSearch aStarSearch = new AStarSearch();

        public override Direction GetDirection()
        {
            return ComputeDirection();
        }

        private Direction ComputeDirection()
        {
            // 1 - Get the next location
            GridCoordinate next_loc = ComputeNextLocation();

            // 2 - If the next location is null (unreachable goal), pile up
            if (next_loc is null)
                next_loc = PileUp(snake.Head);

            // 3 - Compute the next direction from the current and the next location
            Direction nextDir = CloseCellsToDirection(snake.Head, next_loc);

            // 4 - Return the direction;
            return nextDir;
        }

        public GridCoordinate ComputeNextLocation()
        {
            // 1 - Define the parameters of the AStar search
            aStarSearch.Start = snake.Head;
            aStarSearch.Goal = apple.Position;
            aStarSearch.AvailableCells = grid.freeSpace;

            // 2 - Find the path using AStar
            pathToFollow = aStarSearch.SearchPath();

            // 3 - If the path is empty, nothing to return
            if (pathToFollow.Count == 0)
                return null;

            // 4 - Return the next location in the path
            return pathToFollow.Peek();
        }

        private GridCoordinate PileUp(GridCoordinate currCell)
        {
            // 1 - Retrieve the adjacent cells
            GridCoordinate rightCell = new GridCoordinate(currCell.Row, currCell.Col + 1);
            GridCoordinate bottomCell = new GridCoordinate(currCell.Row + 1, currCell.Col);
            GridCoordinate leftCell = new GridCoordinate(currCell.Row, currCell.Col - 1);
            GridCoordinate topCell = new GridCoordinate(currCell.Row - 1, currCell.Col);

            // 2 - The next cell is the adjacent free cell in that order of priority : right, left, top, bottom
            GridCoordinate nextCell = bottomCell;
            if (grid.freeSpace.Contains(rightCell))
                nextCell = rightCell;
            else if (grid.freeSpace.Contains(leftCell))
                nextCell = leftCell;
            else if (grid.freeSpace.Contains(topCell))
                nextCell = topCell;

            // 3 - Return the next cell
            return nextCell;
        }

        private Direction CloseCellsToDirection(GridCoordinate start, GridCoordinate dest)
        {
            if (start.Row == dest.Row - 1)
                return Direction.DOWN;
            if (start.Row == dest.Row + 1)
                return Direction.UP;
            if (start.Col == dest.Col - 1)
                return Direction.RIGHT;
            if (start.Col == dest.Col + 1)
                return Direction.LEFT;

            throw new Exception("Given cells are not close to each other");
        }

        public override void Draw()
        {
            if (pathToFollow.Count == 0)
                return;

            // Draw a line between each part of the path
            Point p1;
            Point p2;
            Rectangle line;

            GridCoordinate prevCell = pathToFollow.Peek();

            foreach(GridCoordinate currCell in pathToFollow)
            {
                p1 = grid.GridCoordinateToPoint(prevCell);
                p2 = grid.GridCoordinateToPoint(currCell);

                line = FigureMaker.MakeLine(p1, p2, 1);

                drawer.DrawRectangle(line, color);

                prevCell = currCell;
            }
        }
    }
}
