using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using MapEditor.Images;
using MapEditor.Managers;
using MapEditor.Maps;

namespace MapEditor.WindowParts
{
    public class Editor : GraphicsDeviceControl
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        string[] selectorPath = { "MapEditor/TopLeft", "MapEditor/TopRight", "MapEditor/BottomLeft", "MapEditor/BottomRight" };
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

        public bool OneLayerActive;

        public List<Image> Selector;

        public Editor ()
        {
            CurrentLayerNumber = 0;
            Selector = new List<Image>();
            SelectorDimensions = Vector2.Zero;
            SelectedTileRegion = new Rectangle(0, 0, 0, 0);
            SelectedTiles = new List<Vector2>();
            SelectedTiles.Add(Vector2.Zero);
            OneLayerActive = false;

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
            if (OneLayerActive)
            {
                CurrentLayer.ReplaceTiles(mousePosition, SelectedTileRegion);
                isMouseDown = true;
            }
        }

        void Editor_MouseMove (object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mousePosition = new Vector2((int)(e.X / CurrentLayer.TileDimensions.X),
                                        (int)(e.Y / CurrentLayer.TileDimensions.Y));
            mousePosition *= CurrentLayer.TileDimensions.X;

            int width = (int)(SelectedTileRegion.Width * CurrentLayer.TileDimensions.X);
            int heigth = (int)(SelectedTileRegion.Height * CurrentLayer.TileDimensions.Y);

            Selector[0].Position = mousePosition;
            Selector[1].Position = new Vector2(mousePosition.X + width, mousePosition.Y);
            Selector[2].Position = new Vector2(mousePosition.X, mousePosition.Y + heigth);
            Selector[3].Position = new Vector2(mousePosition.X + width, mousePosition.Y + heigth);

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
            }

            if (OnInitialize != null)
                OnInitialize(this, null);
        }

        public void ResetSelector ()
        {
            for (int c = 0; c < 4; c++)
                Selector[c].Scale = CurrentLayer.TileDimensions.X / Selector[c].Texture.Width;
        }

        protected override void Draw ()
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            spriteBatch.Begin();
            if(Map != null)
                Map.Draw(spriteBatch);
            if (mouseOnScreen)
                foreach (Image img in Selector)
                    img.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
