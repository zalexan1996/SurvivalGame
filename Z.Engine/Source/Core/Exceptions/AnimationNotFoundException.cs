using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Engine.Source.Core.Exceptions
{
    /// <summary>
    /// This exception is called by classes like the AnimationSequencer when you try to play an animation that doesn't
    /// exist in the directionary.
    /// </summary>
    public class AnimationNotFoundException : Exception
    {
        public string AnimationName { get; set; }

        public AnimationNotFoundException(string animationName) : base($"Could not find an animation for {animationName}")
        {
            AnimationName = animationName;
        }
    }
}
