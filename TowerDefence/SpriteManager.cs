using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefence
{
    class SpriteManager
    {
        
            public static Texture2D bloonsMonkey;


            public static void LoadSprites(ContentManager Content)
            {
                bloonsMonkey = Content.Load<Texture2D>("bloonsMonkey");
            }
        
    }


}
