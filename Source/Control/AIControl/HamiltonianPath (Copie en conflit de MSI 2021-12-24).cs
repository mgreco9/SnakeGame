using Microsoft.Xna.Framework;
using Snake.Source.Item;
using Snake.Source.Option;
using Snake.Source.Util;
using Snake.Source.Util.Container;
using Snake.Source.Util.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Control.AIControl
{
    class HamiltonianPath : SnakeController
    {
        CircularLinkedList<GridCoordinate> vertices { get; set; }
        public Dictionary<GridCoordinate, List<GridCoordinate>> edges { get; set; }

        public CircularLinkedList<GridCoordinate> path { get; set; }
        public SingleNode<GridCoordinate> currentPosition { get; set; }

        HamiltonianCycleArray hamiltonianCycleSearch;
        public HamiltonianCycleMerge merger { get; set; }

        public override void Initialize()
        {
            vertices = new CircularLinkedList<GridCoordinate>();
            edges = new Dictionary<GridCoordinate, List<GridCoordinate>>();
            InitializeVerticesEdges();

            hamiltonianCycleSearch = new HamiltonianCycleArray
            {
                vertices = vertices,
                edges = edges
            };

            merger = new HamiltonianCycleMerge();
            merger.edges = edges;

            path = new CircularLinkedList<GridCoordinate>();
            for (int row = 0; row < GameOptions.NB_ROW; row+=2)
            {
                for (int col = 0; col < GameOptions.NB_COL; col+=2)
                {
                    CircularLinkedList<GridCoordinate> newVertices = new CircularLinkedList<GridCoordinate>();
                    for (int i = row; i < row+2; i++)
                    {
                        for (int j = col; j < col+2; j++)
                        {
                            newVertices.Add(new GridCoordinate(i, j));
                        }
                    }

                    hamiltonianCycleSearch.vertices = newVertices;
                    hamiltonianCycleSearch.Search();
                    path = merger.mergeCycle(path, hamiltonianCycleSearch.path);
                }
            }

            currentPosition = path.Find(snake.Head);
        }

        private void InitializeVerticesEdges()
        {
            for (int row = 0; row < GameOptions.NB_ROW; row++)
            {
                for (int col = 0; col < GameOptions.NB_COL; col++)
                {
                    GridCoordinate currCoordinate = new GridCoordinate(row, col);
                    vertices.Add(currCoordinate);
                    edges.Add(currCoordinate, GetNeighbours(currCoordinate));
                }
            }
        }

        private List<GridCoordinate> GetNeighbours(GridCoordinate currCoordinate)
        {
            List<GridCoordinate> neighbours = new List<GridCoordinate>();

            GridCoordinate rightCell = new GridCoordinate(currCoordinate.Row, currCoordinate.Col + 1);
            GridCoordinate topCell = new GridCoordinate(currCoordinate.Row - 1, currCoordinate.Col);
            GridCoordinate leftCell = new GridCoordinate(currCoordinate.Row, currCoordinate.Col - 1);
            GridCoordinate bottomCell = new GridCoordinate(currCoordinate.Row + 1, currCoordinate.Col);

            if (rightCell.Col < GameOptions.NB_COL)
                neighbours.Add(rightCell);

            if (topCell.Row >= 0)
                neighbours.Add(topCell);

            if (leftCell.Col >= 0)
                neighbours.Add(leftCell);

            if (bottomCell.Row < GameOptions.NB_ROW)
                neighbours.Add(bottomCell);

            return neighbours;
        }

        public override Direction GetDirection()
        {
            Direction direction;
            if (currentPosition.Next != null)
            {
                direction = CloseCellsToDirection(currentPosition.Value, currentPosition.Next.Value);
                currentPosition = currentPosition.Next;
            }
            else
            {
                direction = CloseCellsToDirection(currentPosition.Value, path.Head.Value);
                currentPosition = path.Head;
            }

            return direction;
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

        public override void Draw()
        {
            if (path.Count == 0)
                return;

            // Draw a line between each part of the path
            Point p1;
            Point p2;
            Rectangle line;

            SingleNode<GridCoordinate> currCell = path.Head;

            for (int i = 0; i < path.Count; i++)
            {
                p1 = grid.GridCoordinateToPoint(currCell.Value);
                p2 = grid.GridCoordinateToPoint(currCell.Next.Value);

                line = FigureMaker.MakeLine(p1, p2, 1);

                drawer.DrawRectangle(line, color);

                currCell = currCell.Next;
            }

            p1 = grid.GridCoordinateToPoint(currCell.Value);
            p2 = grid.GridCoordinateToPoint(path.Head.Value);

            line = FigureMaker.MakeLine(p1, p2, 1);

            drawer.DrawRectangle(line, color);
        }

    }
}
