﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GD_2021_UserInterface
{
    public abstract class UIObject
    {
        protected Transform2D transform;
        protected float depth;
        protected SpriteEffects spriteEffects;
        protected Vector2 origin;

        public Transform2D Transform { get => transform; set => transform = value; }
        public float Depth { get => depth; set => depth = value; }
        public SpriteEffects SpriteEffects { get => spriteEffects; set => spriteEffects = value; }
        public Vector2 Origin { get => origin; set => origin = value; }

        protected UIObject(Transform2D transform, float depth, SpriteEffects spriteEffects, Vector2 origin)
        {
            Transform = transform;
            Depth = depth;
            SpriteEffects = spriteEffects;
            Origin = origin;
        }

        public virtual void Update()
        {
            //TODO - here we will update all components
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        //TODO - Clone
    }

    public class UITextureObject : UIObject
    {
        private Texture2D activeTexture;
        private Texture2D alternateTexture;
        private Rectangle sourceRectangle;

        public Texture2D ActiveTexture { get => activeTexture; set => activeTexture = value; }
        public Texture2D AlternateTexture { get => alternateTexture; set => alternateTexture = value; }
        public Rectangle SourceRectangle { get => sourceRectangle; set => sourceRectangle = value; }

        /// <summary>
        /// Use this constructor draw FULL, UNROTATED, ZERO ORIGIN textures
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="depth"></param>
        /// <param name="activeTexture"></param>
        public UITextureObject(Transform2D transform,
            float depth, Texture2D activeTexture)
            : this(transform, depth,
            SpriteEffects.None, Vector2.Zero,
            activeTexture, null,
            new Rectangle(0, 0, activeTexture.Width, activeTexture.Height))
        {
        }

        public UITextureObject(Transform2D transform, float depth,
            SpriteEffects spriteEffects, Vector2 origin,
            Texture2D activeTexture, Texture2D alternateTexture,
            Rectangle sourceRectangle)
        : base(transform, depth, spriteEffects, origin)
        {
            ActiveTexture = activeTexture;
            AlternateTexture = alternateTexture;
            SourceRectangle = sourceRectangle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Begin
            spriteBatch.Draw(activeTexture, //TODO - variable e.g. texture
                Transform.LocalTranslation,
                sourceRectangle,
                Color.White, //TODO - add color to UIObject
                Transform.RotationInDegrees,
                origin,
                Transform.LocalScale,
                SpriteEffects,
                depth);
            //End
        }

        //TODO - Clone
    }

    //TODO - UITextObject
}