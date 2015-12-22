
using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace NoNameGame.Images.Effects
{
    /// <summary>
    /// Stellt einen Spriteeffekt dar. Also, dass sich ein Bild über einen gewissen Zeitraum verändert.
    /// </summary>
    public class SpriteEffect : ImageEffect
    {
        /// <summary>
        /// Ein Enum, was angiebt, wann der Sprite-Effekt ausgeführt wird.
        /// </summary>
        public enum EffectType
        {
            None,
            Always,
            Standing,
            Moving
        }

        /// <summary>
        /// Ein Enum, was angiebt, in welche Richtung das nächste Sprite liegt.
        /// </summary>
        public enum SpriteCicleDirection
        {
            Left = -1,
            Right = 1
        }

        /// <summary>
        /// Gibt an, wieviel Zeit schon vergangen ist.
        /// </summary>
        protected int elapsedTime;

        /// <summary>
        /// Der aktuelle Sprite.
        /// </summary>
        public Vector2 CurrentSprite;
        /// <summary>
        /// Die Größe eines einzelnen Sprites.
        /// </summary>
        [XmlIgnore]
        public Vector2 Size
        { private set; get; }
        /// <summary>
        /// Die Größe des gesamten TileSheets.
        /// </summary>
        public Vector2 SheetSize;
        /// <summary>
        /// Die Geschwindigkeit in welches sich Sprite verändern sollen.
        /// </summary>
        public float ChangeSpeed;
        /// <summary>
        /// Der Offset auf dem TileSheet.
        /// </summary>
        public int Offset;
        /// <summary>
        /// Der SpriteCicleDirection.
        /// </summary>
        public SpriteCicleDirection CicleDirection;
        /// <summary>
        /// Der Spritetyp.
        /// </summary>
        public EffectType SpriteType;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
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
                // Wenn genug Zeit vergangen ist, wird eine neuer Sprite gewählt
                if(elapsedTime >= ChangeSpeed)
                {
                    elapsedTime = 0;

                    CurrentSprite.X += (int)CicleDirection;
                    // Falls der neue Sprite ausserhalb liegen würde, fängt man wieder von vorne an
                    if(CurrentSprite.X >= SheetSize.X || CurrentSprite.X < 0)
                        CurrentSprite.X = (CurrentSprite.X + SheetSize.X) % SheetSize.X;
                    // Anpassung des Rechtecks, damit beim nächsten mal auch das richtige Sprite gemalt wird
                    image.SourceRectangle = new Rectangle((int)CurrentSprite.X * (int)Size.X + Offset, (int)CurrentSprite.Y * (int)Size.Y + Offset,
                                                          (int)Size.X - 2 * Offset, (int)Size.Y - 2 * Offset);
                }
            }
        }

        /// <summary>
        /// Kopieren dieses Effekts.
        /// </summary>
        public override ImageEffect Copy()
        {
            SpriteEffect newEffect = new SpriteEffect();
            newEffect.ChangeSpeed = this.ChangeSpeed;
            newEffect.CicleDirection = this.CicleDirection;
            newEffect.CurrentSprite = this.CurrentSprite;
            newEffect.IsActive = this.IsActive;
            newEffect.Offset = this.Offset;
            newEffect.SheetSize = this.SheetSize;
            newEffect.SpriteType = this.SpriteType;
            return newEffect;
        }
    }
}
