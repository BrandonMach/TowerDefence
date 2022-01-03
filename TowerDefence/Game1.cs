﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WinForm;

namespace TowerDefence
{
    public class Game1 : Game
    {
        enum GameState
        {
            StartMenu,
            Game,
            Pause,
            End,
        }
        GameState currentGameState;

        enum TowerSelect
        {
            None,
            Avast,
            Monkey, 
        }
        TowerSelect currentTowerSelected;



        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        NameMenu myForm1;

        RenderTarget2D renderTarget;
        GameObject gameObject;
        Towers avastSelected;
        Towers monkeySelected;
        List<GameObject> towersList;

        Enemys enemys;
        List<Enemys> enemyList;
        Color backgroundColor;

        public Rectangle rangeRect;
        

        //public SimplePath simplePath;
        float enemyPos; // move up the spline
        float nextEnemyPos;
        float enemyRotation;
        float roadBallPos;

        public bool isPaused;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundColor = new Color(0, 128, 128);
            
         

            // TODO: use this.Content to load your game content here
            SpriteManager.LoadSprites(Content);
            //simplePath = new SimplePath(GraphicsDevice);
            _graphics.PreferredBackBufferWidth = SpriteManager.BackgroundTex.Width ;
            _graphics.PreferredBackBufferHeight = SpriteManager.BackgroundTex.Height;
            _graphics.ApplyChanges();

           
            towersList = new List<GameObject>();
            enemyList = new List<Enemys>();

            Debug.WriteLine(Window.ClientBounds.Width);
            Debug.WriteLine(Window.ClientBounds.Height);
            Debug.WriteLine(SpriteManager.BackgroundTex.Width);
            Debug.WriteLine(SpriteManager.BackgroundTex.Height);

            currentGameState = GameState.StartMenu;
            currentTowerSelected = TowerSelect.None;
            myForm1 = new NameMenu();

            SplineManager.LoadSpline(GraphicsDevice, Window);
            avastSelected = new AvastTower(SpriteManager.AvastTex, Vector2.Zero, new Rectangle(0,0, SpriteManager.AvastTex.Width, SpriteManager.AvastTex.Height), 3, 0,2);
            monkeySelected = new Towers(SpriteManager.BloonsMonkeyTex, Vector2.Zero, new Rectangle(0,0, SpriteManager.BloonsMonkeyTex.Width, SpriteManager.BloonsMonkeyTex.Height), 5, 0,5);

            roadBallPos = SplineManager.simplePath.beginT;



            renderTarget = new RenderTarget2D(GraphicsDevice,Window.ClientBounds.Width+300, Window.ClientBounds.Height+300);
            DrawOnRenderTarget();


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                Exit();
         
            switch (currentGameState)
            { 
                case GameState.StartMenu:
                   
                    if (myForm1.PlayerName != "")
                    {
                        Window.Title = "Player: " + myForm1.PlayerName;
                        currentGameState = GameState.Game;
                        isPaused = false;
                    }
                    break;
                case GameState.Game:                  
                    if(!isPaused)
                    {
                        GameUpdate(gameTime);
                    }                  
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                    {
                        currentGameState = GameState.Pause;
                        isPaused = true;                    
                    }
                    break;
                case GameState.Pause:
                    KeyMouseReader.Update();
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                    {
                        currentGameState = GameState.Game;
                        isPaused = false;
                    }
                    break;
                case GameState.End:
                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }


        public void GameUpdate(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                Exit();


            // TODO: Add your update logic here
            enemyPos += 5;
            nextEnemyPos = enemyPos + 1;
            enemyRotation = (float)Math.Atan2(SplineManager.simplePath.GetPos(nextEnemyPos).Y - SplineManager.simplePath.GetPos(enemyPos).Y, SplineManager.simplePath.GetPos(nextEnemyPos).X - SplineManager.simplePath.GetPos(enemyPos).X);


            KeyMouseReader.Update();
            //gameObject.Update();

            if (KeyMouseReader.KeyPressed(Keys.D1))
            {
                currentTowerSelected = TowerSelect.Avast;

            }
            else if (KeyMouseReader.KeyPressed(Keys.D2))
            {
                currentTowerSelected = TowerSelect.Monkey;

            }
            else if (KeyMouseReader.KeyPressed(Keys.L))
            {
                enemys = new Enemys(SpriteManager.TrojanTex, Vector2.Zero, new Rectangle(0, 0, SpriteManager.TrojanTex.Width, SpriteManager.TrojanTex.Height));
                enemyList.Add(enemys);
               // Debug.WriteLine(enemyList.Count);
            }

            if (KeyMouseReader.KeyPressed(Keys.S)) //Speed up enemys
            {
                foreach (Enemys enemys in enemyList)
                {
                    enemys.speed += 3;
                }
            }
            foreach (Enemys enemys in enemyList)
            {
                enemys.Update();
            }


            //How many enemys alive
            Debug.WriteLine(enemyList.Count);

            

            switch (currentTowerSelected)
            {
                case TowerSelect.None:
                    break;
                case TowerSelect.Avast:

                    avastSelected.Update();
                    //för alla gameobjects bredd och höjd i Mouse.GetState()
                    if (KeyMouseReader.LeftClick() && Mouse.GetState().X > 0 + avastSelected.texture.Width / 2 && Mouse.GetState().Y > 0 + avastSelected.texture.Height / 2 && Mouse.GetState().X < Window.ClientBounds.Width - avastSelected.texture.Width / 2)
                    {
                       
                        if (CanPlace(avastSelected))
                        {
                            Vector2 newPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                            


                            towersList.Add(new AvastTower(SpriteManager.AvastTex, newPosition, new Rectangle((int)newPosition.X - SpriteManager.AvastTex.Width / 2, (int)newPosition.Y - SpriteManager.AvastTex.Height / 2, SpriteManager.AvastTex.Width, SpriteManager.AvastTex.Height),4, 0 , 0.05));
                            Debug.WriteLine("placed");
                        }
                    }
                   // Debug.WriteLine(towersList.Count);


                    break;
                case TowerSelect.Monkey:
                    monkeySelected.Update();
                    //för alla gameobjects bredd och höjd i Mouse.GetState()
                    if (KeyMouseReader.LeftClick() && Mouse.GetState().X > 0 + monkeySelected.texture.Width / 2 && Mouse.GetState().Y > 0 + monkeySelected.texture.Height / 2 && Mouse.GetState().X < Window.ClientBounds.Width - monkeySelected.texture.Width / 2)
                    {
                        
                        if (CanPlace(monkeySelected))
                        {
                            Vector2 newPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                           

                            //dadakdnandjadadnadad
                            towersList.Add(new Towers(SpriteManager.BloonsMonkeyTex, newPosition, new Rectangle((int)newPosition.X - SpriteManager.BloonsMonkeyTex.Width / 2, (int)newPosition.Y - SpriteManager.BloonsMonkeyTex.Height / 2, SpriteManager.BloonsMonkeyTex.Width, SpriteManager.BloonsMonkeyTex.Height),5,0,0.1));
                            Debug.WriteLine("placed");
                        }
                    }
                   // Debug.WriteLine(towersList.Count);
                    break;
                default:
                    break;
            }


            ClickInfo();


            foreach (Towers towers in towersList)
            {
                rangeRect = new Rectangle((int)towers.pos.X-SpriteManager.RangeRing.Width*2, (int)towers.pos.Y - SpriteManager.RangeRing.Height*2, SpriteManager.RangeRing.Width * towers.rad, SpriteManager.RangeRing.Height * towers.rad);

                foreach (Enemys enemys in enemyList)
                {
                    if (rangeRect.Intersects(enemys.hitbox))
                    {
                        //if (Vector2.Distance(towers.pos, enemys.positionV2) < (towers.rad + enemys.rad))
                        //{
                        towers.startAttackTimer += gameTime.ElapsedGameTime.TotalSeconds;
                        if(towers.startAttackTimer >= towers.attackDelay)
                        {
                            towers.startAttackTimer -= towers.attackDelay;
                            Debug.WriteLine("StartTimer" + towers.startAttackTimer);
                            Debug.WriteLine("attack delay " + towers.attackDelay);
                            Debug.WriteLine("Enemy Hp: " + enemys.enemyHp);
                            Debug.WriteLine("Enemy in range");
                            enemys.enemyHp--;
                            
                            break;

                        }



                        //}
                    }

                   
                }
              
            }



            DrawOnRenderTarget(); ///dasdadasad
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            
            switch (currentGameState)
            {
                case GameState.StartMenu:
                    myForm1.Show();
                    break;
                case GameState.Game:
                    GameDraw(gameTime);
                    break;
                case GameState.Pause:
                    GameDraw(gameTime);
                    _spriteBatch.Draw(SpriteManager.PauseWindowTex, new Vector2(SpriteManager.PauseWindowTex.Width/2, SpriteManager.PauseWindowTex.Height), Color.White);
                    break;
                case GameState.End:
                    break;
                default:
                    break;
            }


           

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void GameDraw(GameTime gameTime)
        {
            _spriteBatch.Draw(SpriteManager.BackgroundTex, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

           




            _spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);

            foreach (Towers towers in towersList)
            {
                if (towers.infoClicked)
                {
                    rangeRect = new Rectangle((int)towers.pos.X - SpriteManager.RangeRing.Width * 2, (int)towers.pos.Y - SpriteManager.RangeRing.Height * 2, SpriteManager.RangeRing.Width * towers.rad, SpriteManager.RangeRing.Height * towers.rad);

                    _spriteBatch.Draw(SpriteManager.RangeRing, rangeRect, null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 1f);

                }
            }


         


            switch (currentTowerSelected)
            {
                case TowerSelect.None:
                    break;
                case TowerSelect.Avast:
                    //gameObject.Draw(_spriteBatch);
                    avastSelected.Draw(_spriteBatch);


                    break;
                case TowerSelect.Monkey:
                    monkeySelected.Draw(_spriteBatch);


                    break;
                default:
                    break;
            }

        }

        private void DrawOnRenderTarget()
        {

            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            _spriteBatch.Begin();


            //RoadTexture
            //for (int i = 0; i < SplineManager.simplePath.endT; i += 10)
            //{
            //    Vector2 roadBallVector = SplineManager.simplePath.GetPos(roadBallPos + i);
            //    _spriteBatch.Draw(SpriteManager.RoadTex, roadBallVector, null, new Color(0, 204, 0, 1), 0f, new Vector2(SpriteManager.RoadTex.Width / 2, SpriteManager.RoadTex.Height / 2), 1f, SpriteEffects.None, 0f);
            //}


            // Spine ritas ut i rendertarget
            SplineManager.simplePath.Draw(_spriteBatch);
            //SplineManager.simplePath.DrawPoints(_spriteBatch);


            foreach (Towers twrs in towersList)
            {
                twrs.Draw(_spriteBatch);
            }

           


            foreach (Enemys enemys in enemyList)
            {
                if (!(enemys.positionFloat >= SplineManager.simplePath.endT) && enemys.alive)
                {

                    enemys.Draw(_spriteBatch);
                   
                   // _spriteBatch.Draw(SpriteManager.TrojanTex, SplineManager.simplePath.GetPos(enemys.positionFloat), null, Color.White, enemyRotation, new Vector2(SpriteManager.TrojanTex.Width / 2, SpriteManager.TrojanTex.Height / 2), 1f, SpriteEffects.None, 1f);

                }
                else
                {
                    enemyList.Remove(enemys);
                    break;
                }
               
            }
          
            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
          
        }

        public bool CanPlace(Towers g)
        {
            Color[] pixels = new Color[g.texture.Width * g.texture.Height];
            Color[] pixels2 = new Color[g.texture.Width * g.texture.Height];
            g.texture.GetData<Color>(pixels2);
            renderTarget.GetData(0, g.hitbox, pixels, 0, pixels.Length);
            for (int i = 0; i < pixels.Length; ++i)
            {
                if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f) 
                {
                    return false;
                }
                    
            }
            return true;
        }

        public void ClickInfo()
        {
            foreach (Towers towers in towersList)
            {
                if (towers.hitbox.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                {
                    
                    Debug.WriteLine("Yoasdadaodaodmamd");
                    towers.infoClicked = true;

                }
                else if (!towers.hitbox.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                {
                    towers.infoClicked = false;
                }
            }

           
        }


    }
}
