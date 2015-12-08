using System;
using System.Collections.Generic;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Images;
using NoNameGame.Entities.Abilities;
using NoNameGame.Components;
using NoNameGame.Components.Shapes;
using NoNameGame.Managers;

namespace NoNameGame.Entities
{
    /// <summary>
    /// Stellt alle möglichen Entities des Spieles dar.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Gibt die Art der Entity an.
        /// </summary>
        public enum EntityType
        {
            Player,
            Enemy,
            Shot
        }

        Dictionary<string, EntityAbility> abilitiesList;

        /// <summary>
        /// ID des Objektes.
        /// </summary>
        [XmlIgnore]
        public ulong ID
        { get; private set; }

        /// <summary>
        /// Das Bild der Entity.
        /// </summary>
        public Image Image;
        /// <summary>
        /// DerKörper der Entity.
        /// </summary>
        public Body Body;
        /// <summary>
        /// Die Shape der Entity.
        /// </summary>
        public Shape Shape;
        /// <summary>
        /// Der ShapeType der Entity.
        /// </summary>
        public EntityType Type;

        /// <summary>
        /// Eine Liste von Fähigkeiten, die die Entity besitzt.
        /// </summary>
        [XmlElement("Ability")]
        public List<string> Abilities;
        /// <summary>
        /// Die Spieler-Verfolgungs-Fähigkeit des Entity.
        /// </summary>
        public PlayerFollowingAbility PlayerFollowingAbility;
        /// <summary>
        /// Die Bewegungs-Fähigkeit des Entity.
        /// </summary>
        public MovingAbility MovingAbility;
        /// <summary>
        /// Die Schieß-Fähigkeit des Entity.
        /// </summary>
        public ShootingAbility ShootingAbility;
        /// <summary>
        /// Die Spieler-Kontrollierungs-Fähigkeit des Entity.
        /// </summary>
        public UserControlledAbility UserControlledAbility;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public Entity()
        {
            Type = EntityType.Enemy;
            Shape = new AABBShape();
            abilitiesList = new Dictionary<string, EntityAbility>();
            Abilities = new List<string>();
            ID = IDManager.Instance.EntityID;
        }

        /// <summary>
        /// Lädt alles und aktiviert die Abilities.
        /// </summary>
        public void LoadContent()
        {
            // TODO: Mögliches verschieben
            // Zuweisung aller Events.
            if(Shape.Type == ShapeType.OBB)
            {
                Image.OnRotationChange += delegate
                { (Shape as OBBShape).Rotation = Image.Rotation; };
                Image.OnRotationChange += delegate
                { Body.Rotated = true; };
            }
            Image.OnScaleChange += delegate
            { Shape.Scale = Image.Scale; };
            Shape.OnAreaChanged += delegate
            { Body.Area = Shape.Area; };

            Body.LoadContent();
            Image.LoadContent(Body);
            Shape.LoadContent(Body);

            setAbility<PlayerFollowingAbility>(ref PlayerFollowingAbility);
            setAbility<MovingAbility>(ref MovingAbility);
            setAbility<ShootingAbility>(ref ShootingAbility);
            setAbility<UserControlledAbility>(ref UserControlledAbility);
            foreach(string abilitiesName in Abilities)
                ActivateAbility(abilitiesName);
        }

        /// <summary>
        /// Setzt alle möglichen Abilities.
        /// </summary>
        /// <typeparam name="T">die Art der Ability</typeparam>
        /// <param name="ability">die Ability</param>
        /// <param name="abilityName">der Name der Ability</param>
        void setAbility<T>(ref T ability, string abilityName = "")
        {
            if(ability == null)
                ability = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (ability as EntityAbility).IsActive = true;
                var obj = this;
                (ability as EntityAbility).LoadContent(ref obj);
            }
            // Dem Abilitynamen wird noch ein "-" vorangestellt
            if(abilityName != "")
                abilityName = abilityName.Insert(0, "-");

            // Nun wird der Klassennamen mit dem Abilitynmane verbunden, um dieses besser handhaben zu können
            string abilityKeyName = ability.GetType().ToString().Replace("NoNameGame.Entities.Abilities.", "") + abilityName;
            if(Abilities.Contains(abilityKeyName))
                abilitiesList.Add(abilityKeyName, (ability as EntityAbility));
        }

        /// <summary>
        /// Aktiviert eine Ability
        /// </summary>
        /// <param name="abilityName">der Name der Ability</param>
        public void ActivateAbility(string abilityName)
        {
            // Es wird nur die Ability aktiviert, wenn diese auch in der Liste vorhanden ist
            if(abilitiesList.ContainsKey(abilityName))
            {
                abilitiesList[abilityName].IsActive = true;
                var obj = this;
                abilitiesList[abilityName].LoadContent(ref obj);
            }
        }

        /// <summary>
        /// Deaktiviert eine Ability
        /// </summary>
        /// <param name="abilityName">der Name der Ability</param>
        public void DeactivateAbility(string abilityName)
        {
            // Es wird nur die Ability deaktiviert, wenn diese auch in der Liste vorhanden ist
            if(abilitiesList.ContainsKey(abilityName))
            {
                abilitiesList[abilityName].IsActive = false;
                abilitiesList[abilityName].UnloadContent();
            }
        }
        public virtual void UnloadContent()
        {
            Image.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            Body.Rotated = false;
            Body.Velocity = Vector2.Zero;

            foreach(var ability in abilitiesList)
                ability.Value.Update(gameTime);

            Body.Position += Body.Velocity * Body.SpeedFactor;

            Image.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch, Body.Position);
        }
    }
}
