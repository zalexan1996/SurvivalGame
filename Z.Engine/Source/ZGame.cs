using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Z.Engine.Source._2D.Sprites;
using Z.Engine.Source.Core;
using Z.Engine.Source.Core.Input;

namespace Z.Engine.Source
{

    public class ZGame : Game
    {
        public static Globals Globals
        {
            get;
            protected set;
        } = new Globals();


        /// <summary>
        /// Set this in the base class. This is where the environment assets, characters, sounds, etc are loaded in.
        /// </summary>
        public World.World ? ActiveWorld { get; set; }

        public ZGame()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            // Set the global's references
            Globals.GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Globals.ContentManager = Content;
            Globals.GraphicsDevice = GraphicsDevice;
        }



        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            ActiveWorld?.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
            BetterKeyboard.Inst.Update();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ActiveWorld?.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Globals.SpriteBatch?.Begin(blendState: BlendState.AlphaBlend);

            ActiveWorld?.Draw(gameTime);


            Globals.SpriteBatch?.End();

            base.Draw(gameTime);
        }
    }
}