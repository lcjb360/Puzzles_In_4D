using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Puzzles_In_4D
{
    public class Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;
        public int Width;
        public int Height;
        public Vector2 Window_Centre;
        public Sprite(Texture2D texture, Vector2 position, int width, int height, Vector2 window_centre)
        {
            Texture = texture;
            Position = position;
            Width = width;
            Height = height;
            Window_Centre = window_centre;
        }
        //change x and y etc * ratio
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(Texture, position, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), color);
        }

        public void Draw(SpriteBatch spriteBatch, Vector3 position3, Color color)
        {
            Vector2 position2 = Coordinate_Conversion(position3);
            spriteBatch.Draw(Texture, position2, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), color);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int width, int height)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int width, int height, Color color)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle((int)Position.X, (int)Position.Y, Width, Height), color);
        }

        private Vector2 Coordinate_Conversion(Vector3 Position)
        {
            Vector2 output = new Vector2(Window_Centre.X, 2*Window_Centre.Y - 42);
            output.X += 15 * Position.X;
            output.X -= 15 * Position.Y;
            output.Y -= 11 * Position.X;
            output.Y -= 11 * Position.X;
            output.Y -= 20 * Position.Z;
            return output;
        }
    }
}