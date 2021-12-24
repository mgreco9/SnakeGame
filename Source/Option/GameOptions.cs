using Snake.Source.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Option
{
    public static class GameOptions
    {
        // VIDEO OPTIONS
        public static int SCREEN_WIDTH = 800;
        public static int SCREEN_HEIGHT = 800;

        // GRID OPTIONS
        public static int NB_ROW = 20;
        public static int NB_COL = 20;

        // CONTROLLER OPTIONS
        public static ControllerType controller;
    }
}
