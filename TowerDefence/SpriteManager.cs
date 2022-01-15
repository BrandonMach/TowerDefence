using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TowerDefence
{
    class SpriteManager
    {
          
            public static Texture2D CustomTowerTex { get; private set; }
            public static Texture2D BackgroundTex { get; private set; }
            public static Texture2D TrojanTex { get; private set; }
            public static Texture2D TrojanIceTex { get; private set; }
            public static Texture2D BallTex { get; private set; }
            public static Texture2D AvastTex { get; private set; }       
            public static Texture2D PauseWindowTex { get; private set; }
            public static Texture2D RoadTex { get; private set; }
            public static Texture2D HPBarTex { get; private set; }
            public static Texture2D RangeRing { get; private set; }
            public static Texture2D NordVPNTex { get; private set; }
            public static Texture2D LevelUpButtonTex { get; private set; }
            public static Texture2D SellButtonTex { get; private set; }
            public static Texture2D RangePlusTex { get; private set; }
            public static Texture2D RangeMinusTex { get; private set; }
            public static Texture2D AttackSpdPlusTex { get; private set; }
            public static Texture2D AttackSpdMinusTex { get; private set; }
            public static Texture2D SnowFlakeTex { get; private set; }
            public static Texture2D AvastProjectile { get; private set; }
            public static Texture2D DollarSignParticle { get; private set; }
            public static Texture2D Cursor { get; private set; }
            public static Texture2D Heart { get; private set; }
            public static Texture2D GameOverTex { get; private set; }
            public static Texture2D WinTex { get; private set; }
            public static Texture2D UpdateBar { get; private set; } 
            public static Texture2D HideBlock { get; private set; } 
            public static Song StartUpTheme { get; private set; }
            public static Song MainTheme { get; private set; }
            public static SoundEffect PlacingSound { get; private set; }
            public static SoundEffect CashSound { get; private set; }
            public static SoundEffect DialUpSound { get; private set; }
            public static SoundEffect ErrorSound { get; private set; }
        public static void LoadSprites(ContentManager Content)
            {
                
                CustomTowerTex = Content.Load<Texture2D>("w98 logo");
                BackgroundTex = Content.Load<Texture2D>("wd95");
                TrojanTex = Content.Load<Texture2D>("trojanHorse");
                TrojanIceTex = Content.Load<Texture2D>("trojanHorseIce");
                BallTex = Content.Load<Texture2D>("BigDollar");
                AvastTex = Content.Load<Texture2D>("avastLogo");
                PauseWindowTex = Content.Load<Texture2D>("pauseTD");
                RoadTex = Content.Load<Texture2D>("road");
                HPBarTex = Content.Load<Texture2D>("hpRect");
                RangeRing = Content.Load <Texture2D>("rangeRing");
                NordVPNTex = Content.Load <Texture2D>("NVP");
                LevelUpButtonTex = Content.Load <Texture2D>("LevelUpButton");
                SnowFlakeTex = Content.Load <Texture2D>("snowflake");
                AvastProjectile = Content.Load <Texture2D>("avastProjectile");
                SellButtonTex = Content.Load <Texture2D>("sellButtton");
                RangePlusTex = Content.Load <Texture2D>("RangePlus");
                RangeMinusTex = Content.Load <Texture2D>("RangeMinus");
                AttackSpdPlusTex = Content.Load <Texture2D>("attckSpeedPlus");
                AttackSpdMinusTex = Content.Load <Texture2D>("attckSpeedMinus");
                DollarSignParticle = Content.Load<Texture2D>("DollarSign");
                Cursor = Content.Load<Texture2D>("fingerCursor");
                Heart = Content.Load<Texture2D>("marioHeart");
                GameOverTex = Content.Load<Texture2D>("GameOverTD");
                WinTex = Content.Load<Texture2D>("Updating");
                UpdateBar = Content.Load<Texture2D>("UpdateBar");
                HideBlock = Content.Load<Texture2D>("WhiteBlock");
                StartUpTheme = Content.Load<Song>("Microsoft Windows 95");
                MainTheme = Content.Load<Song>("Microsoft Windows 95 Passport");
                PlacingSound = Content.Load<SoundEffect>("PlacingSound");
                CashSound = Content.Load<SoundEffect>("Cash Register");
                DialUpSound = Content.Load<SoundEffect>("Dial Up");
                ErrorSound = Content.Load<SoundEffect>("Windows 95 - Error Sound");

                MediaPlayer.Volume = 0.1f;
                MediaPlayer.Play(StartUpTheme);
        }     
    }
}
