using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace NoNameGame.Images.Effects
{
    /// <summary>
    /// Stellt die Skalierung eines Bildes dar.
    /// </summary>
    public class ScalingEffect : ImageEffect
    {
        /// <summary>
        /// In welche Richtung wird skaliert.
        /// </summary>
        public enum ScaleDirection
        {
            Down = -1,
            Up = 1,
            None = 0
        }
        /// <summary>
        /// Für die Möglichkeiten wie die Aktion ablaufen soll.
        /// </summary>
        public enum ScaleActionType
        {
            OneWay,
            Repeating
        }

        /// <summary>
        /// Die Endskalierung des Bildes.
        /// </summary>
        float endImageScale;
        /// <summary>
        /// Die Skalierung, die das Bild wirklich am Anfang hatte.
        /// </summary>
        float startImageScaleOrigin;
        /// <summary>
        /// Die Skalierung, die das Bild wirklich am Ende haben sollte.
        /// </summary>
        float endImageScaleOrigin;

        /// <summary>
        /// Gibt an in welche Richtung skaliert wird.
        /// </summary>
        public ScaleDirection Direction;
        /// <summary>
        /// Gibt an wie der Effekt ablaufen soll.
        /// </summary>
        public ScaleActionType ActionType;
        /// <summary>
        /// Die Startskalierung des Bildes.
        /// </summary>
        public float StartImageScale;
        /// <summary>
        /// Die insgesamter Veränderung 
        /// </summary>
        public float TotalScalingChange;
        /// <summary>
        /// Die Skalierung die pro Millisekunde ausgeführt wird.
        /// </summary>
        public float ScalingPerMillisecond;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public ScalingEffect()
        {
            endImageScale = 0.0f;
            Direction = ScaleDirection.None;
            ActionType = ScaleActionType.OneWay;
            StartImageScale = 0.0f;
            ScalingPerMillisecond = 0.0f;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);

            StartImageScale = image.Scale;
            startImageScaleOrigin = StartImageScale;
            endImageScale = StartImageScale + (int)Direction * TotalScalingChange;
            endImageScaleOrigin = endImageScale;
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
                float newScale = (int)Direction * ScalingPerMillisecond * elapsedTime;

                // Entscheide was passiert, wenn der Effekt fertig ist.
                if((Direction == ScaleDirection.Up && Image.Scale + newScale > endImageScale) ||
                   (Direction == ScaleDirection.Down && Image.Scale + newScale < endImageScale))
                {
                    newScale = endImageScale - Image.Scale;
                    Image.Scale += newScale;

                    if(ActionType == ScaleActionType.OneWay)
                    {
                        IsActive = false;
                        onEffectFinished();
                    }
                    else if(ActionType == ScaleActionType.Repeating)
                        RevertEffect(true);
                }
                else
                    Image.Scale += newScale;
            }
        }

        /// <summary>
        /// Kopieren dieses Effekts.
        /// </summary>
        public override ImageEffect Copy()
        {
            ScalingEffect newEffect = new ScalingEffect();
            newEffect.ActionType = this.ActionType;
            newEffect.Direction = this.Direction;
            newEffect.IsActive = this.IsActive;
            newEffect.ScalingPerMillisecond = this.ScalingPerMillisecond;
            newEffect.StartImageScale = this.StartImageScale;
            newEffect.TotalScalingChange = this.TotalScalingChange;
            base.CopyEvents(newEffect);
            return newEffect;
        }

        public override void RevertEffect(bool active)
        {
            if(Direction == ScaleDirection.Up)
            {
                Direction = ScaleDirection.Down;
                StartImageScale = endImageScale;
                endImageScale = startImageScaleOrigin;
            }
            else if(Direction == ScaleDirection.Down)
            {
                Direction = ScaleDirection.Up;
                StartImageScale = endImageScale;
                endImageScale = endImageScaleOrigin;
            }
            IsActive = active;
        }
    }
}
