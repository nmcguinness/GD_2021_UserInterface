using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*

 */

namespace GD_2021_UserInterface
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont spriteFont;
        private Texture2D progressTexture;
        private float rotationInDegrees;

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
            spriteFont = Content.Load<SpriteFont>("ui_font");
            progressTexture = Content.Load<Texture2D>("ui_progress_32_8");
        }

        protected override void Update(GameTime gameTime)
        {
            rotationInDegrees += 1 / 60.0f;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string str = "Hello";
            var dimensions = spriteFont.MeasureString(str);
            var origin = new Vector2(dimensions.X / 2, dimensions.Y / 2);

            //How to draw text/image?
            _spriteBatch.Begin();
            _spriteBatch.DrawString(spriteFont,
                str,
                new Vector2(200, 200), Color.White,
                rotationInDegrees,
                origin, //Vector2.Zero,
                1, SpriteEffects.None, 0);
            _spriteBatch.End();

            _spriteBatch.Begin();
            _spriteBatch.Draw(progressTexture, new Vector2(200, 300),
                new Rectangle(0, 0, 16, 8),
                Color.White, 0,
                new Vector2(progressTexture.Width / 2, progressTexture.Height / 2),
                new Vector2(4, 4),
                SpriteEffects.FlipVertically,
                0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}