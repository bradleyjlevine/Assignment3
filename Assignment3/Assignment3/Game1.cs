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
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

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

            VertexPositionColor[] vertices = new VertexPositionColor[8];

            vertices[0] = new VertexPositionColor(new Vector3(50, 50, 0), Color.White);
            vertices[1] = new VertexPositionColor(new Vector3(60, 50, 0), Color.White);
            vertices[2] = new VertexPositionColor(new Vector3(50, 60, 0), Color.White);
            vertices[3] = new VertexPositionColor(new Vector3(60, 60, 0), Color.White);

            vertices[4] = new VertexPositionColor(new Vector3(50, 50, 10), Color.White);
            vertices[5] = new VertexPositionColor(new Vector3(60, 50, 10), Color.White);
            vertices[6] = new VertexPositionColor(new Vector3(50, 60, 10), Color.White);
            vertices[7] = new VertexPositionColor(new Vector3(60, 60, 10), Color.White);

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 8, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertices);

            short[] indices = new short[36];
            indices[0] = 0; indices[1] = 1; indices[2] = 2;
            indices[3] = 2; indices[4] = 1; indices[5] = 3;


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

                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList ,vertexBuffer, 0, 12);
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

            DrawCube();

            base.Draw(gameTime);
        }
    }
}
