using Microsoft.Xna.Framework;

namespace GD_2021_UserInterface
{
    public sealed class Transform2D
    {
        private Vector2 localTranslation;
        private Vector2 localScale;
        private float rotationInDegrees;

        public Vector2 LocalTranslation { get => localTranslation; set => localTranslation = value; }
        public Vector2 LocalScale { get => localScale; set => localScale = value; }
        public float RotationInDegrees { get => rotationInDegrees; set => rotationInDegrees = value; }

        public Transform2D(Vector2 localTranslation,
        Vector2 localScale, float rotationInDegrees)
        {
            //TODO - add validation in setters
            LocalTranslation = localTranslation;
            LocalScale = localScale;
            RotationInDegrees = rotationInDegrees;
        }
    }
}