using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect effect;
        VertexPositionTexture[] vertexBuffer;
        VertexPositionTexture[] bigBox;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            vertexBuffer = new VertexPositionTexture[8];
            bigBox = new VertexPositionTexture[8];

            //front
            vertexBuffer[0].Position = new Vector3(50, 50, 0);  //v0
            vertexBuffer[1].Position = new Vector3(60, 50, 0);  //v1
            vertexBuffer[2].Position = new Vector3(50, 60, 0);  //v2

            vertexBuffer[3].Position = vertexBuffer[1].Position;
            vertexBuffer[4].Position = new Vector3(60, 60, 0);  //v3
            vertexBuffer[5].Position = vertexBuffer[2].Position;

            //back
            vertexBuffer[6].Position = new Vector3(60, 50, 10); //v5
            vertexBuffer[7].Position = new Vector3(50, 50, 10); //v4
            vertexBuffer[8].Position = new Vector3(50, 60, 10); //v6

            vertexBuffer[9].Position = vertexBuffer[6].Position;
            vertexBuffer[10].Position = vertexBuffer[8].Position;
            vertexBuffer[11].Position = new Vector3(60, 60, 10);//v7
            
            //right
            vertexBuffer[12].Position = vertexBuffer[1].Position;
            vertexBuffer[13].Position = vertexBuffer[6].Position;
            vertexBuffer[14].Position = vertexBuffer[4].Position;

            vertexBuffer[15].Position = vertexBuffer[6].Position;
            vertexBuffer[16].Position = vertexBuffer[11].Position;
            vertexBuffer[17].Position = vertexBuffer[4].Position;

            //left
            vertexBuffer[18].Position = vertexBuffer[7].Position;
            vertexBuffer[19].Position = vertexBuffer[0].Position;
            vertexBuffer[20].Position = vertexBuffer[8].Position;

            vertexBuffer[21].Position = vertexBuffer[0].Position;
            vertexBuffer[22].Position = vertexBuffer[2].Position;
            vertexBuffer[23].Position = vertexBuffer[8].Position;

            //top





            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void DrawCube()
        {
            var cameraPos = new Vector3(25, 25, -10);
            var cameraLookAtVector = Vector3.Zero;
            var cameraUpVector = Vector3.UnitZ;

            effect.View = Matrix.CreateLookAt(cameraPos, cameraLookAtVector, cameraUpVector);

            float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;

            float fieldOfVeiw = MathHelper.PiOver4;

            float nearClipPlane = 1f;
            float farClipPlane = 100f;

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfVeiw, aspectRatio, nearClipPlane, farClipPlane);

            foreach(var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList ,vertexBuffer, 0, 1);
                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexBuffer, 1, 1);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
