using Z.Engine.Source;
using Z.SurvivalGame.Worlds;

namespace Z.SurvivalGame
{
    public class SurvivalGame : ZGame
    {
        public SurvivalGame()
        {
            ActiveWorld = new TestWorld(this);
        }
    }
}
