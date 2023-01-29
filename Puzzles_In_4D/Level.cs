using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles_In_4D
{
    class Level
    {
        List<Object> Objects;

        public Level(List<Object> objects)
        {
            Objects = objects;
            List<Cube> Cubes = new List<Cube>();
            foreach (Object Object in Objects)
            {
                Polyomino temp;
                if (Object.GetType() == typeof(Polyomino))
                {
                    temp = (Polyomino)Object;
                    Cubes.AddRange(temp.Cubes);
                }
            }
            Objects.AddRange(Cubes);
        }

        public void Update()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].GetType() == typeof(Player))
                {
                    Player temp = (Player)Objects[i];
                    temp.Update(Objects);
                    Objects[i] = temp;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int Player_W_Position)
        {
            bool Non_Cube_Drawn = false;
            foreach (Object Object in Sorted_List_To_Draw(Objects, Player_W_Position))
            {
                if (Object.GetType() != typeof(Polyomino))
                {
                    if (Object.Position.W == Player_W_Position)
                    {
                        if (Object.GetType() == typeof(Cube))
                        {
                            if (Non_Cube_Drawn)
                            {
                                Cube temp = (Cube)Object;
                                Cube Transparent_Cube = new Cube(temp.Sprite, temp.Type, temp.Position);
                                Transparent_Cube.Colour *= 0.75f;
                                Transparent_Cube.Draw(spriteBatch);
                            }
                            else
                            {
                                Object.Draw(spriteBatch);
                            }
                        }
                        else
                        {
                            Non_Cube_Drawn = true;
                            Object.Draw(spriteBatch);
                        }
                    }
                }
            }
        }

        public List<Object> Sorted_List_To_Draw(List<Object> Objects, int W)
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < Objects.Count - 1; i++)
                {
                    if (Objects[i].Position.W == W && Objects[i + 1].Position.W == W)
                    {
                        if (Objects[i].Position.Z > Objects[i + 1].Position.Z)
                        {
                            var temp = Objects[i];
                            Objects[i] = Objects[i + 1];
                            Objects[i + 1] = temp;
                            sorted = false;
                        }
                        else if (Objects[i].Position.Z == Objects[i + 1].Position.Z)
                        {
                            if (Objects[i].Position.Y < Objects[i + 1].Position.Y)
                            {
                                var temp = Objects[i];
                                Objects[i] = Objects[i + 1];
                                Objects[i + 1] = temp;
                                sorted = false;
                            }
                            else if (Objects[i].Position.Y == Objects[i + 1].Position.Y)
                            {
                                if (Objects[i].Position.X < Objects[i + 1].Position.X)
                                {
                                    var temp = Objects[i];
                                    Objects[i] = Objects[i + 1];
                                    Objects[i + 1] = temp;
                                    sorted = false;
                                }
                            }
                        }
                    }
                    else if (Objects[i].Position.W > Objects[i + 1].Position.W)
                    {
                        var temp = Objects[i];
                        Objects[i] = Objects[i + 1];
                        Objects[i + 1] = temp;
                        sorted = false;
                    }
                }
            }
            return Objects;
        }
    }
}
