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
        Rectangle hpRect;
        public bool alive;
        public bool wasSlow;

        double startSlowTimer;
        double slowEffectDuration;

        public Enemys(Texture2D texture, Vector2 position, Rectangle HitBox):base(texture, position, HitBox)
        {

            speed = 3;
            startSpeed = speed;
            enemyHp = 20;
            alive = true;

            if(Game1.waveNum >= 5)
            {
                enemyHp = 25;
            }

            startSlowTimer = 0;
            slowEffectDuration = 1000;
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

            if(enemyHp <= 0)
            {
                alive = false;
                Game1.money += 25;
            }
            
        }

        public void TakeDamage()
        {
            enemyHp--;
        }
        public void SlowSpeed(GameTime gameTime)
        {
            speed = 1.5f;
            wasSlow = true;
        }

        public void NoIce(GameTime gameTime)
        {
            startSlowTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (startSlowTimer >= slowEffectDuration)
            {
                startSlowTimer -= slowEffectDuration;
                speed = startSpeed;
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {         

            if(speed > 1.5)
            {
                _spriteBatch.Draw(texture, SplineManager.simplePath.GetPos(positionFloat), null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 1f);
                //_spriteBatch.Draw(texture, hitbox, Color.Red);
            }
            else
            {
                _spriteBatch.Draw(SpriteManager.TrojanIceTex, SplineManager.simplePath.GetPos(positionFloat), null, Color.LightBlue, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 1f);
                
            }

            _spriteBatch.Draw(SpriteManager.HPBarTex, hpRect, Color.White);
            //_spriteBatch.Draw(texture, positionV2, Color.Yellow);
        }


    }
}
