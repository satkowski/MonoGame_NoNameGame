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
    public class Scene
    {
        private List<Entity> players;
        private List<Entity> entities;
        private List<Entity> entitiesToRemove;
        private List<Entity> newEntities;

        public string MapPath;
        [XmlElement("PlayerPath")]
        public List<string> PlayerPaths;
        [XmlElement("EntityPath")]
        public List<string> EntityPaths;        

        [XmlIgnore]
        public Map Map
        { private set; get; }
        [XmlIgnore]
        public List<Entity> Players
        {
            get { return players; }
        }
        [XmlIgnore]
        public List<Entity> Entities
        {
            get { return players.Concat(entities).ToList(); }
        }

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

            foreach(string playerPath in PlayerPaths)
            {
                Entity newPlayer = entityLoader.Load(playerPath);
                newPlayer.LoadContent();
                players.Add(newPlayer);
            }

            foreach(string entityPath in  EntityPaths)
            {
                Entity newEntity = entityLoader.Load(entityPath);
                newEntity.LoadContent();

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
            foreach(Entity entity in newEntities)
            {
                entity.Update(gameTime);
                entities.Add(entity);
            }
            newEntities.Clear();
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

        private void ShootingAbility_OnNewShotEntityCreated(object sender, System.EventArgs e)
        {
            Entity entity = (Entity)sender;
            entity.MovingAbility.OnEndingReached += delegate
            { entitiesToRemove.Add(entity); };
            newEntities.Add(entity);
        }
    }
}
