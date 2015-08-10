using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework;

using MapEditor.Images;
using MapEditor.Extension;

namespace MapEditor.WindowParts
{
    public class TileDisplay : GraphicsDeviceControl
    {
        SpriteBatch spriteBatch;
        Editor editor;
        Tile tile;
        List<Vector2> selectorPositons;
        Image tileSheet;
        bool isMouseDown;
        Vector2 mousePosition;
        Vector2 clickPosition;

        public Vector2 WindowPosition;

        public TileDisplay (Editor editor, Tile tile)
        {
            this.editor = editor;
            this.tile = tile;
            editor.OnInitialize += LoadContent;
            isMouseDown = false;
            selectorPositons = new List<Vector2>();
            WindowPosition = Vector2.Zero;
        }

        void LoadContent (object sender, EventArgs e)
        {
            if (editor.CurrentLayer.TileSheet != null)
                tileSheet = editor.CurrentLayer.TileSheetImage;
            foreach (Image img in editor.Selector)
                selectorPositons.Add(img.Position);
        }

        protected override void Initialize ()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MouseDown += TileDisplay_MouseDown;
            MouseMove += TileDisplay_MouseMove;
            MouseUp += delegate
            {
                isMouseDown = false;

                Rectangle selectedTileRegion = new Rectangle((int)selectorPositons[0].X, (int)selectorPositons[0].Y,
                                                             (int)(selectorPositons[1].X - selectorPositons[0].X),
                                                             (int)(selectorPositons[2].Y - selectorPositons[0].Y));
                selectedTileRegion.X /= (int)editor.CurrentLayer.TileDimensions.X;
                selectedTileRegion.Y /= (int)editor.CurrentLayer.TileDimensions.Y;
                selectedTileRegion.Width /= (int)editor.CurrentLayer.TileDimensions.X;
                selectedTileRegion.Height /= (int)editor.CurrentLayer.TileDimensions.Y;

                editor.SelectedTileRegion = selectedTileRegion;

                tile.Invalidate();
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
            if (editor.DrawingAllowed)
            {
                if (!isMouseDown)
                {
                    clickPosition = mousePosition;

                    Vector2 windowPixelOffset = new Vector2((int)WindowPosition.X % (int)editor.CurrentLayer.TileDimensions.X,
                                                           (int)WindowPosition.Y % (int)editor.CurrentLayer.TileDimensions.Y);

                    for (int c = 0; c < 4; c++)
                        selectorPositons[c] = mousePosition - windowPixelOffset;
                    Tile.Rotation = Maps.Tile.TileRotation.None;
                }
                isMouseDown = true;

                Invalidate();
            }
        }

        void TileDisplay_MouseMove (object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Vector2 windowPixelOffset = new Vector2((int)WindowPosition.X % (int)editor.CurrentLayer.TileDimensions.X,
                                                   (int)WindowPosition.Y % (int)editor.CurrentLayer.TileDimensions.Y);

            mousePosition = new Vector2((int)((e.X + windowPixelOffset.X) / editor.CurrentLayer.TileDimensions.X),
                                        (int)((e.Y + windowPixelOffset.Y) / editor.CurrentLayer.TileDimensions.Y));
            mousePosition*= editor.CurrentLayer.TileDimensions;

            if (mousePosition != clickPosition && isMouseDown)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (c % 2 == 0 && mousePosition.X < clickPosition.X)
                        selectorPositons[c] = new Vector2(mousePosition.X - windowPixelOffset.X, selectorPositons[c].Y);
                    else if (c % 2 == 1 && mousePosition.X > clickPosition.X)
                        selectorPositons[c] = new Vector2(mousePosition.X - windowPixelOffset.X, selectorPositons[c].Y);

                    if (c < 2 && mousePosition.Y < clickPosition.Y)
                        selectorPositons[c] = new Vector2(selectorPositons[c].X, mousePosition.Y - windowPixelOffset.Y);
                    else if (c >= 2 && mousePosition.Y > clickPosition.Y)
                        selectorPositons[c] = new Vector2(selectorPositons[c].X, mousePosition.Y - windowPixelOffset.Y);
                }
                Invalidate();
            }
        }

        protected override void Draw ()
        {
            GraphicsDevice.Clear(Color.Green);

            if (editor.DrawingAllowed)
            {
                List<Vector2> originalPositions = new List<Vector2>();
                for (int c = 0; c < 4; c++)
                {
                    originalPositions.Add(editor.Selector[c].Position);
                    editor.Selector[c].Position = selectorPositons[c];
                }

                spriteBatch.Begin();
                if (tileSheet != null)
                    tileSheet.Draw(spriteBatch, WindowPosition);
                foreach (Image img in editor.Selector)
                    img.Draw(spriteBatch, Vector2.Zero);
                spriteBatch.End();

                for (int c = 0; c < 4; c++)
                    editor.Selector[c].Position = originalPositions[c];
            }
        }
    }
}
