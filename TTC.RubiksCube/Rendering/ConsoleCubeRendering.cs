using System;
using System.Collections.Generic;
using System.Text;
using TTC.RubiksCube.Contracts;
using TTC.RubiksCube.Domain;

namespace TTC.RubiksCube.Rendering
{
    public sealed class ConsoleCubeRenderer : ICubeRendering
    {
        public void Print(Cube cube)
        {
            Console.Clear();
            
            Console.WriteLine("Rubik's Cube (TTC / ruwix orientation)");
            Console.WriteLine("Moves: F f R r U u B b L l D d (X to exit)");
            Console.WriteLine("Z = Undo");
            Console.WriteLine("T = TTC Challenge");
            Console.WriteLine("X = Exit");
            Console.WriteLine();

            PrintFace("UP", cube.Up);

            PrintMiddleRow(
            cube.Left,
            cube.Front,
            cube.Right,
            cube.Back);

            PrintFace("DOWN", cube.Down);
        }

        private string Colorize(Color c, string text)
        {
            string color = c switch
            {
                Color.White => "\u001b[37m",
                Color.Yellow => "\u001b[33m",
                Color.Orange => "\u001b[38;5;208m",
                Color.Red => "\u001b[31m",
                Color.Green => "\u001b[32m",
                Color.Blue => "\u001b[34m",
                _ => "\u001b[0m"
            };

            return $"{color}{text}\u001b[0m";
        }

        private void PrintFace(
        string name,
        Color[,] face)
        {
            Console.WriteLine($" {name}");

            for (int r = 0; r < 3; r++)
            {
                Console.Write(" ");

                for (int c = 0; c < 3; c++)
                {
                    Console.Write(
                    Colorize(face[r, c], "██") + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private void PrintMiddleRow(
        Color[,] left,
        Color[,] front,
        Color[,] right,
        Color[,] back)
        {
            for (int r = 0; r < 3; r++)
            {
                PrintRow(left, r);

                Console.Write(" ");

                PrintRow(front, r);

                Console.Write(" ");

                PrintRow(right, r);

                Console.Write(" ");

                PrintRow(back, r);

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private void PrintRow(
        Color[,] face,
        int row)
        {
            for (int c = 0; c < 3; c++)
            {
                Console.Write(
                Colorize(face[row, c], "██") + " ");
            }
        }
    }
}
