﻿using System;
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
        
            public static Texture2D BloonsMonkeyTex { get; private set; }


            public static void LoadSprites(ContentManager Content)
            {
                BloonsMonkeyTex = Content.Load<Texture2D>("bloonsMonkey");
            }
        
    }


}
