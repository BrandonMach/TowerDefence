using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    class HUDManager
    {

        public HUDManager()
        {

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(SpriteManager.AvastTex, new Rectangle(1750, 120, SpriteManager.AvastTex.Width, SpriteManager.AvastTex.Height), Color.White);
        }
    }
}
