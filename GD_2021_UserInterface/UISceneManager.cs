using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GD_2021_UserInterface
{
    public class UIScene
    {
        //id, name, isVisible, isEnabled, UISceneType {Options, Main, InGame}

        private List<UIObject> uiObjects;
        public List<UIObject> UiObjects { get => uiObjects; set => uiObjects = value; }

        //constructor, add, remove, find, clear, update, draw

        public virtual void Update()
        {
            foreach (UIObject uiObject in uiObjects)
                uiObject.Update();
            //TODO - add isEnabled check on uiObject
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (UIObject uiObject in uiObjects)
                uiObject.Draw(spriteBatch);
        }
    }

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