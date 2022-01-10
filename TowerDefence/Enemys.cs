using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    public class Enemys:GameObject
    {
        public Vector2 positionV2;
        public float positionFloat;
        float nextFloatPosition;
        float rotation;
        public float speed;
        public float startSpeed;
        public Rectangle splineHitbox;
        public float rad;
        public int enemyHp;
        private int maxHealth;
        Rectangle hpRect;
        Rectangle hpbackDrop;
        public bool alive;
        public bool wasSlow;

        double startSlowTimer;
        double slowEffectDuration;

        bool frozen;

        public Enemys(Texture2D texture, Vector2 position, Rectangle HitBox):base(texture, position, HitBox)
        {
            speed = 3;
            startSpeed = speed;
            enemyHp = 20;
            alive = true;

            if (Game1.waveNum >= 4 || Game1.waveNum < 10 && Game1.waveNum !=5)
            {
                enemyHp = 25;
            }
            if(Game1.waveNum == 5)
            {
                speed = 6;
                enemyHp = 5;
            }
            if(Game1.waveNum == 10)
            {
                enemyHp = 50;
                speed = 5;
            }
            startSlowTimer = 0;
            slowEffectDuration = 1000;
            frozen = false;
            maxHealth = enemyHp;     
        }

        public override void Update()
        {
            positionFloat += speed;
            nextFloatPosition = positionFloat + 1;
            rotation  = (float)Math.Atan2(SplineManager.simplePath.GetPos(nextFloatPosition).Y - SplineManager.simplePath.GetPos(positionFloat).Y, SplineManager.simplePath.GetPos(nextFloatPosition).X - SplineManager.simplePath.GetPos(positionFloat).X);
            hitbox = new Rectangle((int)SplineManager.simplePath.GetPos(positionFloat).X - texture.Width / 2, (int)SplineManager.simplePath.GetPos(positionFloat).Y - texture.Height / 2, texture.Width+20, texture.Width+20);
            positionV2 = new Vector2((int)SplineManager.simplePath.GetPos(positionFloat).X , (int)SplineManager.simplePath.GetPos(positionFloat).Y);
            rad = hitbox.Width / 2;

            hpRect = new Rectangle((int)positionV2.X, (int)positionV2.Y-70, enemyHp, 10);
            hpbackDrop = new Rectangle((int)positionV2.X, (int)positionV2.Y - 70, maxHealth, 10);
            if (enemyHp <= 0)
            {
                alive = false;
                Game1.money += 15;
            }
            if (positionFloat >= SplineManager.simplePath.endT && alive)
            {
                Game1.lives--;
            }
        }

        public void TakeDamage()
        {
            enemyHp--;
        }
        public void SlowSpeed(GameTime gameTime)
        {

            if (Game1.waveNum <= 10)
            {
                speed = 1.5f;
                frozen = true;
            }
            if (Game1.waveNum >= 10)
            {
                speed = 3.5f;
                frozen = true;
            }
            
            wasSlow = true;
        }

        public void NoIce(GameTime gameTime)
        {
            startSlowTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (startSlowTimer >= slowEffectDuration)
            {
                startSlowTimer -= slowEffectDuration;
                speed = startSpeed;
                frozen = false;
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            if(Game1.waveNum == 5)
            {
                _spriteBatch.Draw(SpriteManager.TrojanIceTex, SplineManager.simplePath.GetPos(positionFloat), null, Color.Yellow, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 1f);

            }
            if (frozen)
            {
                _spriteBatch.Draw(SpriteManager.TrojanIceTex, SplineManager.simplePath.GetPos(positionFloat), null, Color.LightBlue, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 1f);
                
            }
            else
            {
                _spriteBatch.Draw(texture, SplineManager.simplePath.GetPos(positionFloat), null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 1f);
                //_spriteBatch.Draw(texture, hitbox, Color.Red);
            }
            _spriteBatch.Draw(SpriteManager.HPBarTex, hpbackDrop, Color.Black);
            _spriteBatch.Draw(SpriteManager.HPBarTex, hpRect, Color.White);
            
        }

       


    }
}
