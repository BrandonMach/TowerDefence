using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    class GameObjects
    {
        Texture2D tex;
        Vector2 pos;
        public GameObjects(Texture2D texture, Vector2 position)
        {
            tex = texture;
            pos = position;
        }







        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
