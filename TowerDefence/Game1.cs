using Microsoft.Xna.Framework;
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
        RenderTarget2D backgroundLayer;
        List<GameObjects> go; 

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
            _graphics.PreferredBackBufferWidth = 1900;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            backgroundLayer = new RenderTarget2D(GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);
            go = new List<GameObjects>();
            

            SplineManager.LoadSpline(GraphicsDevice, Window);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            monkeyPos += 5;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            SplineManager.simplePath.Draw(_spriteBatch);
            SplineManager.simplePath.DrawPoints(_spriteBatch);
            if (!(monkeyPos >= SplineManager.simplePath.endT))
            {
                Debug.WriteLine("end");
                _spriteBatch.Draw(SpriteManager.BloonsMonkeyTex, SplineManager.simplePath.GetPos(monkeyPos), null, Color.White, 0f, new Vector2(SpriteManager.BloonsMonkeyTex.Width / 2, SpriteManager.BloonsMonkeyTex.Height / 2), 1f, SpriteEffects.None, 1f);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawRenderTargetLayer(GraphicsDevice device)
        {
            SpriteBatch sb = new SpriteBatch(device);
            device.SetRenderTarget(backgroundLayer);
            device.Clear(Color.Transparent);
            sb.Begin();

            foreach (GameObjects objects in go)
            {
                objects.Draw(sb);
            }

            sb.End();

            device.SetRenderTarget(null);
        }
        public bool CanPlace(GameObjects g)
        {
            Color[] pixels = new Color[g.tex.Width * g.tex.Height];
            Color[] pixels2 = new Color[g.tex.Width * g.tex.Height];
            g.tex.GetData<Color>(pixels2);
            backgroundLayer.GetData(0, g.bb, pixels, 0, pixels.Length);

            for (int i = 0; i < pixels.Length; ++i)
            {
                if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                    return false;
            }

            return true;
        }


    }
}
