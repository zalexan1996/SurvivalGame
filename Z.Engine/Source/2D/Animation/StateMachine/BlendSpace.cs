using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Engine.Source._2D.Animation.StateMachine;

namespace Z.Engine.Source._2D.Animation
{
    /// <summary>
    /// A blend space is a data structure indexed by cartesian plane coordinates.
    /// Very helpful for animation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BlendSpace<T> where T : class
    {
        /// <summary>
        /// An array to store the blend space items.
        /// </summary>
        private readonly List<BlendSpaceItem> items = new();

        /// <summary>
        /// A wrapper for an object within the blend space.
        /// </summary>
        protected class BlendSpaceItem
        {
            public float X { get; set; }
            public float Y { get; set; }

            public T Value { get; set; }

            public BlendSpaceItem(float x, float y, T value)
            {
                X = x;
                Y = y;
                Value = value;
            }
            public BlendSpaceItem(Vector2 input, T value)
            {
                X = input.X;
                Y = input.Y;
                Value = value;
            }
        }


        /// <summary>
        /// Gets the item *closest* to the provided coordinates.
        /// </summary>
        /// <returns></returns>
        public T? Get(float x, float y)
        {
            return items.OrderBy(i => (x - i.X) * (x - i.X) + (y - i.Y) * (y - i.Y)).First().Value;
        }

        /// <summary>
        /// Gets the item *closest* to the provided vector.
        /// </summary>
        /// <param name="input">Point in cartesian coordinate space to look for our item.</param>
        /// <returns></returns>
        public T? Get(Vector2 input)
        {
            return Get(input.X, input.Y);
        }


        /// <summary>
        /// Adds an item to the blendspace.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="item"></param>
        public void Add(float x, float y, T item)
        {
            items.Add(new(x, y, item));
        }

        /// <summary>
        /// Adds an item to the blendspace.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="item"></param>
        public void Add(Vector2 input, T item)
        {
            items.Add(new(input, item));
        }
    }
}
