using System;
using Z.Engine.Source;

namespace Z.SurvivalGame
{
    public class Startup : IDisposable
    {

        public static void Main(string[] args)
        {
            using (Startup startup = new Startup())
            {
                //TODO: Initialize 3rd party libraries
                //TODO: Intro credits

                // Run the game
                startup.RunGame();

                //TODO: Unload extra stuff.
            }
        }

        protected readonly SurvivalGame? _zGame;



        protected Startup()
        {
            _zGame = new SurvivalGame();
        }



        protected void RunGame()
        {
            _zGame?.Run();
        }


        public void Dispose()
        {
            _zGame?.Dispose();
        }
    }
}