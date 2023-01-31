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
    class Polyomino : Object
    {
        public List<Cube> Cubes;

        public Polyomino(List<Cube> cubes)
        {
            Cubes = cubes;
            Colour = Color.White;
            for (int i = 0; i < cubes.Count; i++)
            {
                Cubes[i].Colour = Colour;
            }
        }
        public Polyomino(List<Cube> cubes, Color colour)
        {
            Cubes = cubes;
            Colour = colour;
            for (int i = 0; i < Cubes.Count; i++)
            {
                Cubes[i].Colour = Colour;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Cube Cube in Cubes)
            {
                if (true)//check the correct 4D layer
                {
                    Cube.Draw(spriteBatch, Cube.Colour);
                }
            }
        }
    }
}
