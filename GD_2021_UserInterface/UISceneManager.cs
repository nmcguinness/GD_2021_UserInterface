using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GD_2021_UserInterface
{
    public class UIScene
    {
        //id, name, isVisible, isEnabled, UISceneType {Options, Main, InGame}
        private string id, name;

        private List<UIObject> uiObjects;

        public UIScene(string name)
        {
            Name = name;
            ID = "UIS_" + Guid.NewGuid();
        }

        public string ID { get => id; set => id = value; }

        //TODO - add length and null check on name
        public string Name { get => name; set => name = value.Trim(); }

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
        private Dictionary<string, UIScene> scenes;
        private UIScene activeScene;
        private string activeSceneName;

        public UISceneManager(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            scenes = new Dictionary<string, UIScene>();
            activeScene = null;
            activeSceneName = "";
        }

        public bool SetActiveScene(string sceneName)
        {
            UIScene scene = Find(sceneName);
            if (scene != null)
            {
                activeScene = scene;
                activeSceneName = sceneName;
                return true;
            }
            return false;
        }

        public void Add(UIScene scene)
        {
            if (!scenes.ContainsKey(scene.Name))
                scenes.Add(scene.Name, scene);
        }

        public bool Remove(string key)
        {
            return scenes.Remove(key);
        }

        public UIScene Find(string key)
        {
            UIScene uiScene;
            scenes.TryGetValue(key, out uiScene);
            return uiScene;
        }

        public void Clear()
        {
            if (scenes.Count != 0)
                scenes.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            activeScene?.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.AlphaBlend, null, null);
            activeScene?.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}