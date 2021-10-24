using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Graphic
{
    public class Drawer
    {
        public GraphicsDeviceManager graphics { get; set; }
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

        public void LoadContent()
        {
            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public void Clear()
        {
            graphics.GraphicsDevice.Clear(Color.Black);
        }

        public void DrawRectangle(Rectangle rectangle)
        {
            DrawRectangle(pixel, rectangle, Color.White);
        }

        public void DrawRectangle(Rectangle rectangle, Color color)
        {
            DrawRectangle(pixel, rectangle, color);
        }

        public void DrawRectangle(Texture2D texture, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }

        public void DrawText(String text, Color color)
        {
            spriteBatch.DrawString(spriteFont, text, new Vector2(0,0), color);
        }
    }
}
