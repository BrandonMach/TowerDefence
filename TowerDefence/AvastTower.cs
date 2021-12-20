using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    public class AvastTower: Towers
    {


        public AvastTower(Texture2D texture, Vector2 position, Rectangle HitBox) : base(texture, position, HitBox)
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
