using System.Xml.Serialization;

using Microsoft.Xna.Framework;

namespace NoNameGame.Images.Effects
{
    public class ScalingEffect : ImageEffect
    {
        public enum Direction
        {
            Down = -1,
            Up = 1
        }

        float endImageScale;
        Vector2 endImagePosition;

        [XmlIgnore]
        public float StartImageScale
        { private set; get; }
        [XmlIgnore]
        public Vector2 StartImagePosition
        { private set; get; }
        public float TotalScalingChange;
        public float ScalingPerMillisecond;
        public Direction ScalingDirection;

        public ScalingEffect()
        {
            TotalScalingChange = 0.0f;
            ScalingPerMillisecond = 0.0f;
            ScalingDirection = Direction.Up;
            StartImagePosition = Vector2.Zero;
            endImagePosition = Vector2.Zero;
        }

        public void FinishScaling()
        {
            image.Scale = StartImageScale + (int)ScalingDirection * TotalScalingChange;
            IsActive = false;
        }

        public void SetImagePosition()
        {
            StartImagePosition = image.Position;
            endImagePosition = StartImagePosition + (int)ScalingDirection *
                               new Vector2(image.SourceRectangle.Width / 2, image.SourceRectangle.Height / 2) * (StartImageScale - endImageScale);
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);

            StartImageScale = image.Scale;
            endImageScale = StartImageScale + (int)ScalingDirection * TotalScalingChange;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if(IsActive)
            {
                int elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
                image.Scale += (int)ScalingDirection * ScalingPerMillisecond * elapsedTime;
                image.Position += -(int)ScalingDirection * new Vector2(image.SourceRectangle.Width / 2, image.SourceRectangle.Height / 2) *
                                  (ScalingPerMillisecond * elapsedTime);
                if((ScalingDirection == Direction.Up && image.Scale > endImageScale) ||
                    (ScalingDirection == Direction.Down && image.Scale < endImageScale))
                {
                    image.Scale = endImageScale;
                    image.Position = endImagePosition;
                    IsActive = false;
                }
            }
        }
    }
}
