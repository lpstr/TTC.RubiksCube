using System;
using System.Collections.Generic;
using System.Text;
using TTC.RubiksCube.Domain;

namespace TTC.RubiksCube.Factories
{
    public static class MoveFactory
    {
        public static Move? FromKey(char key)
        {
            return key switch
            {
                'F' => new Move(Face.Front, RotationDirection.Clockwise),
                'f' => new Move(Face.Front, RotationDirection.CounterClockwise),

                'R' => new Move(Face.Right, RotationDirection.Clockwise),
                'r' => new Move(Face.Right, RotationDirection.CounterClockwise),

                'U' => new Move(Face.Up, RotationDirection.Clockwise),
                'u' => new Move(Face.Up, RotationDirection.CounterClockwise),

                'B' => new Move(Face.Back, RotationDirection.Clockwise),
                'b' => new Move(Face.Back, RotationDirection.CounterClockwise),

                'L' => new Move(Face.Left, RotationDirection.Clockwise),
                'l' => new Move(Face.Left, RotationDirection.CounterClockwise),

                'D' => new Move(Face.Down, RotationDirection.Clockwise),
                'd' => new Move(Face.Down, RotationDirection.CounterClockwise),

                _ => null
            };
        }
    }
}
