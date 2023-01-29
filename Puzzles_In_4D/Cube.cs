using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles_In_4D
{
    class Cube : Object
    {
        public string Type;
        public Color Colour = Color.White;
        public Cube(Sprite sprite, string type, Vector4 position)
        {
            Sprite = sprite;
            Type = type;
            Position = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, new Vector3(Position.X, Position.Y, Position.Z), Colour);
        }
    }
}
