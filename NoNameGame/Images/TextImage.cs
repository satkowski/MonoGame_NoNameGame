using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Components;
using NoNameGame.Managers;
using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;

namespace NoNameGame.Images
{
    /// <summary>
    /// Stellt einen Text als ein Bild dar. Wahlweise kann der Text auch auf einem Bild liegen.
    /// </summary>
    [XmlInclude(typeof(TextImage))]
    public class TextImage : Image
    {
        /// <summary>
        /// Eine Aufzöhlung der Möglichen Positionen an der ein Text mit einem Bild verbunden werden kann.
        /// Also Seite an Seite oder (mit None) in dem Bild.
        /// </summary>
        public enum TextMergeSide
        {
            Left,
            Right,
            Top,
            Bottom,
            None
        }

        /// <summary>
        /// Die Font, die der Text annehmen soll.
        /// </summary>
        SpriteFont font;

        /// <summary>
        /// Der Text.
        /// </summary>
        public String Text;
        /// <summary>
        /// Der Name der Font, in dem der Text angezeigt werden soll.
        /// </summary>
        public string FontName;
        /// <summary>
        /// Die Seite an dem der Text verbunden werden soll.
        /// </summary>
        public TextMergeSide MergeSide;
        /// <summary>
        /// Offset beim Verbinden des Textes mit dem Bild.
        /// </summary>
        public Vector2 MergeOffset;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public TextImage()
        {
            Text = String.Empty;
            FontName = String.Empty;
            MergeSide = TextMergeSide.None;
            MergeOffset = Vector2.Zero;
        }

        public override void LoadContent(Body body)
        {
            // Falls es keinen Text geben sollte, wird nur ein Bild geladen.
            if(Text == String.Empty)
                base.LoadContent(body);
            else
            {
                content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

                Texture = content.Load<Texture2D>(Path);
                font = content.Load<SpriteFont>(FontName);

                Vector2 dimension = Vector2.Zero;
                Vector2 fontPosition = Vector2.Zero;
                Vector2 texturePosition = Vector2.Zero;

                // Es wird nur verbunden, wenn es überhaupt ein Bild gibt, mit dem sich der Text verbinden kann.
                if(Texture != null)
                {
                    // Hier wird die Dimension des verbundenen Bildes berechnet.
                    switch(MergeSide)
                    {
                        case TextMergeSide.None:
                            dimension = new Vector2(Math.Max(Texture.Width, font.MeasureString(Text).X),
                                                     Math.Max(Texture.Height, font.MeasureString(Text).Y));
                            break;
                        case TextMergeSide.Right:
                        case TextMergeSide.Left:
                            dimension = new Vector2(Texture.Width + font.MeasureString(Text).X + MergeOffset.X,
                                                     Math.Max(Texture.Height, font.MeasureString(Text).Y));
                            break;
                        case TextMergeSide.Top:
                        case TextMergeSide.Bottom:
                            dimension = new Vector2(Math.Max(Texture.Width, font.MeasureString(Text).X),
                                                     Texture.Height + font.MeasureString(Text).Y + MergeOffset.Y);
                            break;
                    }
                    // Hier werden die Positionen für das Bild und den Text im verbunden Bild berechnet.
                    switch(MergeSide)
                    {
                        case TextMergeSide.Right:
                            fontPosition.X = Texture.Width + MergeOffset.X;
                            break;
                        case TextMergeSide.Left:
                            texturePosition.X = font.MeasureString(Text).X + MergeOffset.X;
                            break;
                        case TextMergeSide.Top:
                            texturePosition.Y = font.MeasureString(Text).Y + MergeOffset.Y;
                            break;
                        case TextMergeSide.Bottom:
                            texturePosition.Y = Texture.Height + MergeOffset.Y;
                            break;
                    }
                }
                else
                    dimension = new Vector2(font.MeasureString(Text).X, font.MeasureString(Text).Y);

                RenderTarget2D renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)dimension.X, (int)dimension.Y);
                // Einsetzen des neuen RenderTargets, damit wird den Text und das Bild verknüpfen können.
                ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
                ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
                ScreenManager.Instance.SpriteBatch.Begin();
                // Malen.
                if(Texture != null)
                    ScreenManager.Instance.SpriteBatch.Draw(Texture, texturePosition, Color.White);
                ScreenManager.Instance.SpriteBatch.DrawString(font, Text, fontPosition, Color.White);
                ScreenManager.Instance.SpriteBatch.End();
                // Setzen des neu gemalten Bildes auf die Texture.
                Texture = renderTarget;

                // Zurücksetzen des Rendertargets.
                ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

                // Aktivieren des Basiskonstruktors. Dafür wird der Pfad kurzzeitig herausgenommen, weil sonst das Bild ohne Text
                // geladen werden würde.
                String path = Path;
                Path = String.Empty;
                base.LoadContent(body);
                Path = path;
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            base.Draw(spriteBatch, position);
        }
    }
}
