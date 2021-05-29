using Snake.Source.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Snake.Source.Util.PathFinding;
using Microsoft.Xna.Framework;

namespace Snake.Source.Control.AIControl
{
    class AStarPathfinder : SnakeController
    {
        Stack<GridCoordinate> pathToFollow = new Stack<GridCoordinate>();
        AStarSearch aStarSearch = new AStarSearch();

        public override Direction getDirection()
        {
            return ComputeDirection();
        }

        private Direction ComputeDirection()
        {
            GridCoordinate snake_loc = snake.Head;
            GridCoordinate apple_loc = apple.Position;
            snake.Color = Color.White;

            aStarSearch.Start = snake_loc;
            aStarSearch.Goal = apple_loc;
            aStarSearch.AvailableCells = grid.freeSpace;

            aStarSearch.Search();

            pathToFollow = aStarSearch.Path;

            if (pathToFollow.Count == 0)
            {
                PileUp(snake_loc);
            }

            if (pathToFollow.Count == 0)
                return Direction.DOWN;

            GridCoordinate next_loc = pathToFollow.Pop();

            return CloseCellsToDirection(snake_loc, next_loc);
        }

        private GridCoordinate FindWayOut()
        {
            HashSet<GridCoordinate> reachableWallCells = aStarSearch.ReachableWallCells;
            LinkedListNode<GridCoordinate> currNode = snake.Body.Last;

            while (!reachableWallCells.Contains(currNode.Value))
            {
                currNode = currNode.Previous;
            }

            return currNode.Value;
        }

        private void PileUp(GridCoordinate currCell)
        {
            GridCoordinate rightCell = new GridCoordinate(currCell.Row, currCell.Col + 1);
            GridCoordinate bottomCell = new GridCoordinate(currCell.Row + 1, currCell.Col);
            GridCoordinate leftCell = new GridCoordinate(currCell.Row, currCell.Col - 1);
            GridCoordinate topCell = new GridCoordinate(currCell.Row - 1, currCell.Col);

            if (grid.freeSpace.Contains(rightCell))
                pathToFollow.Push(rightCell);
            else if (grid.freeSpace.Contains(leftCell))
                pathToFollow.Push(leftCell);
            else if (grid.freeSpace.Contains(topCell))
                pathToFollow.Push(topCell);
            else if (grid.freeSpace.Contains(bottomCell))
                pathToFollow.Push(bottomCell);
        }

        private Direction CloseCellsToDirection(GridCoordinate start, GridCoordinate dest)
        {
            //Trace.TraceInformation($"start : {start}; dest : {dest}");

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
    }
}
