﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        RenderTarget2D renderTarget;
        GameObject gameObject;
        List<GameObject> goList; 

        

        //public SimplePath simplePath;
        float monkeyPos; // move up the spline
        float t;
        

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
            _graphics.PreferredBackBufferWidth = SpriteManager.BackgroundTex.Width * 2;
            _graphics.PreferredBackBufferHeight = SpriteManager.BackgroundTex.Height * 2;
            _graphics.ApplyChanges();

           
            goList = new List<GameObject>();

            
            

            SplineManager.LoadSpline(GraphicsDevice, Window);
            gameObject = new GameObject(SpriteManager.BloonsMonkeyTex, Vector2.Zero, new Rectangle(0,0, SpriteManager.BloonsMonkeyTex.Width, SpriteManager.BloonsMonkeyTex.Height));


            renderTarget = new RenderTarget2D(GraphicsDevice,Window.ClientBounds.Width, Window.ClientBounds.Height);
            DrawOnRenderTarget();


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            // TODO: Add your update logic here
            monkeyPos += 5;
            gameObject.Update();
            KeyMouseReader.Update();


            if (KeyMouseReader.LeftClick())
            {
                Debug.WriteLine("clicked");
                if (CanPlace(gameObject))
                {
                    Vector2 newPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

                    

                    goList.Add(new GameObject(SpriteManager.BloonsMonkeyTex, newPosition, new Rectangle((int)newPosition.X - SpriteManager.BloonsMonkeyTex.Width/2, (int)newPosition.Y- SpriteManager.BloonsMonkeyTex.Height/2, SpriteManager.BloonsMonkeyTex.Width, SpriteManager.BloonsMonkeyTex.Height)));
                    Debug.WriteLine("placed");
                }
            }
            Debug.WriteLine(goList.Count);


            DrawOnRenderTarget(); ///dasdadasad
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(SpriteManager.BackgroundTex, Vector2.Zero, null,Color.White,0f, Vector2.Zero,2f, SpriteEffects.None,1f);
            _spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            
            gameObject.Draw(_spriteBatch);

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
            if (!(monkeyPos >= SplineManager.simplePath.endT))
            {

                _spriteBatch.Draw(SpriteManager.TrojanTex, SplineManager.simplePath.GetPos(monkeyPos), null, Color.White, 0f, new Vector2(SpriteManager.TrojanTex.Width / 2, SpriteManager.TrojanTex.Height / 2), 1f, SpriteEffects.None, 1f);

            }



            foreach (GameObject go in goList)
            {
                go.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
          
        }

        public bool CanPlace(GameObject g)
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
