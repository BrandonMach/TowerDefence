using Microsoft.Xna.Framework;
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
        public enum TowerSelect
        {
            None,
            Avast,
            NordVPN,
            Monkey, 
        }
        public static TowerSelect currentTowerSelected;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        NameMenu myForm1;

        RenderTarget2D renderTarget;
        GameObject gameObject;
        Projectile projectile;
      
        Towers avastSelected;
        Towers nordVPNSelected;
        public static List<GameObject> towersList;
        List<NordVPNTower> nordList;

        public static List<GameObject> projectileList;

        //HUD
        HUDManager hudManager;
        int hudWith = 200;
        public static int startingMoney;
        public static int money;
        public static int avastPlaceCost = 115;
        public static int nordVPNCost = 275;
        public static int waveNum = 0;
        WaveManager wavemanager;

        public static bool spawnWaves;

        Enemys enemys;
        public static List<Enemys> enemyList;
        Color backgroundColor;

        public  Rectangle rangeRect;
        
        float enemyPos; // move up the spline
        float nextEnemyPos;
        
        public bool isPaused;

        ParticleSystem particleSystem;
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

            hudManager = new HUDManager(Content);

            // TODO: use this.Content to load your game content here
            SpriteManager.LoadSprites(Content);
            //simplePath = new SimplePath(GraphicsDevice);
            _graphics.PreferredBackBufferWidth = SpriteManager.BackgroundTex.Width + hudWith;
            _graphics.PreferredBackBufferHeight = SpriteManager.BackgroundTex.Height;
            _graphics.ApplyChanges();

           
            towersList = new List<GameObject>();
            enemyList = new List<Enemys>();
            projectileList = new List<GameObject>();

            Debug.WriteLine(Window.ClientBounds.Width);
            Debug.WriteLine(Window.ClientBounds.Height);
            Debug.WriteLine(SpriteManager.BackgroundTex.Width);
            Debug.WriteLine(SpriteManager.BackgroundTex.Height);

            currentGameState = GameState.StartMenu;
            currentTowerSelected = TowerSelect.None;
            myForm1 = new NameMenu();

            SplineManager.LoadSpline(GraphicsDevice, Window);
            avastSelected = new AvastTower(SpriteManager.AvastTex, Vector2.Zero, new Rectangle(0,0, SpriteManager.AvastTex.Width, SpriteManager.AvastTex.Height), 150, 0,2);
            nordVPNSelected = new NordVPNTower(SpriteManager.NordVPNTex, Vector2.Zero, new Rectangle(0,0, SpriteManager.NordVPNTex.Width, SpriteManager.NordVPNTex.Height), 250, 0,5);

            startingMoney = 350;
            money = 0;
            wavemanager = new WaveManager();
            spawnWaves = false;

            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(SpriteManager.CircleParticle);
            textures.Add(SpriteManager.StarParticle);
            textures.Add(SpriteManager.DiamondParticle);
            particleSystem = new ParticleSystem(textures, new Vector2(400, 240));

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
                        money = startingMoney;
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
            KeyMouseReader.Update();
            //gameObject.Update();

            ClickInfo();
            hudManager.Update();

            particleSystem.EmitterLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            particleSystem.Update();

            if (KeyMouseReader.KeyPressed(Keys.Space))
            {
                spawnWaves = true;
                waveNum++;
                wavemanager.startSpawnDuration = 0;
                money += 150;
            }
            if (spawnWaves)
            {
                wavemanager.Update(gameTime);               
            }
            //if (KeyMouseReader.KeyPressed(Keys.S)) //Speed up enemys
            //{
            //    foreach (Enemys enemys in enemyList)
            //    {
            //        enemys.speed += 3;
            //    }
            //}
            foreach (Enemys enemys in enemyList)
            {
                enemys.Update();
            }


            //How many enemys alive
            Debug.WriteLine(enemyList.Count);

            avastSelected.Update();
            nordVPNSelected.Update();
            foreach (Projectile projectile in projectileList)
            {
                projectile.Update();

            }

            switch (currentTowerSelected)
            {
                case TowerSelect.None:
                    break;
                case TowerSelect.Avast:

                    avastSelected.Update();
                    //för alla gameobjects bredd och höjd i Mouse.GetState()
                    if (KeyMouseReader.LeftClick() && Mouse.GetState().X > 0 + avastSelected.texture.Width / 2 && Mouse.GetState().Y > 0 + avastSelected.texture.Height / 2 && Mouse.GetState().X < Window.ClientBounds.Width - avastSelected.texture.Width / 2 - hudWith && money >= avastPlaceCost)
                    {                      
                        if (CanPlace(avastSelected))
                        {
                            Vector2 newPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                            towersList.Add(new AvastTower(SpriteManager.AvastTex, newPosition, new Rectangle((int)newPosition.X - SpriteManager.AvastTex.Width / 2, (int)newPosition.Y - SpriteManager.AvastTex.Height / 2, SpriteManager.AvastTex.Width, SpriteManager.AvastTex.Height),150,0, 300));
                            Debug.WriteLine("placed");
                            currentTowerSelected = TowerSelect.None;
                            money -= avastPlaceCost;
                        }
                        else
                        {
                            currentTowerSelected = TowerSelect.None;
                        }
                    }
                    

                    break;
                case TowerSelect.NordVPN:
                     nordVPNSelected.Update();
                    //för alla gameobjects bredd och höjd i Mouse.GetState()
                    if (KeyMouseReader.LeftClick() && Mouse.GetState().X > 0 + nordVPNSelected.texture.Width / 2 && Mouse.GetState().Y > 0 + nordVPNSelected.texture.Height / 2 && Mouse.GetState().X < Window.ClientBounds.Width - nordVPNSelected.texture.Width / 2-hudWith && money >= nordVPNCost)
                    {       
                        if (CanPlace(nordVPNSelected))
                        {
                            Vector2 newPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

                            towersList.Add(new NordVPNTower(SpriteManager.NordVPNTex, newPosition, new Rectangle((int)newPosition.X - SpriteManager.NordVPNTex.Width / 2, (int)newPosition.Y - SpriteManager.NordVPNTex.Height / 2, SpriteManager.NordVPNTex.Width, SpriteManager.NordVPNTex.Height),300,0,1500));
                            
                            Debug.WriteLine("placed");
                            Debug.WriteLine("Mouse pos" +KeyMouseReader.mouseState.X + KeyMouseReader.mouseState.Y);
                            Debug.WriteLine("tower" + newPosition);
                            currentTowerSelected = TowerSelect.None;
                            money -= nordVPNCost;
                        }
                        else
                        {
                            currentTowerSelected = TowerSelect.None;
                        }
                    }
                    
                    // Debug.WriteLine(towersList.Count);
                    break;
                default:
                    break;
            }

           
            TowerDamage(gameTime);

            foreach (Enemys enemys in enemyList) 
            {
                foreach(Projectile projectile in projectileList)
                {
                    if (enemys.hitbox.Contains(projectile.hitbox))
                    {
                        projectile.alive = false;
                        projectileList.Remove(projectile);
                        break;
                    }
                }
            }

            if (KeyMouseReader.KeyPressed(Keys.K))
            {
                
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
            hudManager.Draw(_spriteBatch);

            particleSystem.Draw(_spriteBatch);
            foreach (Towers towers in towersList)
            {
                if (towers.infoClicked)
                {
                    rangeRect = new Rectangle((int)towers.pos.X , (int)towers.pos.Y ,  towers.rad*2 - SpriteManager.RangeRing.Width,  towers.rad*2- SpriteManager.RangeRing.Height);
                    _spriteBatch.Draw(SpriteManager.RangeRing, rangeRect, null, Color.Red, 0f, new Vector2(towers.texture.Width/4, towers.texture.Height/4), SpriteEffects.None, 1f);

                    if (towers is AvastTower && towers.infoClicked)
                    {
                        hudManager.currentInfo = HUDManager.TowerInfo.Avast;
                        if (hudManager.levelUpButtonRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick() && towers.level < towers.maxLevel)
                        {
                            if(towers.level == 1 && money >= towers.avastLevel1Cost)
                            {
                                money -= towers.avastLevel1Cost;
                                towers.level++;
                                towers.attackDelay = 250;
                            } 
                            if(towers.level == 2 && money >= towers.avastLevel2Cost)
                            {
                                money -= towers.avastLevel2Cost;
                                towers.level++;
                                towers.attackDelay = 200;
                            }
                            break;
                        }

                        if(hudManager.sellButtonRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                        {
                            int sellLevel1 = avastPlaceCost - 29;
                            int sellLevel2 = sellLevel1 + towers.avastLevel1Cost - 37;
                            int sellLevel3 = sellLevel2 + towers.avastLevel2Cost - 112;

                            SellPrices(towers, sellLevel1, sellLevel2, sellLevel3);

                            towersList.Remove(towers);
                            hudManager.currentInfo = HUDManager.TowerInfo.None;
                            break;
                        }
                    }
                    if (towers is NordVPNTower && towers.infoClicked)
                    {
                        Debug.WriteLine("Nord VPN tower");
                        hudManager.currentInfo = HUDManager.TowerInfo.Nord;


                        if (hudManager.levelUpButtonRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick() && towers.level < towers.maxLevel)
                        {
                            if (towers.level == 1 && money >= towers.NordLevel1Cost)
                            {
                                money -= towers.NordLevel1Cost;
                                towers.level++;
                                towers.attackDelay = 1250;

                            }
                            if (towers.level == 2 && money >= towers.NordLevel2Cost)
                            {
                                money -= towers.NordLevel2Cost;
                                towers.level++;
                                towers.attackDelay = 850;
                            }
                            break;
                        }
                        if (hudManager.sellButtonRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                        {
                            int sellLevel1 = nordVPNCost - 69;
                            int sellLevel2 = sellLevel1 + towers.NordLevel1Cost - 75;
                            int sellLevel3 = sellLevel2+ towers.NordLevel1Cost - 137;

                            SellPrices(towers, sellLevel1, sellLevel2, sellLevel3);
                            towersList.Remove(towers);
                            hudManager.currentInfo = HUDManager.TowerInfo.None;
                            break;
                        }
                    }
                }
            }
            foreach (Projectile projectile in projectileList)
            {
                projectile.Draw(_spriteBatch);
            }

            switch (currentTowerSelected)
            {
                case TowerSelect.None:
                    break;
                case TowerSelect.Avast:
                    //gameObject.Draw(_spriteBatch);
                    avastSelected.Draw(_spriteBatch);
                    break;
                case TowerSelect.NordVPN:
                    nordVPNSelected.Draw(_spriteBatch);
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
                    towers.infoClicked = true;
                }
                else if (!towers.hitbox.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick() && !hudManager.levelUpButtonRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y)&& KeyMouseReader.LeftClick() && !hudManager.sellButtonRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                {
                    towers.infoClicked = false;                
                    hudManager.currentInfo = HUDManager.TowerInfo.None;
                }
            }
        }
 

        public void TowerDamage(GameTime gameTime)
        {
            foreach (Towers towers in towersList)
            {
                foreach (Enemys enemys in enemyList)
                {
                    if (Vector2.Distance(towers.pos, enemys.positionV2) < (towers.rad))
                    {
                        Debug.WriteLine(enemys.positionV2);
                        //towers.StartAttack(gameTime, enemys, Vector2.Subtract(enemys.positionV2, towers.pos));

                        if (towers is AvastTower)
                        {
                            towers.StartAttack(gameTime, enemys, Vector2.Subtract(enemys.positionV2, towers.pos), SpriteManager.AvastProjectile);
                            
                            
                        }
                        if (towers is NordVPNTower)
                        {
                            enemys.SlowSpeed(gameTime);
                            towers.StartAttack(gameTime, enemys, Vector2.Subtract(enemys.positionV2, towers.pos), SpriteManager.SnowFlakeTex);
                        }                      
                    }
                    else if (enemys.wasSlow)
                    {
                        enemys.NoIce(gameTime);
                    }
                }
            }
        }

        public void SellPrices(Towers towers, int level1, int level2, int level3)
        {
            if (towers.level == 1)
            {
                money += level1; 
            }
            if (towers.level == 2)
            {
                money += level2;     
            }
            if (towers.level == 3)
            {
                money += level3;   
            }
        }
    }
}
