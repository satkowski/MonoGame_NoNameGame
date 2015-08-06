using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework;

using MapEditor.Images;

namespace MapEditor.WindowParts
{
    public class TileDisplay : GraphicsDeviceControl
    {
        SpriteBatch spriteBatch;
        Editor editor;
        Tile tile;
        Image tileSheet;
        List<Image> selector;
        bool isMouseDown;
        Vector2 mousePosition;
        Vector2 clickPosition;

        public TileDisplay (Editor editor, Tile tile)
        {
            this.editor = editor;
            this.tile = tile;
            editor.OnInitialize += LoadContent;
            isMouseDown = false;
        }

        void LoadContent (object sender, EventArgs e)
        {
            if (editor.CurrentLayer.TileSheet != null)
                tileSheet = editor.CurrentLayer.TileSheetImage;
            selector = editor.Selector;
        }

        protected override void Initialize ()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MouseDown += TileDisplay_MouseDown;
            MouseMove += TileDisplay_MouseMove;
            MouseUp += delegate
            {
                isMouseDown = false;

                List<Image> selector = editor.Selector;

                Rectangle selectedTileRegion = new Rectangle((int)selector[0].Position.X, (int)selector[0].Position.Y,
                                                             (int)(selector[1].Position.X - selector[0].Position.X),
                                                             (int)(selector[2].Position.Y - selector[0].Position.Y));
                selectedTileRegion.X /= (int)editor.CurrentLayer.TileDimensions.X;
                selectedTileRegion.Y /= (int)editor.CurrentLayer.TileDimensions.Y;
                selectedTileRegion.Width /= (int)editor.CurrentLayer.TileDimensions.X;
                selectedTileRegion.Height /= (int)editor.CurrentLayer.TileDimensions.Y;

                editor.SelectedTileRegion = selectedTileRegion;
            };
        }

        public void SetNewTileImage ()
        {
            if (editor.CurrentLayer.TileSheet != null)
                tileSheet = editor.CurrentLayer.TileSheetImage;
            Invalidate();
        }

        void TileDisplay_MouseDown (object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!isMouseDown)
            {
                clickPosition = mousePosition;
                foreach (Image img in selector)
                    img.Position = mousePosition;
                Tile.Rotation = Maps.Tile.TileRotation.None;
            }
            isMouseDown = true;
            Invalidate();
            tile.Invalidate();
        }

        void TileDisplay_MouseMove (object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mousePosition = new Vector2((int)(e.X / editor.CurrentLayer.TileDimensions.X),
                                        (int)(e.Y / editor.CurrentLayer.TileDimensions.Y));
            mousePosition.X *= editor.CurrentLayer.TileDimensions.X;
            mousePosition.Y *= editor.CurrentLayer.TileDimensions.Y;

            if (mousePosition != clickPosition && isMouseDown)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (c % 2 == 0 && mousePosition.X < clickPosition.X)
                        selector[c].Position.X = mousePosition.X;
                    else if (c % 2 == 1 && mousePosition.X > clickPosition.X)
                        selector[c].Position.X = mousePosition.X;

                    if (c < 2 && mousePosition.Y < clickPosition.Y)
                        selector[c].Position.Y = mousePosition.Y;
                    else if (c >= 2 && mousePosition.Y > clickPosition.Y)
                        selector[c].Position.Y = mousePosition.Y;
                }
                Invalidate();
                tile.Invalidate();
            }
            else if (isMouseDown)
            {
                foreach (Image img in selector)
                    img.Position = mousePosition;

                Invalidate();
                tile.Invalidate();
            }
        }

        protected override void Draw ()
        {
            GraphicsDevice.Clear(Color.Green);

            spriteBatch.Begin();
            if(tileSheet != null)
                tileSheet.Draw(spriteBatch);
            foreach (Image img in selector)
                img.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
