using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
            MainMenu,
            MapCreate,
            TowerCreate,
            Pause,
            GameOver,
            Win,
        }
        GameState currentGameState;
        public enum TowerSelect
        {
            None,
            Avast,
            NordVPN,
            CustomTower, 
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
        Towers customSelected;
        public static List<GameObject> towersList;
        List<NordVPNTower> nordList;

        public static List<GameObject> projectileList;

        //CustomTowerCreator
        int customRad;
        int customAttackSpd;
        public static int customTowerCost;
        int customRadIncrease;

        int pointToMoveIndex;
        bool movingPoint;
        Rectangle rangePlusRect;
        private Rectangle rangeMinusRect;
        private Rectangle attackSpdPlus;
        private Rectangle attackSpdMinus;
        Color settingsColor;

        //HUD
        HUDManager hudManager;
        int hudWith = 200;
        public static int startingMoney;
        public static int money;
        public static int avastPlaceCost = 115;
        public static int nordVPNCost = 275;
        public static int waveNum = 0;
        WaveManager wavemanager;

        Vector2 mousePosCursor;
        
        //Win Screen
        Vector2 updatingPos;
        int bars;
        double startUpdating;
        double startUpdateOneBar;
        double updatingDuration;
        double updatingFinished;
        SpriteFont score_Font;
        int totalMoneySpent;
        int maxlives;

        public static bool spawnWaves;

        public static List<Enemys> enemyList;
        Color backgroundColor;

        public  Rectangle rangeRect;      
        
        public bool isPaused;

        ParticleSystem particleSystem;
        bool startParticleUpdate;

        double startParticles = 0;
        double particlesDuration = 0.25;
        public static int lives;
        private Rectangle gameLogoRect;
        private Rectangle mapMakerRect;
        private Rectangle customTowerRect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundColor = new Color(0, 128, 128);
            settingsColor = new Color(192, 192, 192);

            hudManager = new HUDManager(Content);

            SpriteManager.LoadSprites(Content);
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
            customSelected=  new CustomTower(SpriteManager.CustomTowerTex, new Vector2(400,400), new Rectangle(0,0, SpriteManager.CustomTowerTex.Width, SpriteManager.CustomTowerTex.Height), customRad, 0, customAttackSpd);
            customAttackSpd = 275;
            customRad = 100;
            customRadIncrease = 0;


            startingMoney = 350;
            money = 0;
            wavemanager = new WaveManager();
            spawnWaves = false;

            lives = 10;
            maxlives = 10;

            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(SpriteManager.BallTex);
            textures.Add(SpriteManager.BallTex);
            textures.Add(SpriteManager.BallTex);
            
            particleSystem = new ParticleSystem(textures, new Vector2(400, 240));
            startParticleUpdate = false;

            updatingPos = new Vector2(Window.ClientBounds.Width / 2 - SpriteManager.WinTex.Width, 100);
            bars = 0;
            startUpdating = 0;
            startUpdateOneBar = 0;
            updatingDuration = .5;
            updatingFinished = 20;
            score_Font = Content.Load<SpriteFont>("score_Font");

            renderTarget = new RenderTarget2D(GraphicsDevice,Window.ClientBounds.Width+300, Window.ClientBounds.Height+300);
            DrawOnRenderTarget();
        }

        protected override void Update(GameTime gameTime)
        {   
            switch (currentGameState)
            { 
                case GameState.StartMenu:
                    if (myForm1.PlayerName != "")
                    {
                        Window.Title = "Player: " + myForm1.PlayerName;
                        currentGameState = GameState.MainMenu;
                        isPaused = false;
                        money = startingMoney;
                        MediaPlayer.Play(SpriteManager.MainTheme);
                        MediaPlayer.IsRepeating = true;
                    }
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                        Exit();
                    break;
                case GameState.MainMenu:
                    KeyMouseReader.Update();
                    gameLogoRect = new Rectangle(480, 220, SpriteManager.GameIconTex.Width +30, SpriteManager.GameIconTex.Height + 30);
                    mapMakerRect = new Rectangle(800,300 , SpriteManager.MapManagerTex.Width, SpriteManager.MapManagerTex.Height);
                    customTowerRect = new Rectangle(800,300 + SpriteManager.MapManagerTex.Height + 10, SpriteManager.MapManagerTex.Width, SpriteManager.MapManagerTex.Height);

                    if (mapMakerRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                    {
                        currentGameState = GameState.MapCreate;
                    }
                    if (customTowerRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                    {
                        currentGameState = GameState.TowerCreate;
                    }
                    if (gameLogoRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                    {
                        currentGameState = GameState.Game;
                    }

                    break;
                case GameState.MapCreate:
                    KeyMouseReader.Update();
                    if (KeyMouseReader.mouseState.LeftButton != ButtonState.Pressed)
                    {
                        movingPoint = false;
                    }
                    if (KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed && KeyMouseReader.mouseState.X < Window.ClientBounds.Width - hudWith)
                    {
                        if (!movingPoint)
                        {
                            int i = 0;
                            foreach (var point in SplineManager.pointList)
                            {
                                Rectangle pointHitbox = new Rectangle((int)point.X - 10, (int)point.Y - 10, 20, 20);
                                if (pointHitbox.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y))
                                {
                                    pointToMoveIndex = i;
                                    movingPoint = true;
                                }
                                i++;
                            }
                        }
                        if (movingPoint == true)
                        {
                            SplineManager.simplePath.SetPos(pointToMoveIndex, new Vector2(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y));
                        }
                    }
                    
                    if (KeyMouseReader.KeyPressed(Keys.Back))
                    {
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                case GameState.TowerCreate:
                    KeyMouseReader.Update();
                    rangePlusRect = new Rectangle(620, 670, SpriteManager.RangePlusTex.Width, SpriteManager.RangePlusTex.Height);
                    rangeMinusRect = new Rectangle(620, 670 + SpriteManager.RangeMinusTex.Height + 10, SpriteManager.RangeMinusTex.Width, SpriteManager.RangeMinusTex.Height);
                    attackSpdPlus = new Rectangle(620 + SpriteManager.RangePlusTex.Width + 10, 670, SpriteManager.AttackSpdPlusTex.Width, SpriteManager.AttackSpdPlusTex.Height);
                    attackSpdMinus = new Rectangle(620 + SpriteManager.RangePlusTex.Width + 10, 670 + SpriteManager.AttackSpdMinusTex.Height + 10, SpriteManager.AttackSpdMinusTex.Width, SpriteManager.AttackSpdMinusTex.Height);
                    if ( rangePlusRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && customRadIncrease <=6 && KeyMouseReader.LeftClick())
                    {
                        customRad += 25;
                        customTowerCost += 75;
                        customRadIncrease += 1;
                        
                    }
                    if (rangeMinusRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                    {
                        if (customRad <= 100)
                        {
                            customRad = 100;
                            customTowerCost = 0;
                        }
                        else
                        {
                            customRadIncrease -= 1;
                            customRad -= 25;
                            customTowerCost -= 75;
                        }
                    }
                    if(attackSpdPlus.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && customRadIncrease <= 6 && KeyMouseReader.LeftClick())
                    {
                        customAttackSpd -= 15;
                        customTowerCost += 75;
                        customRadIncrease += 1;
                    } 
                    if(attackSpdMinus.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                    {
                        if (customAttackSpd >= 275)
                        {
                            customAttackSpd = 275;
                        }
                        else
                        {
                            customRadIncrease -= 1;
                            customAttackSpd += 15;
                            customTowerCost -= 75;
                        }
                    }

                    rangeRect = new Rectangle((int)customSelected.pos.X, (int)customSelected.pos.Y, customRad * 2 - SpriteManager.RangeRing.Width, customRad* 2 - SpriteManager.RangeRing.Height);
                    if (KeyMouseReader.KeyPressed(Keys.Back))
                    {
                        currentGameState = GameState.MainMenu;
                    }
                    break;

                case GameState.Game:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                        Exit();
                    if (!isPaused)
                    {
                        GameUpdate(gameTime);
                    }                  
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                    {
                        currentGameState = GameState.Pause;
                        SpriteManager.ErrorSound.Play();
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
                case GameState.GameOver:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                        Exit();
                    MediaPlayer.Stop();
                    KeyMouseReader.Update();
                    break;
                case GameState.Win:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                        Exit();
                    MediaPlayer.Stop();
                    KeyMouseReader.Update();
                    startUpdateOneBar += gameTime.ElapsedGameTime.TotalSeconds;
                    startUpdating += gameTime.ElapsedGameTime.TotalSeconds;

                    if (startUpdating < updatingFinished && bars < 26)
                    {                    
                        if (startUpdateOneBar >= updatingDuration)
                        {
                            startUpdateOneBar -= updatingDuration;
                            bars++;
                        }
                    }
                    break;
                default:
                    break;
            }
            mousePosCursor = new Vector2(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y);
            base.Update(gameTime);
        }
        public void GameUpdate(GameTime gameTime)
        {
         
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                Exit();
          
            KeyMouseReader.Update();

            ClickInfo();
            hudManager.Update();
            if (KeyMouseReader.KeyPressed(Keys.D9) )
            {
                currentGameState = GameState.GameOver;
            }
            if (KeyMouseReader.KeyPressed(Keys.D0) )
            {
                currentGameState = GameState.Win;
                SpriteManager.DialUpSound.Play();
            }

            if (KeyMouseReader.KeyPressed(Keys.Space) && enemyList.Count == 0)
            {
                spawnWaves = true;
                waveNum++;      
                money += 150;
                wavemanager.startSpawnDuration = 0;
            }
            if (waveNum == 10 && enemyList.Count == 0 && !spawnWaves)
            {
                Debug.WriteLine("gameOver");
                currentGameState = GameState.Win;
                SpriteManager.DialUpSound.Play();
            }
            if (spawnWaves)
            {
                wavemanager.Update(gameTime);               
            }
            foreach (Enemys enemys in enemyList)
            {
                enemys.Update();
            }

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
                    if (KeyMouseReader.LeftClick() && Mouse.GetState().X > 0 + avastSelected.texture.Width / 2 && Mouse.GetState().Y > 0 + avastSelected.texture.Height / 2 && Mouse.GetState().X < Window.ClientBounds.Width - avastSelected.texture.Width / 2 - hudWith && money >= avastPlaceCost)
                    {                      
                        if (CanPlace(avastSelected))
                        {
                            totalMoneySpent += avastPlaceCost;
                            Vector2 newPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                            towersList.Add(new AvastTower(SpriteManager.AvastTex, newPosition, new Rectangle((int)newPosition.X - SpriteManager.AvastTex.Width / 2, (int)newPosition.Y - SpriteManager.AvastTex.Height / 2, SpriteManager.AvastTex.Width, SpriteManager.AvastTex.Height),150,0, 400));
                            currentTowerSelected = TowerSelect.None;
                            money -= avastPlaceCost;
                            SpriteManager.PlacingSound.Play();
                        }
                        else
                        {
                            currentTowerSelected = TowerSelect.None;
                        }
                    }
                    break;
                case TowerSelect.NordVPN:
                     nordVPNSelected.Update();
                    if (KeyMouseReader.LeftClick() && Mouse.GetState().X > 0 + nordVPNSelected.texture.Width / 2 && Mouse.GetState().Y > 0 + nordVPNSelected.texture.Height / 2 && Mouse.GetState().X < Window.ClientBounds.Width - nordVPNSelected.texture.Width / 2-hudWith && money >= nordVPNCost)
                    {       
                        if (CanPlace(nordVPNSelected))
                        {
                            totalMoneySpent += nordVPNCost;
                            Vector2 newPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                            towersList.Add(new NordVPNTower(SpriteManager.NordVPNTex, newPosition, new Rectangle((int)newPosition.X - SpriteManager.NordVPNTex.Width / 2, (int)newPosition.Y - SpriteManager.NordVPNTex.Height / 2, SpriteManager.NordVPNTex.Width, SpriteManager.NordVPNTex.Height),300,0,1500));   
                            currentTowerSelected = TowerSelect.None;
                            money -= nordVPNCost;
                            SpriteManager.PlacingSound.Play();
                        }
                        else
                        {
                            currentTowerSelected = TowerSelect.None;
                        }
                    }
                    break;
                case TowerSelect.CustomTower:
                    customSelected.Update();
                    if (KeyMouseReader.LeftClick() && Mouse.GetState().X > 0 + customSelected.texture.Width / 2 && Mouse.GetState().Y > 0 + customSelected.texture.Height / 2 && Mouse.GetState().X < Window.ClientBounds.Width - customSelected.texture.Width / 2 - hudWith && money >= customTowerCost)
                    {
                        if (CanPlace(customSelected))
                        {
                            totalMoneySpent += customTowerCost;
                            Vector2 newPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                            towersList.Add(new CustomTower(SpriteManager.CustomTowerTex, newPosition, new Rectangle((int)newPosition.X - SpriteManager.CustomTowerTex.Width / 2, (int)newPosition.Y - SpriteManager.CustomTowerTex.Height / 2, SpriteManager.CustomTowerTex.Width, SpriteManager.CustomTowerTex.Height), customRad, 0, customAttackSpd));
                            currentTowerSelected = TowerSelect.None;
                            money -= customTowerCost;
                            SpriteManager.PlacingSound.Play();
                        }
                        else
                        {
                            currentTowerSelected = TowerSelect.None;
                        }
                    }
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
          
            if (startParticleUpdate)
            {
                startParticles += gameTime.ElapsedGameTime.TotalSeconds;
                particleSystem.Update();

                if (startParticles >= particlesDuration)
                {
                    startParticles -= particlesDuration;
                    startParticleUpdate = false;
                }
            }
            DrawOnRenderTarget(); 
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);
            _spriteBatch.Begin();         
            switch (currentGameState)
            {
                case GameState.StartMenu:
                    myForm1.Show();
                    break;
                case GameState.Game:
                    GameDraw(gameTime);             
                    break;
                case GameState.MainMenu:
                    _spriteBatch.Draw(SpriteManager.MainMenuTex, new Rectangle(0,100, Window.ClientBounds.Width, Window.ClientBounds.Height -100), Color.White);
                    _spriteBatch.Draw(SpriteManager.MapManagerTex, mapMakerRect, Color.White);
                    _spriteBatch.Draw(SpriteManager.CustomTowerMakerTex, customTowerRect, Color.White);
                    if(gameLogoRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y))
                    {
                        _spriteBatch.Draw(SpriteManager.GameIconTex, gameLogoRect, Color.Gray);
                    }
                    else
                    {
                        _spriteBatch.Draw(SpriteManager.GameIconTex, gameLogoRect, Color.White);
                    }         
                    break;
                case GameState.MapCreate:
                    SplineManager.simplePath.Draw(_spriteBatch);
                    SplineManager.simplePath.DrawPoints(_spriteBatch);

                    break;
                case GameState.TowerCreate:
                    GraphicsDevice.Clear(settingsColor);
                    customSelected.Draw(_spriteBatch);

                    _spriteBatch.Draw(SpriteManager.RangeRing, rangeRect, null, Color.Red, 0f, new Vector2(customSelected.texture.Width / 4, customSelected.texture.Height / 4+2 ), SpriteEffects.None, 1f);
                    _spriteBatch.DrawString(score_Font, "$" + customTowerCost, new Vector2(620, 550), Color.Gold);
                    _spriteBatch.DrawString(score_Font, "Attack Speed: " + customAttackSpd, new Vector2(620, 600), Color.Gold);
                    _spriteBatch.Draw(SpriteManager.RangePlusTex, rangePlusRect, Color.White);
                    _spriteBatch.Draw(SpriteManager.RangeMinusTex, rangeMinusRect, Color.White);
                    _spriteBatch.Draw(SpriteManager.AttackSpdPlusTex, attackSpdPlus, Color.White);
                    _spriteBatch.Draw(SpriteManager.AttackSpdMinusTex, attackSpdMinus, Color.White);



                    break;
                case GameState.Pause:
                    GameDraw(gameTime);
                    _spriteBatch.Draw(SpriteManager.PauseWindowTex, new Vector2(SpriteManager.PauseWindowTex.Width/2, SpriteManager.PauseWindowTex.Height), Color.White);
                    break;
                case GameState.GameOver:           
                    GraphicsDevice.Clear(backgroundColor);
                    _spriteBatch.Draw(SpriteManager.GameOverTex, Vector2.Zero, Color.White);
                    break;
                case GameState.Win:

                    Vector2 playerNamePos = new Vector2(400, 350);
                    Vector2 moneySpentPos = new Vector2(400, 400);
                    Vector2 livesLostPos = new Vector2(400, 450);
                    _spriteBatch.Draw(SpriteManager.WinTex, updatingPos, Color.White);
                  
                    _spriteBatch.DrawString(score_Font, "Name: " + myForm1.PlayerName,playerNamePos, Color.White);
                    _spriteBatch.DrawString(score_Font, "Total Money Spent: " + totalMoneySpent, moneySpentPos, Color.White);
                    _spriteBatch.DrawString(score_Font, "Total Lives Lost :" + (maxlives - lives),livesLostPos, Color.White);
                    _spriteBatch.Draw(SpriteManager.HideBlock, new Rectangle((int)updatingPos.X -10 , (int)updatingPos.Y + SpriteManager.WinTex.Height + bars*10, SpriteManager.WinTex.Width + 20, (SpriteManager.WinTex.Height)), backgroundColor);
                    //Box som täcker score
                    for (int i = 0; i < bars; i++)
                    {                
                        _spriteBatch.Draw(SpriteManager.UpdateBar, new Rectangle((int)updatingPos.X + 57 + (SpriteManager.UpdateBar.Width + 3) * i, (int)updatingPos.Y + 168, SpriteManager.UpdateBar.Width, SpriteManager.UpdateBar.Height), Color.White);    
                    }
                    break;
                default:
                    break;          
            }
            _spriteBatch.Draw(SpriteManager.Cursor, mousePosCursor, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void GameDraw(GameTime gameTime)
        {          
            _spriteBatch.Draw(SpriteManager.BackgroundTex, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            _spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            hudManager.Draw(_spriteBatch);
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
                                towers.rad = towers.avastLvl2Rad;
                                totalMoneySpent += towers.avastLevel1Cost;
                                SpawnParticle();                               
                            } 
                            if(towers.level == 2 && money >= towers.avastLevel2Cost)
                            {
                                money -= towers.avastLevel2Cost;
                                towers.level++;
                                towers.attackDelay = towers.avastLvl3Att;
                                totalMoneySpent += towers.avastLevel2Cost;
                                SpawnParticle();
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
                                towers.attackDelay = towers.nordLvl2Att;
                                totalMoneySpent += towers.NordLevel1Cost;
                                SpawnParticle();
                            }
                            if (towers.level == 2 && money >= towers.NordLevel2Cost)
                            {
                                money -= towers.NordLevel2Cost;
                                towers.level++;
                                towers.attackDelay = towers.nordLvl3Att;
                                totalMoneySpent += towers.NordLevel2Cost;
                                SpawnParticle();
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

                    if (towers is CustomTower && towers.infoClicked)
                    {
                        Debug.WriteLine("Nord VPN tower");
                        hudManager.currentInfo = HUDManager.TowerInfo.Nord;
                        if (hudManager.levelUpButtonRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick() && towers.level < towers.maxLevel)
                        {
                            if (towers.level == 1 && money >= towers.NordLevel1Cost)
                            {
                                money -= towers.NordLevel1Cost;
                                towers.level++;
                                towers.attackDelay = towers.nordLvl2Att;
                                totalMoneySpent += towers.NordLevel1Cost;
                                SpawnParticle();
                            }
                            if (towers.level == 2 && money >= towers.NordLevel2Cost)
                            {
                                money -= towers.NordLevel2Cost;
                                towers.level++;
                                towers.attackDelay = towers.nordLvl3Att;
                                totalMoneySpent += towers.NordLevel2Cost;
                                SpawnParticle();
                            }
                            break;
                        }
                        if (hudManager.sellButtonRect.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
                        {
                            int sellLevel1 = nordVPNCost - 69;
                            int sellLevel2 = sellLevel1 + towers.NordLevel1Cost - 75;
                            int sellLevel3 = sellLevel2 + towers.NordLevel1Cost - 137;
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
            if (startParticleUpdate)
            {
                particleSystem.Draw(_spriteBatch);
            }
            switch (currentTowerSelected)
            {
                case TowerSelect.None:
                    break;
                case TowerSelect.Avast:
                    avastSelected.Draw(_spriteBatch);
                    break;
                case TowerSelect.NordVPN:
                    nordVPNSelected.Draw(_spriteBatch);
                    break; 
                case TowerSelect.CustomTower:
                    customSelected.Draw(_spriteBatch);
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

            SplineManager.simplePath.Draw(_spriteBatch);
            SplineManager.simplePath.DrawPoints(_spriteBatch);

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

                        if (towers is AvastTower)
                        {
                            towers.StartAttack(gameTime, enemys, Vector2.Subtract(enemys.positionV2, towers.pos), SpriteManager.AvastProjectile);
                        }
                       
                        if (towers is NordVPNTower)
                        {
                            enemys.SlowSpeed(gameTime);
                            towers.StartAttack(gameTime, enemys, Vector2.Subtract(enemys.positionV2, towers.pos), SpriteManager.SnowFlakeTex);
                        }
                        if (towers is CustomTower)
                        {
                            towers.StartAttack(gameTime, enemys, Vector2.Subtract(enemys.positionV2, towers.pos), SpriteManager.AvastProjectile);
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
        public void SpawnParticle()
        {
            particleSystem.EmitterLocation = new Vector2(hudManager.levelUpButtonRect.X + SpriteManager.LevelUpButtonTex.Width / 2, hudManager.levelUpButtonRect.Y + SpriteManager.LevelUpButtonTex.Height / 4);
            startParticleUpdate = true;
            SpriteManager.CashSound.Play();
        }   
    }
}
