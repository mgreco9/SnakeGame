using Snake.Source.Item;
using System.Linq;
using Snake.Source.Util.Container;
using System.Collections.Generic;

namespace Snake.Source.Util.PathFinding
{
    class HamiltonianCycleMerge
    {
        public Dictionary<GridCoordinate, List<GridCoordinate>> edges { get; set; }

        public LinkedList<GridCoordinate> MergeCycle(LinkedList<GridCoordinate> cycle1, LinkedListNode<GridCoordinate> startCycle1, LinkedList<GridCoordinate> cycle2)
        {
            if (cycle1.Count == 0)
                return cycle2;
            if (cycle2.Count == 0)
                return cycle1;

            LinkedListNode<GridCoordinate> nodeCycle1 = startCycle1;
            for (int count = 0; count < cycle1.Count; count++)
            {
                for (LinkedListNode<GridCoordinate> nodeCycle2 = cycle2.First; nodeCycle2 != null; nodeCycle2 = nodeCycle2.Next)
                {
                    if (edges[nodeCycle2.NextOrFirst().Value].Contains(nodeCycle1.Value) && edges[nodeCycle2.Value].Contains(nodeCycle1.NextOrFirst().Value))
                    {
                        cycle1.InsertListAfter(nodeCycle1, nodeCycle2.NextOrFirst());
                        return cycle1;
                    }
                }
                nodeCycle1 = nodeCycle1.NextOrFirst();
            }
            return cycle1;
        }
    }
}
