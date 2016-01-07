using Microsoft.Xna.Framework;
using NoNameGame.Components;
using NoNameGame.Components.Shapes;
using NoNameGame.Entities;
using NoNameGame.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameGame.Collisions
{
    /// <summary>
    /// Repräsentiert eine Baum mit maximal 4 Kindern. Wird genutzt um die Kollisionsberechung zu optimieren.
    /// </summary>
    public class Quadtree
    {
        /// <summary>
        /// Maximale Anzahl an Objekten, welche gleichzeitig auf einer Ebene liegen können.
        /// </summary>
        private int maxObjects;
        /// <summary>
        /// Maximale Anzahl an Ebenen, die der Baum haben kann.
        /// </summary>
        private int maxLevels;

        /// <summary>
        /// Aktuelle Ebene dieses Quadtrees.
        /// </summary>
        private int level;
        /// <summary>
        /// Liste aller Objekte, die in dieser Ebene liegen.
        /// </summary>
        private List<Tuple<Body, Shape>> objectList;
        /// <summary>
        /// Der Bereich, der dieser Quadtree einnimmt.
        /// </summary>
        private Rectangle boundingRectangle;
        /// <summary>
        /// Die Kinder dieses Quadtrees.
        /// </summary>
        private Quadtree[] quadtreeChildren;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        /// <param name="level">die Ebene dieses Quadtrees</param>
        /// <param name="boundingRectangle">das Rechteck, dass dieser Quadtree einnimmt</param>
        public Quadtree(int level, Rectangle boundingRectangle)
        {
            maxObjects = 15;
            maxLevels = 6;

            this.level = level;
            this.boundingRectangle = boundingRectangle;

            objectList = new List<Tuple<Body, Shape>>();
            quadtreeChildren = null;
        }

        /// <summary>
        /// Löscht den ganzen Quadtree rekursiv mit all seinen Objekten und Zweigen.
        /// </summary>
        public void Clear()
        {
            objectList.Clear();

            foreach(Quadtree child in quadtreeChildren)
                child.Clear();
            quadtreeChildren = null;
        }

        /// <summary>
        /// Teilt diesen Quadtree in 4 gleichverteilte Kinder auf und initialisiert diese.
        /// </summary>
        private void split()
        {
            int childWidth = boundingRectangle.Width / 2;
            int childHeight = boundingRectangle.Height / 2;

            // Rechtsoben
            quadtreeChildren[0] = new Quadtree(level + 1, new Rectangle(boundingRectangle.X + childWidth, boundingRectangle.Y, childWidth, childHeight));
            // Linksoben
            quadtreeChildren[1] = new Quadtree(level + 1, new Rectangle(boundingRectangle.X, boundingRectangle.Y, childWidth, childHeight));
            // Linkunten
            quadtreeChildren[2] = new Quadtree(level + 1, new Rectangle(boundingRectangle.X, boundingRectangle.Y + childHeight, childWidth, childHeight));
            // Rechtsunten
            quadtreeChildren[3] = new Quadtree(level + 1, new Rectangle(boundingRectangle.X + childWidth, boundingRectangle.Y + childHeight, childWidth, childHeight));
        }

        /// <summary>
        /// Entscheidet zu welchem der Kinder ein Kollisionobjekt gehört.
        /// </summary>
        /// <param name="objectShape">die Form eines Objektes</param>
        /// <returns>gibt das Kind zurück, in dem das Objekt vollends hineinpasst. Andernfalls -1</returns>
        private int getIndex(Shape objectShape)
        {
            int childIndex = -1;
            int midX = boundingRectangle.X + (boundingRectangle.Width / 2);
            int midY = boundingRectangle.Y + (boundingRectangle.Height / 2);

            bool topQuadrant = objectShape.LowermostSide < midY;
            bool bottomQuadrant = objectShape.UppermostSide > midY;
            
            if(objectShape.LeftmostSide > midX)
            {
                // Rechtsoben
                if(topQuadrant)
                    childIndex = 0;
                // Rechtsunten
                else if(bottomQuadrant)
                    childIndex = 3;
            } 
            else if(objectShape.RightmostSide < midX)
            {
                // Linksoben
                if(topQuadrant)
                    childIndex = 1;
                // Linksunten
                else if(bottomQuadrant)
                    childIndex = 2;
            }

            return childIndex;
        }

        /// <summary>
        /// Fügt ein Objekt dem Quadtree hinzu. Falls die Kapazität dadurch erschöpft wird, wird der Quadtree geteilt und die Objekte ihren
        /// zugehörigen Zweigen hinzugefügt.
        /// </summary>
        /// <param name="entity">das Object als Entity</param>
        public void Insert(Entity entity)
        {
            Insert(new Tuple<Body, Shape>(entity.Body, entity.Shape));
        }

        /// <summary>
        /// Fügt ein Objekt dem Quadtree hinzu. Falls die Kapazität dadurch erschöpft wird, wird der Quadtree geteilt und die Objekte ihren
        /// zugehörigen Zweigen hinzugefügt.
        /// </summary>
        /// <param name="tile">das Object als Tile</param>
        public void Insert(Tile tile)
        {
            Insert(new Tuple<Body, Shape>(tile.Body, tile.Shape));
        }

        /// <summary>
        /// Fügt ein Objekt dem Quadtree hinzu. Falls die Kapazität dadurch erschöpft wird, wird der Quadtree geteilt und die Objekte ihren
        /// zugehörigen Zweigen hinzugefügt.
        /// </summary>
        /// <param name="newObject">das neue Object als Körper und Form</param>
        public void Insert(Tuple<Body, Shape> newObject)
        {
            // Falls dieser Quadtree schon Kinder hat, wird probiert das neue Object dort hinein zu verschieben.
            if(quadtreeChildren != null)
            {
                int childIndex = getIndex(newObject.Item2);
                if(childIndex != -1)
                {
                    quadtreeChildren[childIndex].Insert(newObject);
                    return;
                }
            }

            objectList.Add(newObject);
            // Falls der Quadtree zuviele Objekte hätte, wird dieser Quadtree aufgeteilt.
            if(objectList.Count > maxObjects && level < maxLevels)
                if(quadtreeChildren == null)
                    split();

            int c = 0;
            while(c < objectList.Count)
            {
                // Falls es ein Kind gibt, in dem das Objekt vollständig reinpasst, wird
                // das Objekt dahin verschoben und aus dieser Ebene gelöscht.
                int childIndex = getIndex(objectList[c].Item2);
                if(childIndex != -1)
                {
                    Tuple<Body, Shape> oldObject = objectList[c];
                    quadtreeChildren[childIndex].Insert(oldObject);
                    objectList.Remove(oldObject);
                }
                else
                    c++;
            }
        }

        /// <summary>
        /// Gibt alle Objekte zurück, welche mit dem Objekt, dessen ID übergeben wurde, kollidieren könnten.
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        public List<Tuple<Body, Shape>> GetObjectCollisionList(Shape shape)
        {
            // Füge alle Objekte dieser Ebene hinzu.
            List<Tuple<Body, Shape>> outputList = new List<Tuple<Body, Shape>>();
            outputList.AddRange(objectList);

            int childIndex = getIndex(shape);
            // Falls das Objekt auf einer unteren Ebene liegt, füge auch diese Objekte zur Liste hinzu.
            if(childIndex != -1 && quadtreeChildren != null)
                outputList.AddRange(quadtreeChildren[childIndex].GetObjectCollisionList(shape));

            return objectList;
        }
    }
}
