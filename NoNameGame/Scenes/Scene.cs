using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NoNameGame.Entities;
using NoNameGame.Maps;
using NoNameGame.Managers;

namespace NoNameGame.Scenes
{
    /// <summary>
    /// Stellt eine Szene des Spieles dar.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// Die Liste aller Spieler.
        /// </summary>
        private List<Entity> players;
        /// <summary>
        /// Die Liste aller Objekte, welche nicht Spieler sind.
        /// </summary>
        private List<Entity> entities;
        /// <summary>
        /// Liste aller Objekte, welche in einem Zyklus entfernt werden müssen.
        /// </summary>
        private List<Entity> entitiesToRemove;
        /// <summary>
        /// Liste aller Objekte, welche in einem Zyklus neu erschienen sind.
        /// </summary>
        private List<Entity> newEntities;

        /// <summary>
        /// Der Pfad der Karte dieser Sezene.
        /// </summary>
        public string MapPath;
        /// <summary>
        /// Eine Liste an Pfaden, aller Spieler dieser Szene.
        /// </summary>
        [XmlElement("PlayerPath")]
        public List<string> PlayerPaths;
        /// <summary>
        /// Eine Liste an Pfaden, für andere Objekte dieser Szene.
        /// </summary>
        [XmlElement("EntityPath")]
        public List<string> EntityPaths;        

        /// <summary>
        /// Die Karte dieser Szene.
        /// </summary>
        [XmlIgnore]
        public Map Map
        { private set; get; }
        /// <summary>
        /// Liste der Spieler dieser Szene.
        /// </summary>
        [XmlIgnore]
        public List<Entity> Players
        {
            get { return players; }
        }
        /// <summary>
        /// Liste an anderen Objekten dieser Szene.
        /// </summary>
        [XmlIgnore]
        public List<Entity> Entities
        {
            get { return players.Concat(entities).ToList(); }
        }

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public Scene()
        {
            MapPath = String.Empty;
            EntityPaths = new List<string>();
            PlayerPaths = new List<string>();
            entities = new List<Entity>();
            players = new List<Entity>();
            entitiesToRemove = new List<Entity>();
            newEntities = new List<Entity>();
        }

        public void LoadContent()
        {
            XmlManager<Map> mapLoader = new XmlManager<Map>();
            Map = mapLoader.Load(MapPath);
            Map.LoadContent();

            XmlManager<Entity> entityLoader = new XmlManager<Entity>();

            // Läd alle Spieler
            foreach(string playerPath in PlayerPaths)
            {
                Entity newPlayer = entityLoader.Load(playerPath);
                newPlayer.LoadContent();
                players.Add(newPlayer);
            }

            // Läd alle anderen Entities
            foreach(string entityPath in  EntityPaths)
            {
                Entity newEntity = entityLoader.Load(entityPath);
                newEntity.LoadContent();

                // Kontrolliert ob Enities mit gewissen Abilities noch besondere Vorraussetzungen brauchen
                if(newEntity.Abilities.Contains("PlayerFollowingAbility"))
                    players[0].Body.OnPositionChange += delegate
                    { newEntity.PlayerFollowingAbility.PlayerPosition = players[0].Body.Position; };

                if(newEntity.Abilities.Contains("MovingAbility"))
                    newEntity.MovingAbility.Start = newEntity.Body.Position;
                else if(newEntity.Abilities.Contains("ShootingAbility"))
                {
                    newEntity.ShootingAbility.OnNewShotEntityCreated += ShootingAbility_OnNewShotEntityCreated;
                    newEntity.Body.OnPositionChange += delegate
                    { newEntity.ShootingAbility.StartPosition = newEntity.Body.Position; };

                    if(newEntity.ShootingAbility.Type == NoNameGame.Entities.Abilities.ShootingAbility.ShootingType.AgainstPlayer)
                        players[0].Body.OnPositionChange += delegate
                        { newEntity.ShootingAbility.DestinationPosition = players[0].Body.Position; };
                }
                entities.Add(newEntity);
            }
        }

        public void UnloadContent()
        {
            Map.UnloadContent();
            foreach(Entity player in players)
                player.UnloadContent();
            foreach(Entity entity in entities)
                entity.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Map.Update(gameTime);

            foreach(Entity player in players)
                player.Update(gameTime);
            foreach(Entity entity in entities)
                entity.Update(gameTime);
            // Füge neu erzeugte Entities hinzu
            foreach(Entity newEntity in newEntities)
            {
                newEntity.Update(gameTime);
                entities.Add(newEntity);
            }
            newEntities.Clear();
            // Entferne nicht mehr gebrauchte Entities
            foreach(Entity entity in entitiesToRemove)
            {
                players.Remove(entity);
                entities.Remove(entity);
            }
            entitiesToRemove.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            foreach(Entity player in players)
                player.Draw(spriteBatch);
            foreach(Entity entity in entities)
                entity.Draw(spriteBatch);
        }

        /// <summary>
        /// Reagiert, wenn ein neues Schussobjekt erstellt wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShootingAbility_OnNewShotEntityCreated(object sender, System.EventArgs e)
        {
            Entity entity = (Entity)sender;
            entity.MovingAbility.OnEndingReached += delegate
            { entitiesToRemove.Add(entity); };
            newEntities.Add(entity);
        }
    }
}
