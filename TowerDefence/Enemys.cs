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
        public Rectangle splineHitbox;
        public float rad;
        public int enemyHp;
        Rectangle hpRect;
        public bool alive;

        public Enemys(Texture2D texture, Vector2 position, Rectangle HitBox):base(texture, position, HitBox)
        {

            speed = 3;
            enemyHp = 50;
            alive = true;
        }

        public override void Update()
        {
            positionFloat += speed;
            nextFloatPosition = positionFloat + 1;
            rotation  = (float)Math.Atan2(SplineManager.simplePath.GetPos(nextFloatPosition).Y - SplineManager.simplePath.GetPos(positionFloat).Y, SplineManager.simplePath.GetPos(nextFloatPosition).X - SplineManager.simplePath.GetPos(positionFloat).X);
            hitbox = new Rectangle((int)SplineManager.simplePath.GetPos(positionFloat).X - texture.Width / 2, (int)SplineManager.simplePath.GetPos(positionFloat).Y - texture.Height / 2, texture.Width, texture.Width);
            positionV2 = new Vector2((int)SplineManager.simplePath.GetPos(positionFloat).X - texture.Width / 2, (int)SplineManager.simplePath.GetPos(positionFloat).Y - texture.Height / 2);
            rad = hitbox.Width / 2;


            hpRect = new Rectangle((int)positionV2.X, (int)positionV2.Y-30, enemyHp, 10);

            if(enemyHp <= 0)
            {
                alive = false;
            }
            if(speed<= 1)
            {
                speed = 1;
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {         
                _spriteBatch.Draw(texture, SplineManager.simplePath.GetPos(positionFloat), null, Color.Blue, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 1f);
                _spriteBatch.Draw(texture, hitbox, Color.Red);
                _spriteBatch.Draw(SpriteManager.HPBarTex, hpRect, Color.White);  
        }


    }
}
