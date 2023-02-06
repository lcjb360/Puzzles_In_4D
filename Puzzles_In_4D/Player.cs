using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles_In_4D
{
    class Player : Object
    {
        public Player(Vector4 position, Sprite sprite)
        {
            Colour = Color.White;
            Position = position;
            Sprite = sprite;
        }


        bool D_Pressed = false;
        bool A_Pressed = false;
        bool W_Pressed = false;
        bool S_Pressed = false;
        bool Moving_Polyomino = false;
        Polyomino Polyomino_Moving;
        public Vector4 Movement_Control(Vector4 Position, List<Object> Objects)
        {
            Vector4 Original_Position = Position;

            //4D Movement
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                W_Pressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.W) && W_Pressed)
            {
                Position.W++;
                W_Pressed = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                S_Pressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.S) && S_Pressed)
            {
                Position.W--;
                S_Pressed = false;
            }

            //3D Movement
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                D_Pressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.D) && D_Pressed && (Keyboard.GetState().IsKeyDown(Keys.LeftControl) || Keyboard.GetState().IsKeyDown(Keys.LeftShift)))
            {
                D_Pressed = false;
                if (Position.X < 15)
                {
                    Position.X += 1;
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.D) && D_Pressed && !(Keyboard.GetState().IsKeyDown(Keys.LeftControl) || Keyboard.GetState().IsKeyDown(Keys.LeftShift)))
            {
                D_Pressed = false;
                if (Position.Y > 0)
                {
                    Position.Y -= 1;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                A_Pressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.A) && A_Pressed && !(Keyboard.GetState().IsKeyDown(Keys.LeftControl) || Keyboard.GetState().IsKeyDown(Keys.LeftShift)))
            {
                A_Pressed = false;
                if (Position.X > 0)
                {
                    Position.X -= 1;
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.A) && A_Pressed && (Keyboard.GetState().IsKeyDown(Keys.LeftControl) || Keyboard.GetState().IsKeyDown(Keys.LeftShift)))
            {
                A_Pressed = false;
                if (Position.Y < 15)
                {
                    Position.Y += 1;
                }
            }

            //Grabbing a Polyomino
            if (Keyboard.GetState().IsKeyDown(Keys.F) && !Moving_Polyomino)
            {
                foreach (Object Cube in Objects)
                {
                    if (Cube.GetType() == typeof(Cube))
                    {
                        if (Cube.Position == Position && ((Cube)Cube).Type.Contains("Movable"))
                        {
                            Polyomino_Moving = ((Cube)Cube).Polyomino;
                            Moving_Polyomino = true;
                            Position = Original_Position;
                            return Position;
                        }
                    }
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.F) && Moving_Polyomino)
            {
                Moving_Polyomino = false;
                Polyomino_Moving = null;
            }

            //Interacting with cubes
            bool Falling = true;
            bool Colliding = false;
            bool Jumping = false;
            foreach (Object Object in Objects)
            {
                if (Object.GetType() == typeof(Cube) && Object.Position == Position)
                {
                    if (!Moving_Polyomino)
                    {
                        Colliding = true;
                        Jumping = true;
                    }
                    else if (Polyomino_Moving != ((Cube)Object).Polyomino)
                    {
                        Colliding = true;
                        Jumping = false;
                    }
                }
            }
            foreach (Object Object in Objects)
            {
                if (Object.GetType() == typeof(Cube) && (Object.Position == new Vector4(Position.X, Position.Y, Position.Z + 1, Position.W) || Object.Position == new Vector4(Original_Position.X, Original_Position.Y, Original_Position.Z + 1, Original_Position.W)))
                {
                    Jumping = false;
                }
            }
            if (Colliding && !Jumping)
            {
                Position = Original_Position;
            }
            if (Jumping && !Moving_Polyomino)
            {
                Position.Z += 1;
            }
            if (!Colliding && !Jumping)
            {
                foreach (Object Object in Objects)
                {
                    if (Object.GetType() == typeof(Cube) && Object.Position == new Vector4(Position.X, Position.Y, Position.Z - 1, Position.W))
                    {
                        Falling = false;
                    }
                }
            }
            bool Found_Surface = true;
            if (Falling && !Jumping)
            {
                Found_Surface = false;
                int Z_Fall = 0;
                for (int i = (int)Position.Z - 1; i >= 0; i--)
                {
                    foreach (Object Object in Objects)
                    {
                        if (Object.GetType() == typeof(Cube) && Object.Position == new Vector4(Position.X, Position.Y, i, Position.W))
                        {
                            Found_Surface = true;
                            if (i > Z_Fall)
                            {
                                Z_Fall = i;
                            }
                        }
                    }
                }
                Position.Z = Z_Fall;
            }
            if (!Found_Surface)
            {
                Position = Original_Position;
            }

            bool Cube_Collision = false;
            Vector4 New_Cube_Position;
            if (Moving_Polyomino)
            {
                foreach (Cube Cube in Polyomino_Moving.Cubes)
                {
                    if (Cube.Position == new Vector4(Position.X, Position.Y, Position.Z - 1, Position.W))
                    {
                        Cube_Collision = true;
                        break;
                    }
                    New_Cube_Position = Cube.Position + (Position - Original_Position);
                    foreach (Object Other_Cube in Objects)
                    {
                        if (Other_Cube.GetType() == typeof(Cube))
                        {
                            if (Other_Cube.Position == New_Cube_Position && ((Cube)Other_Cube).Polyomino != Polyomino_Moving)
                            {
                                Cube_Collision = true;
                                break;
                            }
                            if (New_Cube_Position.X < 0 || New_Cube_Position.X > 15 || New_Cube_Position.Y < 0 || New_Cube_Position.Y > 15)
                            {
                                Cube_Collision = true;
                                break;
                            }
                        }
                    }
                }
                if (!Cube_Collision)
                {
                    for (int i = 0; i < Polyomino_Moving.Cubes.Count; i++)
                    {
                        Polyomino_Moving.Cubes[i].Position += Position - Original_Position;
                    }
                }
                else
                {
                    Position = Original_Position;
                }
            }

            foreach (Object Object in Objects)
            {
                if (Object.GetType() == typeof(Cube) && Object.Position == new Vector4(Position.X, Position.Y, Position.Z - 1, Position.W))
                {
                    if (((Cube)Object).Type == "Victory")
                    {
                        Position = new Vector4(100, 100, 100, Position.W);
                    }
                }
            }

            return Position;
        }

        public void Update(List<Object> Objects)
        {
            Position = Movement_Control(Position, Objects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, new Vector3(Position.X + 0.7f, Position.Y, Position.Z - 0.9f), Colour);
        }
    }
}
