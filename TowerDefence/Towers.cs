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
        public int rad;
        public Rectangle rangeCircle;

        public double startAttackTimer;
        public double attackDelay;
        public bool infoClicked;
        public  int level;
        public int maxLevel;
       

        

        public Towers(Texture2D texture, Vector2 position, Rectangle HitBox, int rad, double attackTimer, double attackDelay) : base(texture, position, HitBox)
        {
            startAttackTimer = attackTimer;
            this.attackDelay = attackDelay;
            infoClicked = false;
            this.rad = rad;
            maxLevel = 3;
            level = 1;
            
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
