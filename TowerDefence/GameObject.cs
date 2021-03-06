using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    public class GameObject
    {
        public Texture2D texture;
        public Vector2 pos;
        public Rectangle hitbox;
        public GameObject(Texture2D texture, Vector2 position, Rectangle HitBox)
        {
            this.texture = texture;
            this.pos = position;
            this.hitbox = HitBox;
        }
        public virtual void Update()
        {     
            hitbox = new Rectangle((int)pos.X - texture.Width / 2, (int)pos.Y - texture.Height / 2,texture.Width , texture.Height);
        }
        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, pos , null ,Color.White,0f,new Vector2(texture.Width/2,texture.Height/2),1f,SpriteEffects.None,1f);
        }
    }
}

