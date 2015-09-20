using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using MapEditor.Images;
using MapEditor.Maps;

namespace MapEditor.WindowParts
{
    public class Editor : GraphicsDeviceControl
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        string[] selectorPath = { "MapEditor/TopLeft", "MapEditor/TopRight", "MapEditor/BottomLeft", "MapEditor/BottomRight" };
        List<float> selectorScales;
        bool isMouseDown = false;
        bool mouseOnScreen = false;
        Vector2 mousePosition;

        public int CurrentLayerNumber;
        public bool CreateNewMap = false;
        public event EventHandler OnInitialize;
        public Layer CurrentLayer
        {
            get 
            {
                if (Map != null && !CreateNewMap)
                    return Map.Layers[CurrentLayerNumber];
                else
                    return new Layer();
            }
        }
        public ContentManager Content
        {
            get { return content; }
        }
        public Map Map;
        public Vector2 SelectorDimensions;
        public Rectangle SelectedTileRegion;
        public List<Vector2> SelectedTiles;
        public List<Image> Selector;

        public bool DrawingAllowed;

        public Vector2 WindowPosition;
        public int ActualLayerSizeX { get; set; }
        public int ActualLayerSizeY { get; set; }

        public Editor ()
        {
            CurrentLayerNumber = 0;
            Selector = new List<Image>();
            SelectorDimensions = Vector2.Zero;
            SelectedTileRegion = new Rectangle(0, 0, 0, 0);
            SelectedTiles = new List<Vector2>();
            SelectedTiles.Add(Vector2.Zero);
            DrawingAllowed = false;
            WindowPosition = Vector2.Zero;
            ActualLayerSizeX = 0;
            ActualLayerSizeY = 0;
            selectorScales = new List<float>();

            Selector = new List<Image>();
            for (int c = 0; c < 4; c++)
                Selector.Add(new Image());

            MouseMove += Editor_MouseMove;
            MouseDown += Editor_MouseDown;
            MouseUp += delegate { isMouseDown = false; };
            MouseEnter += delegate { mouseOnScreen = true; };
            MouseLeave += delegate { mouseOnScreen = false; Draw(); Invalidate(); };
        }

        void Editor_MouseDown (object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (DrawingAllowed)
            {
                CurrentLayer.ReplaceTiles(mousePosition, SelectedTileRegion, WindowPosition);
                isMouseDown = true;
            }
        }

        void Editor_MouseMove (object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Vector2 windowPixelOffset = new Vector2((int)WindowPosition.X % (int)(CurrentLayer.TileDimensions.X * CurrentLayer.Scale),
                                                   (int)WindowPosition.Y % (int)(CurrentLayer.TileDimensions.Y * CurrentLayer.Scale));

            mousePosition = new Vector2((int)((e.X - CurrentLayer.Offset.X + windowPixelOffset.X) / (CurrentLayer.TileDimensions.X * CurrentLayer.Scale)),
                                        (int)((e.Y - CurrentLayer.Offset.Y + windowPixelOffset.Y) / (CurrentLayer.TileDimensions.Y * CurrentLayer.Scale)));
            mousePosition *= CurrentLayer.TileDimensions * CurrentLayer.Scale;

            int width = (int)(SelectedTileRegion.Width * CurrentLayer.TileDimensions.X * CurrentLayer.Scale);
            int heigth = (int)(SelectedTileRegion.Height * CurrentLayer.TileDimensions.Y * CurrentLayer.Scale);

            Selector[0].Position = mousePosition + CurrentLayer.Offset - windowPixelOffset;
            Selector[1].Position = new Vector2(mousePosition.X + width, mousePosition.Y) + CurrentLayer.Offset - windowPixelOffset;
            Selector[2].Position = new Vector2(mousePosition.X, mousePosition.Y + heigth) + CurrentLayer.Offset - windowPixelOffset;
            Selector[3].Position = new Vector2(mousePosition.X + width, mousePosition.Y + heigth) + CurrentLayer.Offset - windowPixelOffset;

            if (isMouseDown)
                Editor_MouseDown(this, null);

            Invalidate();
        }

        protected override void Initialize ()
        {
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int c = 0; c < 4; c++)
            {
                Selector[c].Path = selectorPath[c];
                Selector[c].Initialize(content);
                selectorScales.Add(Selector[0].Scale * CurrentLayer.Scale);
            }

            if (OnInitialize != null)
                OnInitialize(this, null);
        }

        public void ResetSelector ()
        {
            selectorScales.Clear();
            for (int c = 0; c < 4; c++)
            {
                Selector[c].Scale = CurrentLayer.TileDimensions.X / Selector[c].Texture.Width;
                selectorScales.Add(Selector[0].Scale * CurrentLayer.Scale);
            }
        }

        protected override void Draw ()
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            spriteBatch.Begin();
            if(Map != null)
                Map.Draw(spriteBatch, WindowPosition);
            if (mouseOnScreen)
                for(int c = 0; c < Selector.Count; c++)
                {
                    float originalScale = Selector[c].Scale;
                    Selector[c].Scale = selectorScales[c];
                    Selector[c].Draw(spriteBatch, Vector2.Zero);
                    Selector[c].Scale = originalScale;
                }
            spriteBatch.End();
        }
    }
}
