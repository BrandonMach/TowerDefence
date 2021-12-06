using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Diagnostics;

namespace TowerDefence
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public SimplePath simplePath;
        float moveUpSpline;
        float t;

        Vector2 monkeyPos;

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
            simplePath = new SimplePath(GraphicsDevice);
            _graphics.PreferredBackBufferWidth = 1900;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            moveUpSpline += 5;

            monkeyPos = simplePath.GetPos(simplePath.beginT + moveUpSpline); // start of spline + ökar t med moveUpSline
            Debug.WriteLine(simplePath.endT);
            if (moveUpSpline >= simplePath.endT)
            {
                Debug.WriteLine("end");

            }
            simplePath.AddPoint(new Vector2(30, 10));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            simplePath.Draw(_spriteBatch);
            simplePath.DrawPoints(_spriteBatch);
            if (!(moveUpSpline >= simplePath.endT))
            {
                Debug.WriteLine("end");
                _spriteBatch.Draw(SpriteManager.bloonsMonkey, monkeyPos, null, Color.White, 0f, new Vector2(SpriteManager.bloonsMonkey.Width / 2, SpriteManager.bloonsMonkey.Height / 2), 1f, SpriteEffects.None, 1f);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
