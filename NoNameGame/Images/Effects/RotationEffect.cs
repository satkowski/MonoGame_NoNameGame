using System.Xml.Serialization;

using Microsoft.Xna.Framework;

namespace NoNameGame.Images.Effects
{
    /// <summary>
    /// Stellt eine Rotation eines Bildes dar.
    /// </summary>
    public class RotationEffect : ImageEffect
    {
        public enum RotationDirection
        {
            Counterclockwise = -1,
            Clockwise = 1
        }

        public RotationDirection Direction;
        public float RotationPerMillisecond;
        [XmlIgnore]
        public float StartImageRotation
        { private set; get; }

        public RotationEffect()
        {
            Direction = RotationDirection.Clockwise;
            RotationPerMillisecond = 0.0f;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);

            StartImageRotation = image.Rotation;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if(IsActive)
            {
                // Einfache Rotation anhand der angegeben Rotation und der vergangenen Zeit
                image.Rotation += (int)Direction * RotationPerMillisecond;
                if(image.Rotation > MathHelper.TwoPi ||
                    image.Rotation < -MathHelper.TwoPi)
                    image.Rotation += -(int)Direction * MathHelper.TwoPi;
            }
        }
    }
}
