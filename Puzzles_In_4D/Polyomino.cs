using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Puzzles_In_4D
{
    class Polyomino
    {
        public List<Vector4> Box_Positions;
        private Sprite Sprite;
        private Color Colour;

        public Polyomino(Sprite sprite, List<Vector4> box_positions, Color colour)
        {
            Sprite = sprite;
            Box_Positions = box_positions;
            Colour = colour;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Vector4 Position in Box_Positions)
            {
                if (true)//check the correct 4D layer
                {
                    Sprite.Draw(spriteBatch, new Vector3(Position.X, Position.Y, Position.Z), Colour);
                }
            }
        }
    }
}
