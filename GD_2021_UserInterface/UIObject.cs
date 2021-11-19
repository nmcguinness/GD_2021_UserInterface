using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GD_2021_UserInterface
{
    public abstract class UIObject
    {
        private Transform2D transform;
        private float depth;
        private SpriteEffects spriteEffects;
        private Vector2 origin;

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

        public abstract void Draw(SpriteBatch spriteBatch);
    }

    public class UITextureObject : UIObject
    {
        private Texture2D activeTexture;
        private Texture2D alternateTexture;
        private Rectangle sourceRectangle;

        public Texture2D ActiveTexture { get => activeTexture; set => activeTexture = value; }
        public Texture2D AlternateTexture { get => alternateTexture; set => alternateTexture = value; }
        public Rectangle SourceRectangle { get => sourceRectangle; set => sourceRectangle = value; }

        public UITextureObject(Transform2D transform, float depth, SpriteEffects spriteEffects, Vector2 origin,
            Texture2D activeTexture, Texture2D alternateTexture, Rectangle sourceRectangle)
        : base(transform, depth, spriteEffects, origin)
        {
            ActiveTexture = activeTexture;
            AlternateTexture = alternateTexture;
            SourceRectangle = sourceRectangle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }
    }
}