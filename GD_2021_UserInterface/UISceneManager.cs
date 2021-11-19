using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GD_2021_UserInterface
{
    public class UISceneManager : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;

        public UISceneManager(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}