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
        public float rad;

        public Towers(Texture2D texture, Vector2 position, Rectangle HitBox, float rad) : base(texture, position, HitBox)
        {

        }



        public override void Update()
        {
            pos.X = Mouse.GetState().X;
            pos.Y = Mouse.GetState().Y;
            

            hitbox = new Rectangle((int)pos.X - texture.Width / 2, (int)pos.Y - texture.Height / 2, texture.Width, texture.Height);
        }

        public bool EnemyInRange(Enemys other)
        {
            return Vector2.Distance(pos, other.pos) < (rad + other.rad);

        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }


    }
}
