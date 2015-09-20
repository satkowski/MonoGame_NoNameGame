
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Maps;

namespace MapEditor.WindowParts
{
    public class Tile : GraphicsDeviceControl
    {
        Editor editor;
        SpriteBatch spriteBatch;

        public static MapEditor.Maps.Tile.TileRotation Rotation;

        public Tile (Editor editor)
        {
            this.editor = editor;
            Rotation = Maps.Tile.TileRotation.None;
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

                //Vector2 lastPosition = editor.Selector[0].Position;
                //for (int c = 1; c < editor.Selector.Count; c++)
                //{
                //    if (lastPosition != editor.Selector[c].Position)
                //        return;
                //    lastPosition = editor.Selector[c].Position;
                //}
                if(editor.SelectedTileRegion.Width == 0 &&
                   editor.SelectedTileRegion.Height == 0)
                {
                    float scale = Size.Width / editor.CurrentLayer.TileDimensions.X;
                    spriteBatch.Begin();
                    editor.CurrentLayer.DrawTile(spriteBatch, 
                                                 new Vector2(editor.SelectedTileRegion.X * editor.CurrentLayer.TileDimensions.X,
                                                             editor.SelectedTileRegion.Y * editor.CurrentLayer.TileDimensions.Y),
                                                 scale,
                                                 Rotation);
                    spriteBatch.End();
                }
            }
        }

        public void RotateTile (int rotation)
        {
            int newRotation = (int)Rotation + rotation;
            newRotation = (newRotation + 4) % 4;
            Rotation = Rotation.GetValueFromInt(newRotation);

            Invalidate();
        }
    }
}
