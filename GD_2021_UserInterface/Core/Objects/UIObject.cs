﻿using GDLibrary.Components.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GDLibrary
{
    /// <summary>
    /// Enumeration of types of ui objects used by UI manager and menu manager
    /// </summary>
    public enum UIObjectType : sbyte
    {
        Text,
        Texture,
        Background,
        Progress
    }

    /// <summary>
    /// Parent class for any ui element used in main ui or menu
    /// </summary>
    public abstract class UIObject : ICloneable, IDisposable
    {
        #region Statics

        private static readonly int DEFAULT_SIZE = 4;

        #endregion Statics

        #region Fields

        /// <summary>
        /// Enumerated type indicating what category ths ui object belongs to (e.g. Text, Texture, Progress)
        /// </summary>
        protected UIObjectType uiObjectType;

        /// <summary>
        /// Unique identifier for each ui object - may be used for search, sort later
        /// </summary>
        private string id;

        /// <summary>
        /// Friendly name for the current ui object
        /// </summary>
        private string name;

        /// <summary>
        /// Set on first update of the component in UISceneManager::Update
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Set in constructor to true. By default all components are enabled on instanciation
        /// </summary>
        private bool isEnabled;

        /// <summary>
        /// Drawn translation, rotation, and scale of ui object on screen
        /// </summary>
        private Transform2D transform;

        /// <summary>
        /// Depth used to sort ui objects on screen (0 = front-most, 1 = back-most)
        /// </summary>
        private float layerDepth;

        /// <summary>
        /// Used to flip the text/texture
        /// </summary>
        private SpriteEffects spriteEffects;

        /// <summary>
        /// Blend color used for text/texture
        /// </summary>
        protected Color color;

        /// <summary>
        /// Origin of rotation for the ui object in texture space (i.e. [0,0] - [w,h])
        /// Useful to rotate textures around unusual origin points e.g. a speedometer needle
        /// </summary>
        private Vector2 origin;

        /// <summary>
        /// List of all attached components
        /// </summary>
        protected List<UIComponent> components;

        #endregion Fields

        #region Properties

        public string ID { get => id; protected set => id = value; }
        protected string Name { get => name; set => name = value.Trim(); }
        public bool IsRunning { get => isRunning; private set => isRunning = value; }
        public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        public Transform2D Transform { get => transform; set => transform = value; }
        public float LayerDepth { get => layerDepth; set => layerDepth = value >= 0 && value <= 1 ? value : 0; }
        public SpriteEffects SpriteEffects { get => spriteEffects; set => spriteEffects = value; }
        public Color Color { get => color; set => color = value; }
        public Vector2 Origin { get => origin; set => origin = value; }

        #endregion Properties

        #region Constructors

        protected UIObject(string name, UIObjectType uiObjectType, Transform2D transform, float layerDepth,
            Color color, SpriteEffects spriteEffects, Vector2 origin)
        {
            Transform = transform;
            LayerDepth = layerDepth;
            SpriteEffects = spriteEffects;
            Color = color;
            Origin = origin;
            components = new List<UIComponent>(DEFAULT_SIZE);

            IsEnabled = true;
            IsRunning = false;

            this.uiObjectType = uiObjectType;
            ID = "UIO-" + Guid.NewGuid();
            Name = string.IsNullOrEmpty(name) ? ID : name;
        }

        #endregion Constructors

        #region Initialization

        public virtual void Initialize()
        {
            if (!IsRunning)
            {
                IsRunning = true;

                //TODO - Add sort IComparable in each component
                //components.Sort();

                //for (int i = 0; i < components.Count; i++)
                //    components[i].Start();
            }
        }

        #endregion Initialization

        #region Actions - Update & Draw

        public virtual void Update()
        {
            //TODO - here we will update all components
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        #endregion Actions - Update & Draw

        #region Actions - Add & Get Components

        public UIComponent AddComponent(UIComponent component)
        {
            if (component == null)
                return null;

            //set this as component's parent game object
            component.UIObject = this;
            //perform any initial wake up operations
            component.Awake();
            //add to list
            components.Add(component);

            if (isRunning && !component.IsRunning)
            {
                component.Start();
                component.IsRunning = true;
                components.Sort();
            }

            return component;
        }

        public T AddComponent<T>() where T : UIComponent, new()
        {
            var component = new T();
            return (T)AddComponent(component);
        }

        public T GetComponent<T>() where T : UIComponent
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T)
                    return components[i] as T;
            }

            return null;
        }

        public T GetComponent<T>(Predicate<UIComponent> pred) where T : UIComponent
        {
            return components.Find(pred) as T;
        }

        public T[] GetComponents<T>() where T : UIComponent
        {
            List<T> componentList = new List<T>();

            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T)
                    componentList.Add(components[i] as T);
            }

            return componentList.ToArray();
        }

        public T[] GetComponents<T>(Predicate<UIComponent> pred) where T : UIComponent
        {
            return components.FindAll(pred).ToArray() as T[];
        }

        #endregion Actions - Add & Get Components

        #region Actions - Housekeeping

        public virtual void Dispose()
        {
            //TODO
            //foreach (Component component in components)
            //    component.Dispose();
        }

        public virtual object Clone()
        {
            //TODO
            return null;
            //var clone = new GameObject($"Clone - {Name}", gameObjectType);
            //clone.ID = "GO-" + Guid.NewGuid();

            //Component clonedComponent = null;
            //Transform clonedTransform = null;

            //foreach (Component component in components)
            //{
            //    clonedComponent = clone.AddComponent((Component)component.Clone());
            //    clonedComponent.gameObject = clone;

            //    clonedTransform = clonedComponent as Transform;

            //    if (clonedTransform != null)
            //        clonedComponent.transform = clonedTransform;
            //}

            //clone.IsStatic = this.isStatic;
            //return clone;
        }

        #endregion Actions - Housekeeping
    }

    /// <summary>
    /// Draws a texture on screen
    /// </summary>
    public class UITextureObject : UIObject
    {
        #region Fields

        /// <summary>
        /// Default texture shown for this object
        /// </summary>
        private Texture2D defaultTexture;

        /// <summary>
        /// Alternate image may be used for hover/mouse click effects
        /// </summary>
        private Texture2D alternateTexture;

        /// <summary>
        /// Used to control how much of the source image we draw (e.g. for a portion of an image as in a progress bar)
        /// </summary>
        private Rectangle sourceRectangle;

        /// <summary>
        /// Sets current to be either active or alternate (e.g. used for hover over texture change)
        /// </summary>
        private Texture2D currentTexture;

        #endregion Fields

        #region Properties

        public Texture2D DefaultTexture { get => defaultTexture; set => defaultTexture = value; }
        public Texture2D AlternateTexture { get => alternateTexture; set => alternateTexture = value; }
        public Rectangle SourceRectangle { get => sourceRectangle; set => sourceRectangle = value; }
        public Texture2D CurrentTexture { get => currentTexture; set => currentTexture = value; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Use this constructor draw WHITE BLEND, FULL, UNROTATED, ZERO-ORIGIN textures
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="depth"></param>
        /// <param name="activeTexture"></param>
        public UITextureObject(string name, UIObjectType uiObjectType, Transform2D transform,
            float depth, Texture2D defaultTexture)
            : this(name, uiObjectType, transform, depth,
            Color.White, SpriteEffects.None, Vector2.Zero, defaultTexture, null,
            new Rectangle(0, 0, defaultTexture.Width, defaultTexture.Height))
        {
        }

        public UITextureObject(string name, UIObjectType uiObjectType, Transform2D transform, float depth,
            Color color, SpriteEffects spriteEffects, Vector2 origin,
            Texture2D defaultTexture, Texture2D alternateTexture,
            Rectangle sourceRectangle)
        : base(name, uiObjectType, transform, depth, color, spriteEffects, origin)
        {
            DefaultTexture = defaultTexture;
            AlternateTexture = alternateTexture;
            SourceRectangle = sourceRectangle;

            //sets the texture used by default in the Draw() below
            CurrentTexture = defaultTexture;
        }

        #endregion Constructors

        #region Actions - Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(defaultTexture,
                Transform.LocalTranslation,
                sourceRectangle,
                color,
                Transform.RotationInDegrees,
                Origin,
                Transform.LocalScale,
                SpriteEffects,
                LayerDepth);
        }

        #endregion Actions - Draw

        #region Actions - Housekeeping

        public override void Dispose()
        {
            //TODO
            //foreach (Component component in components)
            //    component.Dispose();
        }

        public override object Clone()
        {
            //TODO
            return null;
            //var clone = new GameObject($"Clone - {Name}", gameObjectType);
            //clone.ID = "GO-" + Guid.NewGuid();

            //Component clonedComponent = null;
            //Transform clonedTransform = null;

            //foreach (Component component in components)
            //{
            //    clonedComponent = clone.AddComponent((Component)component.Clone());
            //    clonedComponent.gameObject = clone;

            //    clonedTransform = clonedComponent as Transform;

            //    if (clonedTransform != null)
            //        clonedComponent.transform = clonedTransform;
            //}

            //clone.IsStatic = this.isStatic;
            //return clone;
        }

        #endregion Actions - Housekeeping
    }

    /// <summary>
    /// Draws a texture on screen
    /// </summary>
    public class UITextObject : UIObject
    {
        #region Fields

        /// <summary>
        /// Font used to render text for this object
        /// </summary>
        private SpriteFont spriteFont;

        /// <summary>
        /// Text rendered for this object
        /// </summary>
        private string text;

        #endregion Fields

        #region Properties

        public SpriteFont SpriteFont { get => spriteFont; set => spriteFont = value; }
        public string Text { get => text; set => text = value.Trim(); }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Use this constructor draw WHITE BLEND, UNROTATED, ZERO-ORIGIN text
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="depth"></param>
        /// <param name="activeTexture"></param>
        public UITextObject(string name, UIObjectType uiObjectType, Transform2D transform, float depth,
                   SpriteFont spriteFont, string text)
                   : this(name, uiObjectType, transform, depth,
                   Color.White, SpriteEffects.None, Vector2.Zero,
                   spriteFont, text)
        {
        }

        public UITextObject(string name, UIObjectType uiObjectType, Transform2D transform, float depth,
            Color color, SpriteEffects spriteEffects, Vector2 origin,
            SpriteFont spriteFont, string text)
        : base(name, uiObjectType, transform, depth, color, spriteEffects, origin)
        {
            SpriteFont = spriteFont;
            Text = text;
        }

        #endregion Constructors

        #region Actions - Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont,
                text,
                Transform.LocalTranslation,
                color,
                Transform.RotationInDegrees,
                Origin,
                Transform.LocalScale,
                SpriteEffects,
                LayerDepth);
        }

        #endregion Actions - Draw

        #region Actions - Housekeeping

        public override void Dispose()
        {
            //TODO
            //foreach (Component component in components)
            //    component.Dispose();
        }

        public override object Clone()
        {
            //TODO
            return null;
            //var clone = new GameObject($"Clone - {Name}", gameObjectType);
            //clone.ID = "GO-" + Guid.NewGuid();

            //Component clonedComponent = null;
            //Transform clonedTransform = null;

            //foreach (Component component in components)
            //{
            //    clonedComponent = clone.AddComponent((Component)component.Clone());
            //    clonedComponent.gameObject = clone;

            //    clonedTransform = clonedComponent as Transform;

            //    if (clonedTransform != null)
            //        clonedComponent.transform = clonedTransform;
            //}

            //clone.IsStatic = this.isStatic;
            //return clone;
        }

        #endregion Actions - Housekeeping
    }

    //TODO - UITextObject
}