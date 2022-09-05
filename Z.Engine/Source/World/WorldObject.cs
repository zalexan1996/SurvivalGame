using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Engine.Source.Interfaces;

namespace Z.Engine.Source.World
{
    /// <summary>
    /// A drawable object that has a location.
    /// </summary>
    public class WorldObject : DrawableGameComponent, IPosition
    {
        public WorldObject(Game game, IPosition? parent) : base(game)
        {
            Parent = parent;
        }


        public Vector2 WorldPosition
        {
            get
            {
                return RelativePosition + (Parent?.WorldPosition ?? Vector2.Zero);
            }
        }

        /// <summary>
        /// The world position of this object. Calculated by accumulating locations through the parent chain.
        /// </summary>
        public Vector2 RelativePosition
        {
            get
            {
                return _relativePosition;
            }
            set
            {
                _relativePosition = value;
            }
        } private Vector2 _relativePosition = new Vector2();

        public IPosition? Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        } private IPosition? _parent;

    }
}
