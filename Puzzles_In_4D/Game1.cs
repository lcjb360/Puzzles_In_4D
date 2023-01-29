using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Puzzles_In_4D
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D SpriteSheet;
        public Rectangle Window;

        //Delete
        Polyomino p;
        Player Player;
        Level Level_1;
        //



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 60;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 120;
            _graphics.ApplyChanges();
            Window = GraphicsDevice.Viewport.Bounds;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet = Content.Load<Texture2D>("Untitled");
            Sprite Cube_Sprite = new Sprite(SpriteSheet, new Vector2(0,0), 62, 85, new Vector2(Window.Center.X, Window.Center.Y));
            Sprite Player_Sprite = new Sprite(SpriteSheet, new Vector2(64, 0), 20, 46, new Vector2(Window.Center.X, Window.Center.Y));
            //delete
            Player = new Player(new Vector4(0, 0, 1, 0), Player_Sprite);
            List<Cube> list = new List<Cube>() { new Cube(Cube_Sprite, "Immovable", new Vector4(1,0,0,0)), new Cube(Cube_Sprite, "Immovable", new Vector4(0, 1, 2, 0)), new Cube(Cube_Sprite, "Immovable", new Vector4(2, 1, 0, 0)), new Cube(Cube_Sprite, "Immovable", new Vector4(0,1,0,0)), new Cube(Cube_Sprite, "Immovable", new Vector4(0, 0, 0, 0)), new Cube(Cube_Sprite, "Immovable", new Vector4(0, 0, 1, 0)) };
            p = new Polyomino(Cube_Sprite, list, Color.White);
            Level_1 = new Level(new List<Object> { p, Player });
            //
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Player.Update();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Orange);
            _spriteBatch.Begin();
            Level_1.Draw(_spriteBatch, 0);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}