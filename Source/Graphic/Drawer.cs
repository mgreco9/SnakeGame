using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Graphic
{
    class Drawer
    {
        public SpriteBatch spriteBatch { get; set; }
        public SpriteFont spriteFont { get; set; }

        Texture2D pixel;

        //Singleton class
        private static Drawer _instance;
        public static Drawer Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new Drawer();
                return _instance;
            }
        }

        private Drawer() 
        { 

        }

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public void DrawRectangle(Rectangle rectangle)
        {
            spriteBatch.Draw(pixel, rectangle, Color.White);
        }

        public void DrawRectangle(Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(pixel, rectangle, color);
        }

        public void DrawRectangle(Rectangle rectangle, Texture2D texture)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void DrawText(String text, Color color)
        {
            spriteBatch.DrawString(spriteFont, text, new Vector2(0,0), color);
        }
    }
}
