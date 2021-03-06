﻿/*
 * Bradley Levine
 * Assignemnt3
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Assignment3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D opm;

        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        BasicEffect basicEffect;
        Matrix world = Matrix.CreateTranslation(0, 0, 0);
        Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 50), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 1000f);
        double angle = 0;

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
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            opm = Texture2D.FromStream(GraphicsDevice, new FileStream("one.jpg", FileMode.Open));
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            basicEffect = new BasicEffect(GraphicsDevice);

            // A temporary array, with 12 items in it, because
            // the icosahedron has 12 distinct vertices
            VertexPositionTexture[] vertices = new VertexPositionTexture[8];

            vertices[0] = new VertexPositionTexture(new Vector3(-5f, -5f, 5f), new Vector2(1, 1));
            vertices[1] = new VertexPositionTexture(new Vector3(5f, -5f, 5f), new Vector2(0, 1));
            vertices[2] = new VertexPositionTexture(new Vector3(-5f, 5f, 5f), new Vector2(1, 0));
            vertices[3] = new VertexPositionTexture(new Vector3(5f, 5f, 5f), new Vector2(0, 0));

            vertices[4] = new VertexPositionTexture(new Vector3(-5f, -5f, -5f), new Vector2(1, 1));
            vertices[5] = new VertexPositionTexture(new Vector3(5f, -5f, -5f), new Vector2(0, 1));
            vertices[6] = new VertexPositionTexture(new Vector3(-5f, 5f, -5f), new Vector2(1, 0));
            vertices[7] = new VertexPositionTexture(new Vector3(5f, 5f, -5f), new Vector2(0, 0));

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), 8, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionTexture>(vertices);

            short[] indices = new short[36];
            indices[0] = 0; indices[1] = 1; indices[2] = 2;
            indices[3] = 2; indices[4] = 1; indices[5] = 3;
            indices[6] = 4; indices[7] = 5; indices[8] = 6;
            indices[9] = 6; indices[10] = 5; indices[11] = 7;
            indices[12] = 1; indices[13] = 5; indices[14] = 3;
            indices[15] = 3; indices[16] = 5; indices[17] = 7;
            indices[18] = 4; indices[19] = 0; indices[20] = 6;
            indices[21] = 6; indices[22] = 0; indices[23] = 2;
            indices[24] = 4; indices[25] = 5; indices[26] = 0;
            indices[27] = 0; indices[28] = 5; indices[29] = 1;
            indices[30] = 6; indices[31] = 7; indices[32] = 2;
            indices[33] = 2; indices[34] = 7; indices[35] = 3;

            indexBuffer = new IndexBuffer(GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Matrix temp = Matrix.Identity;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                temp = Matrix.CreateTranslation(-1, 0, 0);
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                temp = Matrix.CreateTranslation(1, 0, 0);
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                temp = Matrix.CreateTranslation(0, 0, -1);
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                temp = Matrix.CreateTranslation(0, 0, 1);

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                temp *= Matrix.CreateFromYawPitchRoll(-1, 0, 0);
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                temp *= Matrix.CreateFromYawPitchRoll(1, 0, 0);
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                temp *= Matrix.CreateFromYawPitchRoll(0, 0, -1);
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                temp *= Matrix.CreateFromYawPitchRoll(0, 0, 1);

            angle += 0.01f;
            view = Matrix.CreateLookAt(
                new Vector3(5 * (float)Math.Sin(angle), -2, 5 * (float)Math.Cos(angle)),
                new Vector3(0, 0, 0),
                Vector3.UnitY);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.VertexColorEnabled = false;
            basicEffect.TextureEnabled = true;
            basicEffect.Texture = opm;

            GraphicsDevice.SetVertexBuffer(vertexBuffer);
            GraphicsDevice.Indices = indexBuffer;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 8, 0, 12);
            }

            base.Draw(gameTime);
        }
    }
}