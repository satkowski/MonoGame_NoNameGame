using System;
using System.Collections.Generic;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Managers;
using NoNameGame.Images.Effects;

namespace NoNameGame.Images
{
    public class Image
    {
        Vector2 origin;
        ContentManager content;
        Vector2 position;
        Dictionary<string, ImageEffect> effectList;

        [XmlIgnore]
        public Texture2D Texture;
        public string Path;
        public float Scale;
        public float Rotation;
        public float Alpha;
        public Color Color;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                updateRectangles();
                if(OnPositionChange != null)
                    OnPositionChange(position, null);
            }
        }
        public Vector2 Offset;
        public Rectangle SourceRectangle;
        public Rectangle CurrentRectangle;
        public Rectangle PrevRectangle;
        public int MergeOffset;
        [XmlIgnore]
        public Vector2 ScaledOrigin { private set; get; }

        public event EventHandler OnPositionChange;
        
        [XmlElement("Effect")]
        public List<String> Effects;
        public ScalingEffect ScalingEffect;
        public RotationEffect RotationEffect;
        public SpriteEffect SpriteEffectMoving;
        public SpriteEffect SpriteEffectStanding;
        public SpriteEffect SpriteEffectAlways;

        public Image ()
        {
            Position = Vector2.Zero;
            Scale = 1.0f;
            Alpha = 1.0f;
            Rotation = 0.0f;
            Path = String.Empty;
            SourceRectangle = Rectangle.Empty;
            PrevRectangle = Rectangle.Empty;
            CurrentRectangle = Rectangle.Empty;
            MergeOffset = 0;
            Color = Color.White;
            origin = Vector2.Zero;
            ScaledOrigin = Vector2.Zero;
            Offset = Vector2.Zero;
            effectList = new Dictionary<string, ImageEffect>();
            Effects = new List<string>();
        }

        public void LoadContent ()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (Path != String.Empty)
                Texture = content.Load<Texture2D>(Path);

            // Setzen der Effekte
            setEffect<ScalingEffect>(ref ScalingEffect);
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

            updateRectangles();
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position + ScaledOrigin + Offset, SourceRectangle, Color.White, Rotation, origin, Scale, SpriteEffects.None, 0.0f);
        }

        private void updateRectangles ()
        {
            origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            ScaledOrigin = new Vector2((SourceRectangle.Width * Scale) / 2, (SourceRectangle.Height * Scale) / 2);

            PrevRectangle = CurrentRectangle;
            CurrentRectangle = new Rectangle((int)(position.X), (int)(position.Y),
                                             (int)(SourceRectangle.Width * Scale), (int)(SourceRectangle.Height * Scale));
        }

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
            if(effectName != "")
                effectName = effectName.Insert(0, "-");

            string effectKeyName = effect.GetType().ToString().Replace("NoNameGame.Images.Effects.", "") + effectName;
            if(Effects.Contains(effectKeyName))
                effectList.Add(effectKeyName, (effect as ImageEffect));
        }

        public void ActivateEffect(string effectName)
        {
            if(effectList.ContainsKey(effectName))
            {
                effectList[effectName].IsActive = true;
                var obj = this;
                effectList[effectName].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effectName)
        {
            if(effectList.ContainsKey(effectName))
            {
                effectList[effectName].IsActive = false;
                effectList[effectName].UnloadContent();
            }
        }
    }
}
