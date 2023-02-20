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
    class Level
    {
        List<Object> Objects;
        bool Complete;
        bool Unlocked;
        int Current_W;

        public Level(List<Object> objects, bool complete, bool unlocked)
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
            Complete = complete;
            Unlocked = unlocked;
        }

        bool Q_Pressed = false;
        bool E_Pressed = false;
        public bool Update()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].GetType() == typeof(Player))
                {
                    Player temp = (Player)Objects[i];
                    temp.Update(Objects);
                    if (temp.Position.X == 100)
                    {
                        Complete = true;
                    }
                    Current_W = (int)temp.Position.W;
                    Objects[i] = temp;
                }
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Q_Pressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Q) && Q_Pressed)
            {
                Rotate(-1);
                Q_Pressed = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                E_Pressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.E) && E_Pressed)
            {
                Rotate(1);
                E_Pressed = false;
            }
            return Complete;
        }

        public void Rotate(int direction)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].GetType() != typeof(Polyomino))
                {
                    if (direction == -1)
                    {
                        Objects[i].Position = new Vector4(15f - Objects[i].Position.Y, Objects[i].Position.X, Objects[i].Position.Z, Objects[i].Position.W);
                    }
                    else
                    {
                        Objects[i].Position = new Vector4(Objects[i].Position.Y, 15f - Objects[i].Position.X, Objects[i].Position.Z, Objects[i].Position.W);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bool Non_Cube_Drawn = false;
            foreach (Object Object in Sorted_List_To_Draw(Objects, Current_W))
            {
                if (Object.GetType() != typeof(Polyomino))
                {
                    if (Object.Position.W == Current_W)
                    {
                        if (Object.GetType() == typeof(Cube))
                        {
                            if (Non_Cube_Drawn)
                            {
                                Cube temp = (Cube)Object;
                                Cube Transparent_Cube = new Cube(temp.Sprite, temp.Type, temp.Position);
                                Transparent_Cube.Colour = Object.Colour * 0.75f;
                                Transparent_Cube.Draw(spriteBatch, Transparent_Cube.Colour);
                            }
                            else
                            {
                                Cube temp = (Cube)Object;
                                Cube Cube = new Cube(temp.Sprite, temp.Type, temp.Position);
                                Cube.Draw(spriteBatch, Object.Colour);
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
