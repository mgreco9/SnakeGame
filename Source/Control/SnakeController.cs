using Microsoft.Xna.Framework;
using Snake.Source.Control.AIControl;
using Snake.Source.Control.KeyboardControl;
using Snake.Source.Graphic;
using Snake.Source.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Control
{
    public abstract class SnakeController
    {
        public Apple apple { get; set; }
        public Snakey snake { get; set; }
        public Grid grid { get; set; }

        public virtual void Initialize(){}

        public abstract Direction GetDirection();

        protected Drawer drawer = Drawer.Instance;
        public Color color = Color.LightGray;

        public abstract void Draw();
    }
}
