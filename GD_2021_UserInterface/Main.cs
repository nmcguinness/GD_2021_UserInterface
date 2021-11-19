using GDLibrary;
using GDLibrary.Components.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
MOSCOW

MUST
----
- Texture, Text
- Respond to mouse/keyboard/gamepad

SHOULD
-----
- Modify object over time

COULD
-----
- Camera (camera effects)

Entities
------
- Transform2D (t,r,s,w)
- UIObject, UITextObject, UITextureObject
- UIScene, UISceneManager

 */

namespace GD_2021_UserInterface
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //move the SpriteBatch init from LoadContent because its needed in UISceneManager!
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //make the screen a little bigger to see the UI
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();

            InitializeUI();

            base.Initialize();
        }

        /// <summary>
        /// Initialize menu and screen UI
        /// </summary>
        private void InitializeUI()
        {
            InitializeGameMenu();

            InitializeGameUI();
        }

        private void InitializeGameMenu()
        {
            //TODO - we will need to add MenuManager : UISceneManager in Week 9
        }

        private void InitializeGameUI()
        {
            #region Manager and Main Scene

            //create the manager
            var uiManager = new UISceneManager(this, _spriteBatch);
            Components.Add(uiManager);

            //create the scene
            var mainGameUIScene = new UIScene("main game ui");

            #endregion Manager and Main Scene

            #region Add Health Bar

            //create the UI element
            var healthTextureObj = new UITextureObject("health", UIObjectType.Texture,
                new Transform2D(new Vector2(50, 100), Vector2.One, 0),
                0, Content.Load<Texture2D>("ui_progress_32_8"));

            //add the ui element to the scene
            mainGameUIScene.Add(healthTextureObj);

            #endregion Add Health Bar

            #region Add Text

            //create the UI element
            var nameTextObj = new UITextObject("player name", UIObjectType.Text,
                new Transform2D(new Vector2(50, 50), Vector2.One, 0),
                0, Content.Load<SpriteFont>("ui_font"), "Brutus Maximus");

            //add the ui element to the scene
            mainGameUIScene.Add(nameTextObj);

            #endregion Add Text

            #region Add Scene To Manager & Set Active Scene

            //add the ui scene to the manager
            uiManager.Add(mainGameUIScene);

            //set the active scene
            uiManager.SetActiveScene("main game ui");

            #endregion Add Scene To Manager & Set Active Scene
        }

        protected override void LoadContent()
        {
            //OLD DEMO
            //spriteFont = Content.Load<SpriteFont>("ui_font");
            //   progressTexture = Content.Load<Texture2D>("ui_progress_32_8");
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //OLD DEMO
            //string str = "Hello";
            //var dimensions = spriteFont.MeasureString(str);
            //var origin = new Vector2(dimensions.X / 2, dimensions.Y / 2);

            ////How to draw text/image?
            //_spriteBatch.Begin();
            //_spriteBatch.DrawString(spriteFont,
            //    str,
            //    new Vector2(200, 200), Color.White,
            //    rotationInDegrees,
            //    origin, //Vector2.Zero,
            //    1, SpriteEffects.None, 0);
            //_spriteBatch.End();

            //_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
            //    null, null, null);
            //_spriteBatch.Draw(progressTexture, new Vector2(200, 300),
            //    new Rectangle(0, 0, 16, 8),
            //    Color.White, 0,
            //    new Vector2(progressTexture.Width / 2, progressTexture.Height / 2),
            //    new Vector2(4, 4),
            //    SpriteEffects.FlipVertically,
            //    0);
            //_spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}