using Snake.Source.Item;
using Snake.Source.Util.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Util.PathFinding
{
    public class HamiltonianCycleSearch
    {
        public LinkedList<GridCoordinate> Vertices { get; set; }
        public Dictionary<GridCoordinate, List<GridCoordinate>> Edges { get; set; }

        public HamiltonianCycleSearch()
        {
        }
        public LinkedList<GridCoordinate> SearchPath()
        {
            // 1 - Initialize path
            LinkedList<GridCoordinate> path = new LinkedList<GridCoordinate>();

            // 2 - Add the first node to the path
            LinkedListNode<GridCoordinate> firstNode = Vertices.First;
            path.AddLast(firstNode.Value);

            // 3 - Start the recursion
            HamiltonianRecursion(path);

            return path;
        }

        private bool HamiltonianRecursion(LinkedList<GridCoordinate> path)
        {
            // 1 - Retrieve the neighbours of the last entry of the path
            GridCoordinate currCoordinate = path.Last.Value;
            List<GridCoordinate> neighbours = Edges[currCoordinate];

            // 2 - Iterate over the neighbours
            foreach (GridCoordinate nextCoordinate in neighbours)
            {
                // 2.0 - Check the neighbour is in the defined vertice
                if (!Vertices.Contains(nextCoordinate))
                    continue;

                // 2.1 - Check if the hamiltonian cycle is complete and valid, if so stop process
                if (nextCoordinate.Equals(path.First.Value) && path.Count == Vertices.Count)
                    return true;
                
                // 2.2 - If the path already contains the next coordinate, iterate over next neighbour
                if (path.Contains(nextCoordinate))
                    continue;

                // 2.3 - If the next coordinate as not been added yet, add it at the end of the path
                path.AddLast(nextCoordinate);

                // 2.4 - Do a recursion, if return true, stop process
                if (HamiltonianRecursion(path))
                    return true;

                // 2.5 - If recursion didn't work, remove next coordinate from the path and iterate over next one
                path.RemoveLast();
            }
            return false; 
        }
    }
}
