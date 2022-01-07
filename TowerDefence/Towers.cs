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

        public  int avastLevel1Cost = 150;
        public  int avastLevel2Cost = 450;
        public  int NordLevel1Cost = 300;
        public  int NordLevel2Cost = 550;

        Projectile projectile;

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

        public void StartAttack(GameTime gameTime, Enemys enemys, Vector2 direction, Texture2D projectileTex)
        {
            //Vector2 projectileDirection = Vector2.Subtract(enemys.pos, pos);
            startAttackTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (startAttackTimer >= attackDelay)
            {
                startAttackTimer -= attackDelay;
                Debug.WriteLine("StartTimer" + startAttackTimer);
                Debug.WriteLine("attack delay " + attackDelay);
                // Debug.WriteLine("Enemy Hp: " + enemys.enemyHp);


                
                projectile = new Projectile(projectileTex, pos, new Rectangle((int)pos.X, (int)pos.Y, SpriteManager.BallTex.Width, SpriteManager.BallTex.Height), direction, level);
                Game1.projectileList.Add(projectile);


                Debug.WriteLine("Enemy in range");
                enemys.TakeDamage();  
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch); 
        }


    }
}
