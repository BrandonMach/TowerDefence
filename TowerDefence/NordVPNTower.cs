using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    class NordVPNTower : Towers
    {

        Rectangle slowRange;
        public NordVPNTower(Texture2D texture, Vector2 position, Rectangle HitBox, int rad, double attackTimer, double attackDelay) : base(texture, position, HitBox, rad, attackTimer, attackDelay)
        {

        }

        public override void Update()
        {

            base.Update();
            slowRange = new Rectangle((int)pos.X - SpriteManager.RangeRing.Width * 2, (int)pos.Y - SpriteManager.RangeRing.Height * 2, SpriteManager.RangeRing.Width * rad, SpriteManager.RangeRing.Height *rad);


            foreach (Enemys enemy in Game1.enemyList)
            {
                if (slowRange.Intersects(enemy.hitbox))
                {
                    enemy.speed -= 0.1f;
                    Debug.WriteLine("enemy speed: " +enemy.speed);
                }
            }

         
        }



        public override void Draw(SpriteBatch _spriteBatch)
        {

            base.Draw(_spriteBatch);
        }

    }
}
