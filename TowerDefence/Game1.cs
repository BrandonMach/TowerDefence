﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    public class Game1 : Game
    {

        enum TowerSelect
        {
            None,
            Avast,
            Monkey, 
        }
        TowerSelect currentTowerSelected;



        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        RenderTarget2D renderTarget;
        GameObject gameObject;
        Towers avastSelected;
        Towers monkeySelected;
        List<GameObject> towersList;

        Enemys enemys;
        List<Enemys> enemyList;
        

        //public SimplePath simplePath;
        float enemyPos; // move up the spline
        float nextEnemyPos;
        float enemyRotation;
        

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


            currentTowerSelected = TowerSelect.None;

            SplineManager.LoadSpline(GraphicsDevice, Window);
            avastSelected = new AvastTower(SpriteManager.AvastTex, Vector2.Zero, new Rectangle(0,0, SpriteManager.AvastTex.Width, SpriteManager.AvastTex.Height));
            monkeySelected = new Towers(SpriteManager.BloonsMonkeyTex, Vector2.Zero, new Rectangle(0,0, SpriteManager.BloonsMonkeyTex.Width, SpriteManager.BloonsMonkeyTex.Height));
            



            renderTarget = new RenderTarget2D(GraphicsDevice,Window.ClientBounds.Width+300, Window.ClientBounds.Height+300);
            DrawOnRenderTarget();


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
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
                Debug.WriteLine(enemyList.Count);
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





            switch (currentTowerSelected)
            {
                case TowerSelect.None:
                    break;
                case TowerSelect.Avast:

                    avastSelected.Update();
                    //för alla gameobjects bredd och höjd i Mouse.GetState()
                    if (KeyMouseReader.LeftClick() && Mouse.GetState().X > 0 + avastSelected.texture.Width / 2 && Mouse.GetState().Y > 0 + avastSelected.texture.Height / 2)
                    {
                        Debug.WriteLine("clicked");
                        if (CanPlace(avastSelected))
                        {
                            Vector2 newPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);



                            towersList.Add(new AvastTower(SpriteManager.AvastTex, newPosition, new Rectangle((int)newPosition.X - SpriteManager.AvastTex.Width / 2, (int)newPosition.Y - SpriteManager.AvastTex.Height / 2, SpriteManager.AvastTex.Width, SpriteManager.AvastTex.Height)));
                            Debug.WriteLine("placed");
                        }
                    }
                    Debug.WriteLine(towersList.Count);
                    break;
                case TowerSelect.Monkey:
                    monkeySelected.Update();
                    //för alla gameobjects bredd och höjd i Mouse.GetState()
                    if (KeyMouseReader.LeftClick() && Mouse.GetState().X > 0 + monkeySelected.texture.Width / 2 && Mouse.GetState().Y > 0 + monkeySelected.texture.Height / 2)
                    {
                        Debug.WriteLine("clicked");
                        if (CanPlace(monkeySelected))
                        {
                            Vector2 newPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);



                            towersList.Add(new AvastTower(SpriteManager.BloonsMonkeyTex, newPosition, new Rectangle((int)newPosition.X - SpriteManager.BloonsMonkeyTex.Width / 2, (int)newPosition.Y - SpriteManager.BloonsMonkeyTex.Height / 2, SpriteManager.BloonsMonkeyTex.Width, SpriteManager.BloonsMonkeyTex.Height)));
                            Debug.WriteLine("placed");
                        }
                    }
                    Debug.WriteLine(towersList.Count);
                    break;
                default:
                    break;
            }
         


            DrawOnRenderTarget(); ///dasdadasad
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(SpriteManager.BackgroundTex, Vector2.Zero, null,Color.White,0f, Vector2.Zero,1f, SpriteEffects.None,1f);
            _spriteBatch.Draw(renderTarget, Vector2.Zero,Color.White);



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
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawOnRenderTarget()
        {


            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            _spriteBatch.Begin();

           


            // dnjasdhadasdnjasdjsan
            SplineManager.simplePath.Draw(_spriteBatch);
            SplineManager.simplePath.DrawPoints(_spriteBatch);

            foreach (Enemys enemys in enemyList)
            {
                if (!(enemys.positionFloat >= SplineManager.simplePath.endT))
                {

                    enemys.Draw(_spriteBatch);
                   // _spriteBatch.Draw(SpriteManager.TrojanTex, SplineManager.simplePath.GetPos(enemys.positionFloat), null, Color.White, enemyRotation, new Vector2(SpriteManager.TrojanTex.Width / 2, SpriteManager.TrojanTex.Height / 2), 1f, SpriteEffects.None, 1f);

                }
            }
          



            foreach (Towers twrs in towersList)
            {
                twrs.Draw(_spriteBatch);
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


    }
}
