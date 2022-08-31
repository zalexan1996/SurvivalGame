using Z.Engine.Source.World;
using Microsoft.Xna.Framework;

namespace Z.SurvivalGame.Worlds
{
    internal class TestWorld : World
    {
        public TestWorld(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // Add our character to the world.
            Actors.Add(new LinkPlayer(Game, this));


            base.Initialize();
        }
    }
}
