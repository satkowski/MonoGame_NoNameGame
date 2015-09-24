
using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace NoNameGame.Images.Effects
{
    public class SpriteEffect : ImageEffect
    {
        public enum EffectType
        {
            None,
            Always,
            Standing,
            Moving
        }

        public enum SpriteCicleDirection
        {
            Left = -1,
            Right = 1
        }

        protected int elapsedTime;

        public Vector2 CurrentSprite;
        [XmlIgnore]
        public Vector2 Size
        { private set; get; }
        public Vector2 SheetSize;
        public float ChangeSpeed;
        public int Offset;
        public SpriteCicleDirection CicleDirection;
        public EffectType SpriteType;

        public SpriteEffect()
        {
            elapsedTime = 0;
            CurrentSprite = Vector2.Zero;
            Size = Vector2.Zero;
            SheetSize = Vector2.Zero;
            Offset = 0;
            ChangeSpeed = 0.0f;
            CicleDirection = SpriteCicleDirection.Left;
            SpriteType = EffectType.None;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);

            Size = new Vector2(image.Texture.Width / SheetSize.X, image.Texture.Height / SheetSize.Y);
            image.SourceRectangle = new Rectangle((int)CurrentSprite.X * (int)Size.X + Offset, (int)CurrentSprite.Y * (int)Size.Y + Offset,
                                                  (int)Size.X - 2 * Offset, (int)Size.Y - 2 * Offset);
        }

        public override void Update(GameTime gameTime)
        {
            if(SpriteType == EffectType.Standing && IsActive ||
                SpriteType == EffectType.Always)
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                if(elapsedTime >= ChangeSpeed)
                {
                    elapsedTime = 0;

                    CurrentSprite.X += (int)CicleDirection;

                    if(CurrentSprite.X >= SheetSize.X || CurrentSprite.X < 0)
                        CurrentSprite.X = (CurrentSprite.X + SheetSize.X) % SheetSize.X;

                    image.SourceRectangle = new Rectangle((int)CurrentSprite.X * (int)Size.X + Offset, (int)CurrentSprite.Y * (int)Size.Y + Offset,
                                                          (int)Size.X - 2 * Offset, (int)Size.Y - 2 * Offset);
                }
            }
        }
    }
}
