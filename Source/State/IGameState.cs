using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.State
{
    public interface IGameState
    {
        State Update(GameTime gameTime);

        void Draw();
    }
}
