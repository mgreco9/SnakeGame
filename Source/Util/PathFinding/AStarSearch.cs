using Priority_Queue;
using Snake.Source.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Util.PathFinding
{
    public class AStarSearch
    {
        public GridCoordinate Start { get; set; }
        public GridCoordinate Goal { get; set; }
        public HashSet<GridCoordinate> AvailableCells { get; set; }
        public HashSet<GridCoordinate> ReachableWallCells { get; }
        public Stack<GridCoordinate> Path { get; }

        private SimplePriorityQueue<GridCoordinate> frontier;
        private HashSet<GridCoordinate> explored;
        private Dictionary<GridCoordinate, GridCoordinate> previousCellDict;
        private Dictionary<GridCoordinate, int> distanceFromStart;

        public AStarSearch()
        {
            ReachableWallCells = new HashSet<GridCoordinate>();
            Path = new Stack<GridCoordinate>();
        }

        public void Search()
        {
            InitializeSearch();

            while (frontier.Count != 0)
            {
                GridCoordinate currCoordinate = frontier.Dequeue();

                if (currCoordinate.Equals(Goal))
                {
                    explored.Add(Goal);
                    break;
                }

                ExploreNeighbours(currCoordinate);
            }

            if (explored.Contains(Goal))
                BuildPath();
        }

        private void InitializeSearch()
        {
            frontier = new SimplePriorityQueue<GridCoordinate>();
            explored = new HashSet<GridCoordinate>();
            previousCellDict = new Dictionary<GridCoordinate, GridCoordinate>();
            distanceFromStart = new Dictionary<GridCoordinate, int>();
            ReachableWallCells.Clear();

            previousCellDict.Add(Start, null);
            distanceFromStart.Add(Start, 0);

            frontier.Enqueue(Start, GetManhattanDistance(Start, Goal));
        }

        private void ExploreNeighbours(GridCoordinate currCoordinate)
        {
            List<GridCoordinate> neighbours = GetNeighbours(currCoordinate);

            foreach (GridCoordinate neighbour in neighbours)
            {
                if (explored.Contains(neighbour))
                    continue;

                explored.Add(neighbour);
                previousCellDict.Add(neighbour, currCoordinate);
                distanceFromStart.Add(neighbour, distanceFromStart[currCoordinate] + 1);
                frontier.Enqueue(neighbour, distanceFromStart[neighbour] + GetManhattanDistance(neighbour, Goal));
            }
        }

        private List<GridCoordinate> GetNeighbours(GridCoordinate currCoordinate)
        {
            List<GridCoordinate> neighbours = new List<GridCoordinate>();

            GridCoordinate rightCell = new GridCoordinate(currCoordinate.Row, currCoordinate.Col + 1);
            GridCoordinate topCell = new GridCoordinate(currCoordinate.Row - 1, currCoordinate.Col);
            GridCoordinate leftCell = new GridCoordinate(currCoordinate.Row, currCoordinate.Col - 1);
            GridCoordinate bottomCell = new GridCoordinate(currCoordinate.Row + 1, currCoordinate.Col);

            if (AvailableCells.Contains(rightCell))
                neighbours.Add(rightCell);
            else
                ReachableWallCells.Add(rightCell);

            if (AvailableCells.Contains(topCell))
                neighbours.Add(topCell);
            else
                ReachableWallCells.Add(topCell);

            if (AvailableCells.Contains(leftCell))
                neighbours.Add(leftCell);
            else
                ReachableWallCells.Add(leftCell);

            if (AvailableCells.Contains(bottomCell))
                neighbours.Add(bottomCell);
            else
                ReachableWallCells.Add(bottomCell);


            return neighbours;
        }

        private int GetManhattanDistance(GridCoordinate p1, GridCoordinate p2)
        {
            int distance = 0;

            distance += (int)Math.Abs(p1.Row - p2.Row);
            distance += (int)Math.Abs(p1.Col - p2.Col);

            return distance;
        }

        private void BuildPath()
        {
            Path.Clear();
            GridCoordinate currCoordinate = Goal;
            while (!currCoordinate.Equals(Start))
            {
                Path.Push(currCoordinate);
                currCoordinate = previousCellDict[currCoordinate];
            }
        }

    }
}
