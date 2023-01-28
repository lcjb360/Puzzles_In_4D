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
        public Vector4 Movement_Control(KeyboardState Keyboard_State, Vector4 Position)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                D_Pressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.D) && D_Pressed)
            {
                D_Pressed = false;
                if (Position.X < 15)
                {
                    Position.X += 1;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                A_Pressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.A) && A_Pressed)
            {
                A_Pressed = false;
                if (Position.X > 0)
                {
                    Position.X -= 1;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                W_Pressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.W) && W_Pressed)
            {
                W_Pressed = false;
                if (Position.Y < 15)
                {
                    Position.Y += 1;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                S_Pressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.S) && S_Pressed)
            {
                S_Pressed = false;
                if (Position.Y > 0)
                {
                    Position.Y -= 1;
                }
            }

            return Position;
        }

        public void Update()
        {
            Position = Movement_Control(Keyboard.GetState(), Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, new Vector3(Position.X + 0.7f, Position.Y, Position.Z + 0.1f), Colour);
        }
    }
}
