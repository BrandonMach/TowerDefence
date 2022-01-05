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


        public NordVPNTower(Texture2D texture, Vector2 position, Rectangle HitBox, int rad, double attackTimer, double attackDelay) : base(texture, position, HitBox, rad, attackTimer, attackDelay)
        {

        }

        public override void Update()
        {

            base.Update();

        }



        public override void Draw(SpriteBatch _spriteBatch)
        {

            base.Draw(_spriteBatch);
        }

    }
}
