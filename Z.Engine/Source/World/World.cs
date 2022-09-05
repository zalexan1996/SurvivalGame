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
     /// Represents a collection of actors that exist in the World.
     /// Used for drawing all active objects. TODO: Implement draw order.
     /// </summary>
    public class World : WorldObject
    {
        public World(Game game) : base(game, null)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            Actors.ForEach(a => a.Initialize());
        }

        public override void Update(GameTime gameTime)
        {
            Actors.ForEach(a => a.Update(gameTime));

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            Actors.ForEach(a => a.Draw(gameTime));
            base.Draw(gameTime);
        }

       

        public List<DrawableGameComponent> Actors { get; set; } = new List<DrawableGameComponent>();
    }
}
