using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Images;
using NoNameGame.Maps;
using NoNameGame.Extensions;

namespace NoNameGame.Entities
{
    public class Entity
    {
        public Image Image;
        [XmlIgnore]
        public Vector2 MoveVelocity;
        public int CollisionLevel;

        protected Entity ()
        {
            MoveVelocity = Vector2.Zero;
            CollisionLevel = 0;
        }

        public virtual void LoadContent ()
        {
            Image.LoadContent();
        }

        public virtual void UnloadContent ()
        {
            Image.UnloadContent();
        }

        public virtual void Update (GameTime gameTime, Map map)
        {
            collisionHandling(map);

            Image.Update(gameTime);
        }

        public virtual void Draw (SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }

        private void collisionHandling (Map map)
        {
            List<Rectangle> collidingEnteties = map.GetCollidingTileRectangles(Image.CurrentRectangle, CollisionLevel);

            foreach (Rectangle tileRectangle in collidingEnteties)
            {
                Vector2 collidingDepth = tileRectangle.GetIntersectionDepth(Image.CurrentRectangle);
                Vector2 collidingDirection = getCollisionSolving(tileRectangle);

                Image.Position += collidingDirection * collidingDepth;
            }
        }

        private Vector2 getCollisionSolving (Rectangle entityRectangle)
        {
            Vector2 collision = Vector2.Zero;
            if (entityRectangle.Intersects(Image.CurrentRectangle) && !entityRectangle.Intersects(Image.PrevRectangle))
            {
                // welche Seite vom Tile ist mit dem Spieler kollidiert
                bool leftTileCollision = Image.CurrentRectangle.Right > entityRectangle.Left &&
                                         Image.CurrentRectangle.Right < entityRectangle.Right;
                bool rightTileCollision = Image.CurrentRectangle.Left < entityRectangle.Right &&
                                          Image.CurrentRectangle.Left > entityRectangle.Left;
                bool topTileCollision = Image.CurrentRectangle.Bottom > entityRectangle.Top &&
                                        Image.CurrentRectangle.Bottom < entityRectangle.Bottom;
                bool bottomTileCollision = Image.CurrentRectangle.Top < entityRectangle.Bottom &&
                                           Image.CurrentRectangle.Top > entityRectangle.Top;

                bool playerDirChangeX = Image.PrevRectangle.Right != Image.CurrentRectangle.Right ||
                                        Image.PrevRectangle.Left != Image.CurrentRectangle.Left;
                bool playerDirChangeY = Image.PrevRectangle.Bottom != Image.CurrentRectangle.Bottom ||
                                        Image.PrevRectangle.Top != Image.CurrentRectangle.Top;

                if (bottomTileCollision)
                {
                    if (rightTileCollision)
                    {
                        if (leftTileCollision)
                            collision.Y = 1;
                        else
                        {
                            if (playerDirChangeX)
                            {
                                if (playerDirChangeY)
                                    collision.Y = 1;
                                else
                                    collision.X = 1;
                            }
                            else
                                if (playerDirChangeY)
                                    collision.Y = 1;
                        }
                    }
                    else if (leftTileCollision)
                    {
                        if (playerDirChangeX)
                        {
                            if (playerDirChangeY)
                                collision.Y = 1;
                            else
                                collision.X = -1;
                        }
                        else
                            if (playerDirChangeY)
                                collision.Y = 1;
                    }
                    else
                        collision.Y = 1;
                }
                else if (topTileCollision)
                {
                    if (rightTileCollision)
                    {
                        if (leftTileCollision)
                            collision.Y = -1;
                        else
                        {
                            if (playerDirChangeX)
                            {
                                if (playerDirChangeY)
                                    collision.Y = -1;
                                else
                                    collision.X = 1;
                            }
                            else
                                if (playerDirChangeY)
                                    collision.Y = -1;
                        }
                    }
                    else if (leftTileCollision)
                    {
                        if (playerDirChangeX)
                        {
                            if (playerDirChangeY)
                                collision.Y = -1;
                            else
                                collision.X = -1;
                        }
                        else
                            if (playerDirChangeY)
                                collision.Y = -1;
                    }
                    else
                        collision.Y = -1;
                }
                else if (leftTileCollision)
                {
                    //if (topTileCollision && bottomTileCollision)
                    //    collision.X = -1;
                    //else
                    collision.X = -1;
                }
                else if (rightTileCollision)
                {
                    //if (topTileCollision && bottomTileCollision)
                    //    collision.X = 1;
                    //else
                    collision.X = 1;
                }
            }
            return collision;
        }
    }
}
