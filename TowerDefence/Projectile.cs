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
        private int currentFrame;
        private Rectangle sourceRect;
        public bool alive;
        



        public Projectile(Texture2D texture, Vector2 position,Rectangle HitBox, Vector2 direction, int currentFrame) :base(texture, position,HitBox)
        {
            this.position = position;
            this.direction = direction;
            this.currentFrame = currentFrame;
            alive = true;
           
        }

        public override void Update()
        {
            position += direction * 0.1f;
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width/3, texture.Height);
            
          
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            if (alive)
            {
                sourceRect = new Rectangle((currentFrame - 1) * texture.Width / 3, 0, texture.Width / 3, texture.Height);
                _spriteBatch.Draw(texture, hitbox, sourceRect, Color.White);
            }
          
        }
    }
}
