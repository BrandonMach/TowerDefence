using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    public class Towers : GameObject
    {


        public Towers(Texture2D texture, Vector2 position, Rectangle HitBox) : base(texture, position, HitBox)
        {

        }



        public override void Update()
        {
            pos.X = Mouse.GetState().X;
            pos.Y = Mouse.GetState().Y;
            

            hitbox = new Rectangle((int)pos.X - texture.Width / 2, (int)pos.Y - texture.Height / 2, texture.Width, texture.Height);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }


    }
}
