using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Engine.Source.Interfaces
{
    public interface IPosition
    {
        /// <summary>
        /// The world position of an object. Calculated by accumulating the 
        /// object's RelativePosition and its parent's WorldPosition.
        /// </summary>
        public Vector2 WorldPosition { get; }

        /// <summary>
        /// The location of the implementor relative to its parent.
        /// </summary>
        /// </summary>
        public Vector2 RelativePosition { get; set; }

        /// <summary>
        /// The parent of this object, if there is one. Each object SHOULD at least have a World for a parent.
        /// </summary>
        public IPosition? Parent { get; set; }
    }
}
