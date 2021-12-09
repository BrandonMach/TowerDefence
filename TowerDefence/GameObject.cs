using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    public class GameObject
    {
        public Texture2D texture;
        public Vector2 pos;
        public Rectangle hitbox;
        public GameObject(Texture2D texture, Vector2 position, Rectangle HitBox)
        {
            this.texture = texture;
            this.pos = position;
            this.hitbox = HitBox;
        }







        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, pos, Color.White);
        }
    }
}

