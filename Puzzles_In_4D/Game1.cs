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

        
        Polyomino P;
        Player Player;
        Level Level_1;

        Polyomino Base;
        



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

        private List<Cube> Generate_Base_Cubes(int W_Height, Sprite Cube_Sprite)
        {
            List<Cube> Base_Cubes = new List<Cube>();
            for (int w = 0; w < W_Height; w++)
            {
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        Base_Cubes.Add(new Cube(Cube_Sprite, "Immovable", new Vector4(x, y, 0, w)));
                    }
                }
            }
            return Base_Cubes;
        }

        private List<Cube> Vectors_To_Cubes(List<Vector4> Vectors, Sprite Cube_Sprite, string Type)
        {
            List<Cube> Cubes = new List<Cube>();
            foreach (Vector4 Vector in Vectors)
            {
                Cubes.Add(new Cube(Cube_Sprite, Type, Vector));
            }
            return Cubes;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet = Content.Load<Texture2D>("Untitled");
            Sprite Cube_Sprite = new Sprite(SpriteSheet, new Vector2(0,0), 62, 85, new Vector2(Window.Center.X, Window.Center.Y));
            Sprite Player_Sprite = new Sprite(SpriteSheet, new Vector2(64, 0), 20, 46, new Vector2(Window.Center.X, Window.Center.Y));


            Player = new Player(new Vector4(5, 5, 1, 0), Player_Sprite);

            //Level_1
            List<Vector4> Immovable_Cubes = new List<Vector4>() { new Vector4(0, 0, 1, 0), new Vector4(0, 0, 2, 0) };
            P = new Polyomino(Vectors_To_Cubes(Immovable_Cubes, Cube_Sprite, "Immovable"), Color.White);

            List<Cube> Base_Cubes = Generate_Base_Cubes(3, Cube_Sprite);
            Base = new Polyomino(Base_Cubes, Color.White);

            Level_1 = new Level(new List<Object> { Base, P, Player });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Level_1.Update();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Orange);
            _spriteBatch.Begin();
            Level_1.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}