using Priority_Queue;
using Snake.Source.Item;
using System;
using System.Collections.Generic;

namespace Snake.Source.Util.PathFinding
{
    public class AStarSearch
    {
        public GridCoordinate Start { get; set; }
        public GridCoordinate Goal { get; set; }
        public HashSet<GridCoordinate> AvailableCells { get; set; }

        private SimplePriorityQueue<GridCoordinate> frontier;
        private HashSet<GridCoordinate> explored;
        private Dictionary<GridCoordinate, GridCoordinate> previousCellDict;
        private Dictionary<GridCoordinate, int> distanceFromStart;

        public AStarSearch()
        {
        }

        public Stack<GridCoordinate> SearchPath()
        {
            // 1 - Initialize the different lists
            InitializeSearch();

            // 2 - While there is an element in the frontier to explore
            while (frontier.Count != 0)
            {
                // 2.1 - Retrieve the element to explore
                GridCoordinate currCoordinate = frontier.Dequeue();

                // 2.2 - If it's the goal stop the process
                if (currCoordinate.Equals(Goal))
                {
                    explored.Add(Goal);
                    break;
                }

                // 2.3 - Explore the new element
                ExploreNeighbours(currCoordinate);
            }

            // 3 - If the goal has been reached, build the path from the start to the goal
            Stack<GridCoordinate> path = new Stack<GridCoordinate>();
            if (explored.Contains(Goal))
                path = BuildPath();
            
            // 4 - Return the path
            return path;
        }

        private void InitializeSearch()
        {
            // 1 - Initialize the list
            frontier = new SimplePriorityQueue<GridCoordinate>();
            explored = new HashSet<GridCoordinate>();
            previousCellDict = new Dictionary<GridCoordinate, GridCoordinate>();
            distanceFromStart = new Dictionary<GridCoordinate, int>();

            // 2 - Put the first element in the different lists
            previousCellDict.Add(Start, null);
            distanceFromStart.Add(Start, 0);

            frontier.Enqueue(Start, GetManhattanDistance(Start, Goal));
        }

        private void ExploreNeighbours(GridCoordinate currCoordinate)
        {
            // 1 - Get the neighbours of the current location
            List<GridCoordinate> neighbours = GetNeighbours(currCoordinate);

            // 2 - Iterate over the neighbours
            foreach (GridCoordinate neighbour in neighbours)
            {
                // 2.1 - If the neighbour has already been explored, iterate to the next one
                if (explored.Contains(neighbour))
                    continue;

                // 2.2 - If the neighbour is going to be explored, iterate to the next one
                if (frontier.Contains(neighbour))
                    continue;

                // 2.3 - Add the neighbours to different lists
                previousCellDict.Add(neighbour, currCoordinate);
                distanceFromStart.Add(neighbour, distanceFromStart[currCoordinate] + 1);
                frontier.Enqueue(neighbour, distanceFromStart[neighbour] + GetManhattanDistance(neighbour, Goal));
            }

            // 3 - Set the current location as explored
            explored.Add(currCoordinate);
        }

        private List<GridCoordinate> GetNeighbours(GridCoordinate currCoordinate)
        {
            // 1 - Initialize the list of neighbours
            List<GridCoordinate> neighbours = new List<GridCoordinate>();

            // 2 - Retrieve the neighbours of the current location
            GridCoordinate rightCell = new GridCoordinate(currCoordinate.Row, currCoordinate.Col + 1);
            GridCoordinate topCell = new GridCoordinate(currCoordinate.Row - 1, currCoordinate.Col);
            GridCoordinate leftCell = new GridCoordinate(currCoordinate.Row, currCoordinate.Col - 1);
            GridCoordinate bottomCell = new GridCoordinate(currCoordinate.Row + 1, currCoordinate.Col);

            // 3 - Check if the neighbour cells are available cells (not wall or body), if so add them to the list
            if (AvailableCells.Contains(rightCell))
                neighbours.Add(rightCell);

            if (AvailableCells.Contains(topCell))
                neighbours.Add(topCell);

            if (AvailableCells.Contains(leftCell))
                neighbours.Add(leftCell);

            if (AvailableCells.Contains(bottomCell))
                neighbours.Add(bottomCell);

            // 4 - Return the list of available neighbours
            return neighbours;
        }

        private int GetManhattanDistance(GridCoordinate p1, GridCoordinate p2)
        {
            int distance = 0;

            distance += Math.Abs(p1.Row - p2.Row);
            distance += Math.Abs(p1.Col - p2.Col);

            return distance;
        }

        private Stack<GridCoordinate> BuildPath()
        {
            Stack<GridCoordinate> Path = new Stack<GridCoordinate>();

            GridCoordinate currCoordinate = Goal;
            while (!currCoordinate.Equals(Start))
            {
                Path.Push(currCoordinate);
                currCoordinate = previousCellDict[currCoordinate];
            }

            return Path;
        }

    }
}
