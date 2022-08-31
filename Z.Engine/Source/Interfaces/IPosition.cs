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
        /// The location of the implementor relative to its parent.
        /// </summary>
        public Vector2 RelativePosition { get; set; }
        public IPosition? Parent { get; set; }
    }
}
