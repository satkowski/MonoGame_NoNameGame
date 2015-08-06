using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MapEditor.WindowParts;
using MapEditor.Images;

namespace MapEditor.WindowParts
{
    public class Tile : GraphicsDeviceControl
    {
        Editor editor;
        SpriteBatch spriteBatch;

        public float Rotation;

        public Tile (Editor editor)
        {
            this.editor = editor;
            Rotation = 0.0f;
        }

        protected override void Initialize ()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Draw ()
        {
            if (editor.CurrentLayer.TileSheet != null)
            {
                GraphicsDevice.Clear(Color.Violet);

                Vector2 lastPosition = editor.Selector[0].Position;
                for (int c = 1; c < editor.Selector.Count; c++)
                {
                    if (lastPosition != editor.Selector[c].Position)
                        return;
                    lastPosition = editor.Selector[c].Position;
                }

                spriteBatch.Begin();
                editor.CurrentLayer.DrawTile(spriteBatch, lastPosition);
                spriteBatch.End();
            }
        }

        public void RotateTile (float rotation)
        {
            this.Rotation += rotation;

            if (this.Rotation >= MathHelper.TwoPi)
                this.Rotation -= MathHelper.TwoPi;
            else if (this.Rotation <= -MathHelper.TwoPi)
                this.Rotation += MathHelper.TwoPi;

            Invalidate();
        }
    }
}
