using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Engine.Source._2D.Animation.StateMachine;

namespace Z.Engine.Source._2D.Animation
{
    public class BlendSpace<T> where T : class
    {
        public BlendSpace()
        {

        }

        /// <summary>
        /// A wrapper for an object within the blend space.
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected class BlendSpaceItem<T> where T : class
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
        }


        /// <summary>
        /// Gets the item closest to the provided coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public T? Get(float x, float y)
        {
            return items.OrderBy(i => (x - i.X) * (x - i.X) + (y - i.Y) * (y - i.Y)).First().Value;
        }


        public void Add(float x, float y, T item)
        {
            items.Add(new(x, y, item));
        }

        /// <summary>
        /// An array to store the blend space items.
        /// </summary>
        protected List<BlendSpaceItem<T>> items { get; set; } = new();
    }
}
