using System;
using System.Collections.Generic;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Managers;
using NoNameGame.Images.Effects;
using NoNameGame.Components;

namespace NoNameGame.Images
{
    /// <summary>
    /// Stellt ein Bild dar.
    /// </summary>
    public class Image
    {
        Vector2 origin;
        ContentManager content;
        Dictionary<string, ImageEffect> effectList;

        /// <summary>
        /// Die Textur des Bildes.
        /// </summary>
        [XmlIgnore]
        public Texture2D Texture;
        /// <summary>
        /// Der Pfad zum Bild des Bildes.
        /// </summary>
        public string Path;

        /// <summary>
        /// Die Skalierung des Bildes.
        /// </summary>
        public float Scale;
        /// <summary>
        /// Die Rotation des Bildes.
        /// </summary>
        public float Rotation;
        /// <summary>
        /// Der Alphawert des Bildes.
        /// </summary>
        public float Alpha;
        /// <summary>
        /// Die Farbe der Transparententeile des Bildes.
        /// </summary>
        public Color Color;
        /// <summary>
        /// Der Offset des Bildes.
        /// </summary>
        public Vector2 Offset;
        /// <summary>
        /// Das Rechteck, welches angiebt, welcher Bereich des Bildes gemalt werden soll.
        /// </summary>
        public Rectangle SourceRectangle;
        /// <summary>
        /// Der skalierte Ursprungspunkt.
        /// </summary>
        [XmlIgnore]
        public Vector2 ScaledOrigin { private set; get; }
                
        /// <summary>
        /// Die Liste der Effekte, die dieses Bild hat.
        /// </summary>
        [XmlElement("Effect")]
        public List<String> Effects;
        /// <summary>
        /// Der Rotations-Effekt des Bildes.
        /// </summary>
        public RotationEffect RotationEffect;
        /// <summary>
        /// Der Sprite-Animations-Effekt des Bildes, während die Entity sich bewegt.
        /// </summary>
        public SpriteEffect SpriteEffectMoving;
        /// <summary>
        /// Der Sprite-Animations-Effekt des Bildes, während die Entity stehen bleibt.
        /// </summary>
        public SpriteEffect SpriteEffectStanding;
        /// <summary>
        /// Der Sprite-Animations-Effekt des Bildes.
        /// </summary>
        public SpriteEffect SpriteEffectAlways;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public Image ()
        {
            Scale = 1.0f;
            Alpha = 1.0f;
            Rotation = 0.0f;
            Path = String.Empty;
            SourceRectangle = Rectangle.Empty;
            Color = Color.White;
            origin = Vector2.Zero;
            ScaledOrigin = Vector2.Zero;
            Offset = Vector2.Zero;
            effectList = new Dictionary<string, ImageEffect>();
            Effects = new List<string>();
        }

        /// <summary>
        /// Lädt alle notwendigen Daten und aktiviert die Effekte.
        /// </summary>
        /// <param name="body"></param>
        public void LoadContent (Body body)
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            body.OnPositionChange += updateRectangles;

            if (Path != String.Empty)
                Texture = content.Load<Texture2D>(Path);

            // Setzen der Effekte
            setEffect<RotationEffect>(ref RotationEffect);
            setEffect<SpriteEffect>(ref SpriteEffectMoving, "Moving");
            setEffect<SpriteEffect>(ref SpriteEffectStanding, "Standing");
            setEffect<SpriteEffect>(ref SpriteEffectAlways, "Always");
            foreach(string effectName in Effects)
                ActivateEffect(effectName);
        }

        public void UnloadContent ()
        {
            content.Unload();
        }

        public void Update (GameTime gameTime)
        {
            foreach(var effect in effectList)
                effect.Value.Update(gameTime);
        }

        public void Draw (SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position + Offset, SourceRectangle, Color.White, Rotation, origin, Scale, SpriteEffects.None, 0.0f);
        }

        /// <summary>
        /// Aktualisiert den Ursprung anhand der neuen Position und des Rechtecks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateRectangles (object sender, EventArgs e)
        {
            Vector2 position = (Vector2)sender;

            origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            ScaledOrigin = new Vector2((SourceRectangle.Width * Scale) / 2, (SourceRectangle.Height * Scale) / 2);
        }

        /// <summary>
        /// Setzt alle möglichen Effekte.
        /// </summary>
        /// <typeparam name="T">die Art des Effekts</typeparam>
        /// <param name="effect">der Effekt</param>
        /// <param name="effectName">der Effektname</param>
        void setEffect<T>(ref T effect, string effectName = "")
        {
            if(effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            // Dem Abilitynamen wird noch ein "-" vorangestellt
            if(effectName != "")
                effectName = effectName.Insert(0, "-");

            // Nun wird der Klassennamen mit dem Abilitynmane verbunden, um dieses besser handhaben zu können
            string effectKeyName = effect.GetType().ToString().Replace("NoNameGame.Images.Effects.", "") + effectName;
            if(Effects.Contains(effectKeyName))
                effectList.Add(effectKeyName, (effect as ImageEffect));
        }

        /// <summary>
        /// Aktiviert einen Effekt
        /// </summary>
        /// <param name="effectName">der Effektname</param>
        public void ActivateEffect(string effectName)
        {
            // Es wird nur die Ability aktiviert, wenn diese auch in der Liste vorhanden ist
            if(effectList.ContainsKey(effectName))
            {
                effectList[effectName].IsActive = true;
                var obj = this;
                effectList[effectName].LoadContent(ref obj);
            }
        }

        /// <summary>
        /// Deaktiviert einen Effekt
        /// </summary>
        /// <param name="effectName">der Effektname</param>
        public void DeactivateEffect(string effectName)
        {
            // Es wird nur die Ability deaktiviert, wenn diese auch in der Liste vorhanden ist
            if(effectList.ContainsKey(effectName))
            {
                effectList[effectName].IsActive = false;
                effectList[effectName].UnloadContent();
            }
        }
    }
}
