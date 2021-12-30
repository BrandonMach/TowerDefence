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
        
            public static Texture2D BloonsMonkeyTex { get; private set; }
            public static Texture2D BackgroundTex { get; private set; }
            public static Texture2D TrojanTex { get; private set; }
            public static Texture2D BallTex { get; private set; }
            public static  Texture2D AvastTex { get; private set; }
            
            public static Texture2D PauseWindowTex { get; private set; }
            public static Texture2D RoadTex { get; private set; }


        public static void LoadSprites(ContentManager Content)
            {
                BloonsMonkeyTex = Content.Load<Texture2D>("bloonsMonkey");
                BackgroundTex = Content.Load<Texture2D>("wd95");
                TrojanTex = Content.Load<Texture2D>("trojanHorse");
                BallTex = Content.Load<Texture2D>("ball");
                AvastTex = Content.Load<Texture2D>("avastLogo");
                PauseWindowTex = Content.Load<Texture2D>("pauseTD");
                RoadTex = Content.Load<Texture2D>("road");
            }
        
    }


}
