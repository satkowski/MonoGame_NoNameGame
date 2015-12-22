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
            endImageScale = StartImageScale + (int)Direction * TotalScalingChange;
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
                if((Direction == ScaleDirection.Up && image.Scale > endImageScale) ||
                   (Direction == ScaleDirection.Down && image.Scale < endImageScale))
                {
                    newScale = endImageScale - image.Scale;
                    if(ActionType == ScaleActionType.OneWay)
                        IsActive = false;
                    else if(ActionType == ScaleActionType.Repeating)
                    {
                        float endImageScaleTemp = endImageScale;
                        endImageScale = StartImageScale;
                        StartImageScale = endImageScaleTemp;
                        Direction = (ScaleDirection)(-1 * (int)Direction);
                    }
                }
                image.Scale += newScale;
            }
        }

        /// <summary>
        /// Kopieren diese übergebenen Effekts.
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
            return newEffect;
        }
    }
}
