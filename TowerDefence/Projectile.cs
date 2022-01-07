using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    class Projectile : GameObject
    {
        public Vector2 direction;
        public Vector2 position;



        public Projectile(Texture2D texture, Vector2 position,Rectangle HitBox, Vector2 direction) :base(texture, position,HitBox)
        {
            this.position = position;
            this.direction = direction;

           
        }

        public override void Update()
        {
            position += direction * 0.2f;
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
          
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, hitbox, Color.White);
        }
    }
}
