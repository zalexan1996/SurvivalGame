using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Threading.Tasks;

namespace Z.Engine.Source.Core
{
    public class Globals
    {
        public SpriteBatch? SpriteBatch { get; set; }
        public ContentManager? ContentManager { get; set; }
        public GraphicsDevice? GraphicsDevice { get; set; }
        public GraphicsDeviceManager? GraphicsDeviceManager { get; set; }

    }
}
