using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Engine.Source.Interfaces;

namespace Z.Engine.Source.World
{
    public class WorldObject : DrawableGameComponent, IPosition
    {
        public WorldObject(Game game, IPosition? parent) : base(game)
        {
            Parent = parent;
        }

        /// <summary>
        /// The world position of this object. Calculated by accumulating locations through the parent chain.
        /// </summary>
        public Vector2 WorldPosition
        {
            get
            {
                return RelativePosition + (Parent?.RelativePosition ?? Vector2.Zero);
            }
        }
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
