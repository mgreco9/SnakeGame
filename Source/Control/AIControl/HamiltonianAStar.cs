using Microsoft.Xna.Framework;
using Snake.Source.Item;
using Snake.Source.Util.Container;
using Snake.Source.Util.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Control.AIControl
{
    class HamiltonianAStar : SnakeController
    {
        GridCoordinate nextLocationAStar;

        LinkedList<GridCoordinate> hamiltonianPath;
        LinkedListNode<GridCoordinate> currentHamiltonianPosition;
        HamiltonianCycleMerge merger;

        HamiltonianPath hamiltonian;
        AStarPathfinder aStar;

        public override void Initialize()
        {
            aStar = new AStarPathfinder
            {
                snake = snake,
                grid = grid,
                apple = apple,
                color = Color.Yellow
            };

            hamiltonian = new HamiltonianPath
            {
                snake = snake,
                grid = grid,
                apple = apple,
                color = Color.Blue
            };

            aStar.Initialize();
            hamiltonian.Initialize();

            hamiltonianPath = hamiltonian.path;
            currentHamiltonianPosition = hamiltonian.currentPosition;

            merger = hamiltonian.merger;
        }

        public override Direction GetDirection()
        {
            Direction direction;

            nextLocationAStar = aStar.ComputeNextLocation();

            GridCoordinate hamiltonianNextLoc = currentHamiltonianPosition.NextOrFirst().Value;
            LinkedListNode<GridCoordinate> betterNextNode = hamiltonianPath.Find(nextLocationAStar);

            if (nextLocationAStar!= null && !hamiltonianNextLoc.Equals(nextLocationAStar)
                && hamiltonian.edges[betterNextNode.PreviousOrLast().Value].Contains(currentHamiltonianPosition.NextOrFirst().Value)
                && GetManhattanDistance(hamiltonianNextLoc, apple.Position) > GetManhattanDistance(nextLocationAStar, apple.Position))
            {
                LinkedList<GridCoordinate> listToMerge = hamiltonianPath.Divide(currentHamiltonianPosition, betterNextNode);
                merger.MergeCycle(hamiltonianPath, currentHamiltonianPosition.NextOrFirst(), listToMerge);

                if (currentHamiltonianPosition.PreviousOrLast().Value.Equals(nextLocationAStar))
                    hamiltonianPath.Reverse();
            }

            direction = CloseCellsToDirection(currentHamiltonianPosition.Value, currentHamiltonianPosition.NextOrFirst().Value);
            currentHamiltonianPosition = currentHamiltonianPosition.NextOrFirst();
            return direction;
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
            hamiltonian.Draw();
            aStar.Draw();
        }

        private bool checkLinkedListValid(LinkedList<GridCoordinate> listToCheck)
        {
            GridCoordinate prevCoordinate = null;
            foreach(GridCoordinate coordinate in listToCheck)
            {
                if (prevCoordinate == null)
                {
                    prevCoordinate = coordinate;
                    continue;
                }
                
                if(!hamiltonian.edges[prevCoordinate].Contains(coordinate))
                {
                    return false;
                }
                prevCoordinate = coordinate;
            }
            if (!hamiltonian.edges[prevCoordinate].Contains(listToCheck.First.Value))
            {
                return false;
            }

            return true;
        }

        private int GetManhattanDistance(GridCoordinate p1, GridCoordinate p2)
        {
            int distance = 0;

            distance += Math.Abs(p1.Row - p2.Row);
            distance += Math.Abs(p1.Col - p2.Col);

            return distance;
        }
    }
}
