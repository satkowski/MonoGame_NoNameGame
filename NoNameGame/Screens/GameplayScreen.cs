
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

using NoNameGame.Maps;
using NoNameGame.Managers;
using NoNameGame.Entities;
using NoNameGame.Screens.Managers;
using NoNameGame.Extensions;

namespace NoNameGame.Screens
{
    public class GameplayScreen : Screen
    {
        ZoomingManager zoomingManager;
        CollisionManager collisionManager;
        List<Entity> entitiesToRemove;

        public Map Map;
        public Entity Player;
        public List<Entity> Enemies;
        public List<Entity> Shots;

        public GameplayScreen ()
        {
            zoomingManager = new ZoomingManager();
            collisionManager = new CollisionManager();
            Map = new Maps.Map();
            Player = new Entity();
            Enemies = new List<Entity>();
            Shots = new List<Entity>();
            entitiesToRemove = new List<Entity>();
        }

        public override void LoadContent ()
        {
            base.LoadContent();

            XmlManager<Map> mapLoader = new XmlManager<Map>();
            Map = mapLoader.Load("Load/Maps/Map_005.xml");
            Map.LoadContent();

            XmlManager<Entity> playerLoader = new XmlManager<Entity>();
            Player = playerLoader.Load("Load/Entities/Players/Player_01.xml");
            Player.LoadContent(Entity.EntityType.Player);

            XmlManager<Entity> enemyLoader = new XmlManager<Entity>();
            Enemies.Add(enemyLoader.Load("Load/Entities/Enemies/Enemy_001.xml"));
            Enemies.Add(enemyLoader.Load("Load/Entities/Enemies/Enemy_002.xml"));
            Enemies.Add(enemyLoader.Load("Load/Entities/Enemies/Enemy_003.xml"));
            foreach(Entity enemy in Enemies)
            {
                enemy.LoadContent(Entity.EntityType.Enemy);
                if(enemy.Abilities.Contains("PlayerFollowingAbility"))
                    Player.Image.OnPositionChange += delegate
                    { enemy.PlayerFollowingAbility.PlayerPosition = Player.Image.Position; };
                if(enemy.Abilities.Contains("MovingAbility"))
                    enemy.MovingAbility.Start = enemy.Image.Position;
                else if(enemy.Abilities.Contains("ShootingAbility"))
                {
                    enemy.ShootingAbility.OnNewShotEntityCreated += ShootingAbility_OnNewShotEntityCreated;
                    enemy.Image.OnPositionChange += delegate
                    { enemy.ShootingAbility.StartPosition = enemy.Image.Position; };
                    if(enemy.ShootingAbility.Type == Entities.Abilities.ShootingAbility.ShootingType.AgainstPlayer)
                        Player.Image.OnPositionChange += delegate
                        { enemy.ShootingAbility.DestinationPosition = Player.Image.Position; };
                }
            }

            zoomingManager.LoadContent(ref Map, Player, Enemies[0], Enemies[1], Enemies[2]);
            zoomingManager.Type = ZoomingManager.ZoomingType.OneTime;
            zoomingManager.MaxZoom = 3.0f;
            zoomingManager.MinZoom = -0.85f;
            zoomingManager.ZoomingFactor = 0.001f;
            collisionManager.LoadContent(ref Map, Player, Enemies[0], Enemies[1], Enemies[2]);
        }

        private void ShootingAbility_OnNewShotEntityCreated(object sender, System.EventArgs e)
        {
            Entity entity = (Entity)sender;
            (entity).MovingAbility.OnEndingReached += delegate
            { entitiesToRemove.Add(entity); };
            Shots.Add(entity);
            zoomingManager.AddEntity(entity);
            collisionManager.AddEntity(entity);
        }

        public override void UnloadContent ()
        {
            Map.UnloadContent();
            Player.UnloadContent();
            zoomingManager.UnloadContent();
            collisionManager.UnloadContent();
        }

        public override void Update (GameTime gameTime)
        {
            if(InputManager.Instance.KeyDown(Keys.OemPlus)) 
            {
                zoomingManager.Direction = ZoomingManager.ZoomingDirection.In;
                zoomingManager.IsActive = true;
            }
            else if(InputManager.Instance.KeyDown(Keys.OemMinus))
            {
                zoomingManager.Direction = ZoomingManager.ZoomingDirection.Out;
                zoomingManager.IsActive = true;
            }
            zoomingManager.Update(gameTime);

            Player.Update(gameTime, Map);
            foreach(Entity enemy in Enemies)
                enemy.Update(gameTime, Map);
            foreach(Entity shot in Shots)
                shot.Update(gameTime, Map);
            foreach(Entity entity in entitiesToRemove)
            {
                Enemies.Remove(entity);
                Shots.Remove(entity);
                zoomingManager.RemoveEntity(entity);
                collisionManager.RemoveEntity(entity);
            }
            entitiesToRemove.Clear();

            collisionManager.Update(gameTime);

            Map.Update(gameTime);

            Vector2 offset = ScreenManager.Instance.Dimensions / 2 - Player.Image.Position;
            Player.Image.Offset = offset.RoundDownToIntVector2();
            foreach(Entity enemy in Enemies)
                enemy.Image.Offset = Player.Image.Offset;
            foreach(Entity shot in Shots)
                shot.Image.Offset = Player.Image.Offset;
            foreach(Layer layer in Map.Layers)
                layer.TileSheet.Offset = Player.Image.Offset;

            //Vector2 actualOffset = Player.Image.Offset;
            //Vector2 offsetChange = Vector2.Zero;
            //if (Player.Image.CurrentRectangle.InVerticalDirection(Map.CamMovingRectangle))
            //    offsetChange.Y -= Player.MoveVelocity.Y + Player.CollisionMovement.Y;
            //else
            //{
            //    if (Player.Image.CurrentRectangle.OverlapTop(Map.CamMovingRectangle))
            //        offsetChange.Y = -actualOffset.Y;
            //    else if (Player.Image.CurrentRectangle.OverlapBottom(Map.CamMovingRectangle))
            //        offsetChange.Y = ScreenManager.Instance.Dimensions.Y / 2 + actualOffset.Y;
            //}
            //if (Player.Image.CurrentRectangle.InHorizontalDirection(Map.CamMovingRectangle))
            //    offsetChange.X -= Player.MoveVelocity.X + Player.CollisionMovement.X;
            //else
            //{
            //    if (Player.Image.CurrentRectangle.OverlapLeft(Map.CamMovingRectangle))
            //        offsetChange.X = -actualOffset.X;
            //    else if (Player.Image.CurrentRectangle.OverlapRight(Map.CamMovingRectangle))
            //        offsetChange.X = ScreenManager.Instance.Dimensions.X / 2 + actualOffset.X;
            //}

            //if (offsetChange != Vector2.Zero)
            //{
            //    foreach (Layer layer in Map.Layers)
            //        layer.TileSheet.Offset += offsetChange;
            //    Player.Image.Offset += offsetChange;
            //}
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            foreach(Entity enemy in Enemies)
                enemy.Draw(spriteBatch);
            foreach(Entity shot in Shots)
                shot.Draw(spriteBatch);
        }
    }
}
