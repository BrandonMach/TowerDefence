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
        public static int level;
        public NordVPNTower(Texture2D texture, Vector2 position, Rectangle HitBox, int rad, double attackTimer, double attackDelay) : base(texture, position, HitBox, rad, attackTimer, attackDelay)
        {
            this.rad = rad;
        }

        public override void Update()
        {
            base.Update();

        }
        public void SlowEnemy()
        {
            slowRange = new Rectangle((int)pos.X - SpriteManager.RangeRing.Width, (int)pos.Y - SpriteManager.RangeRing.Height, rad * SpriteManager.RangeRing.Width, rad * SpriteManager.RangeRing.Height);

           
              
        }



        public override void Draw(SpriteBatch _spriteBatch)
        {

            base.Draw(_spriteBatch);
            _spriteBatch.Draw(SpriteManager.BallTex, slowRange, Color.Blue);
           
        }

    }
}
