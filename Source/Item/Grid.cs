using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Source.Graphic;
using Snake.Source.Option;
using Snake.Source.State;
using Snake.Source.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Item
{
    public class Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int GridSize { get; set; }
        public float CellSize { get; set; }

        private float xOffset;
        private float yOffset;

        public HashSet<GridCoordinate> freeSpace; // Cells with no snake

        public Grid()
        {
            Width = GameOptions.NB_COL;
            Height = GameOptions.NB_ROW;
            GridSize = Width * Height;

            float num = Math.Min(GameOptions.SCREEN_HEIGHT, GameOptions.SCREEN_WIDTH);
            float denum = Math.Max(Width, Height);

            CellSize = num / denum;

            xOffset = (GameOptions.SCREEN_WIDTH - CellSize * Width) / 2;
            yOffset = (GameOptions.SCREEN_HEIGHT - CellSize * Height) / 2;

            InitializeGrid();
        }

        /// <summary>
        /// Initialize the current free cells, snake position and the apple position
        /// </summary>
        public void InitializeGrid()
        {
            freeSpace = new HashSet<GridCoordinate>();

            for(int row = 0; row < Height; row++)
            {
                for(int col = 0; col < Width; col++)
                {
                    freeSpace.Add(new GridCoordinate(row, col));
                }
            }
        }

        public void Update()
        {
        }

        public void Draw()
        {
        }

        public void AddFreeCell(GridCoordinate cellToAdd)
        {
            freeSpace.Add(cellToAdd);
        }

        public void RemoveFreeCell(GridCoordinate cellToRemove)
        {
            freeSpace.Remove(cellToRemove);
        }


        /// <summary>
        /// Transform the cell index into a position in the game window
        /// </summary>
        /// <param name="gridCoordinate"> The cell index </param>
        /// <returns> The corresponding point of the cell in the game window </returns>
        public Point GridCoordinateToPoint(GridCoordinate gridCoordinate)
        {
            Point point = new Point
            {
                X = (int)((gridCoordinate.Col * CellSize + CellSize / 2) + xOffset),
                Y = (int)((gridCoordinate.Row * CellSize + CellSize / 2) + yOffset)
            };

            return point;
        }
    }
}
