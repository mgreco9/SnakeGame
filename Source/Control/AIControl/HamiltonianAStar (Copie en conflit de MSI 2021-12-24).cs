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

        CircularLinkedList<GridCoordinate> hamiltonianPath;
        SingleNode<GridCoordinate> currentHamiltonianPosition;
        HamiltonianCycleMerge merger;

        HamiltonianPath hamiltonian;
        AStarPathfinder aStar;

        public override void Initialize()
        {
            aStar = new AStarPathfinder();
            aStar.snake = snake;
            aStar.grid = grid;
            aStar.apple = apple;

            hamiltonian = new HamiltonianPath();
            hamiltonian.snake = snake;
            hamiltonian.grid = grid;
            hamiltonian.apple = apple;

            aStar.Initialize();
            hamiltonian.Initialize();

            aStar.color = Color.Yellow;
            hamiltonian.color = Color.Blue;

            hamiltonianPath = hamiltonian.path;
            currentHamiltonianPosition = hamiltonian.currentPosition;

            merger = hamiltonian.merger;
        }

        public override Direction GetDirection()
        {
            Direction direction;

            nextLocationAStar = aStar.ComputeNextLocation();

            GridCoordinate hamiltonianNextLoc = currentHamiltonianPosition.Next.Value;

            int hamiltonianDistance = GetManhattanDistance(apple.Position, hamiltonianNextLoc);

            if (nextLocationAStar != null && hamiltonianDistance > GetManhattanDistance(apple.Position, nextLocationAStar))
            {
                SingleNode<GridCoordinate> betterNextNode = hamiltonianPath.Find(nextLocationAStar);

                if (hamiltonian.edges[betterNextNode.Previous.Value].Contains(currentHamiltonianPosition.Next.Value))
                {

                    CircularLinkedList<GridCoordinate> listToMerge = hamiltonianPath.Divide(currentHamiltonianPosition, betterNextNode);
                    CheckCorrectHamiltonianCycle(hamiltonianPath);
                    CheckCorrectHamiltonianCycle(listToMerge);

                    merger.mergeCycle(hamiltonianPath, listToMerge);
                    CheckCorrectHamiltonianCycle(hamiltonianPath);
                }

            }

            direction = CloseCellsToDirection(currentHamiltonianPosition.Value, currentHamiltonianPosition.Next.Value);
            currentHamiltonianPosition = currentHamiltonianPosition.Next;

            return direction;
        }

        private int GetManhattanDistance(GridCoordinate p1, GridCoordinate p2)
        {
            int distance = 0;

            distance += (int)Math.Abs(p1.Row - p2.Row);
            distance += (int)Math.Abs(p1.Col - p2.Col);

            return distance;
        }

        public void CheckCorrectHamiltonianCycle(CircularLinkedList<GridCoordinate> path)
        {
            SingleNode<GridCoordinate> currNode = path.Head;
            SingleNode<GridCoordinate> nextNode = currNode.Next;
            for (int i = 1; i < path.Count; i++)
            {
                if(!hamiltonian.edges[currNode.Value].Contains(nextNode.Value))
                {
                    throw new Exception("ERROR : incorrect hamiltonian path");
                }

                currNode = nextNode;
                nextNode = currNode.Next;
            }

            if (!hamiltonian.edges[currNode.Value].Contains(nextNode.Value))
            {
                throw new Exception("ERROR : incorrect hamiltonian path");
            }
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
    }
}
