using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Puzzles_In_4D
{
    abstract class Object
    {
        public Vector4 Position;
        public Sprite Sprite;
        public Color Colour;

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
