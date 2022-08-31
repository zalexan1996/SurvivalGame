using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Engine.Source._2D.Animation
{
    public class SpriteSheetAnimationProperties
    {
        public SpriteSheetAnimationProperties(string animationName)
        {
            AnimationName = animationName;
        }

        public SpriteSheetAnimationProperties(string animationName, int frameWidth, int frameHeight, int numFrames)
        {
            AnimationName = animationName;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            NumFrames = numFrames;
        }

        public string AnimationName { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public int NumFrames { get; set; }
    }
}
