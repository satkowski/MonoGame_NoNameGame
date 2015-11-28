using System.Xml.Serialization;

using Microsoft.Xna.Framework;

namespace NoNameGame.Images.Effects
{
    /// <summary>
    /// Stellt eine Rotation eines Bildes dar.
    /// </summary>
    public class RotationEffect : ImageEffect
    {
        /// <summary>
        /// Enum, welches angiebt, in welche Richtung sich gedreht werden soll.
        /// </summary>
        public enum RotationDirection
        {
            Counterclockwise = -1,
            Clockwise = 1
        }

        /// <summary>
        /// Die Rotationsrichtung.
        /// </summary>
        public RotationDirection Direction;
        /// <summary>
        /// Die Rotation pro Millisekunde.
        /// </summary>
        public float RotationPerMillisecond;
        /// <summary>
        /// Der Startrotationswert.
        /// </summary>
        [XmlIgnore]
        public float StartImageRotation
        { private set; get; }

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
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
