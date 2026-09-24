using System;
using System.Collections.Generic;
using System.Text;

namespace TTC.RubiksCube.Domain
{
    public class Cube
    {
        public Color[,] Up => U;
        public Color[,] Down => D;
        public Color[,] Left => L;
        public Color[,] Right => R;
        public Color[,] Front => F;
        public Color[,] Back => B;
        private readonly Color[,] U = new Color[3, 3];
        private readonly Color[,] D = new Color[3, 3];
        private readonly Color[,] L = new Color[3, 3];
        private readonly Color[,] R = new Color[3, 3];
        private readonly Color[,] F = new Color[3, 3];
        private readonly Color[,] B = new Color[3, 3];
        private readonly Dictionary<Face, Action> _rotations;
        private readonly Stack<Move> _history = new();
        public Cube()
        {
            InitSolved();

            _rotations = new Dictionary<Face, Action>
            {
                { Face.Front, RotateFrontClockwise },
                { Face.Back, RotateBackClockwise },
                { Face.Left, RotateLeftClockwise },
                { Face.Right, RotateRightClockwise },
                { Face.Up, RotateUpClockwise },
                { Face.Down, RotateDownClockwise }
            };
        }

        private void InitSolved()
        {
            Fill(U, Color.White);   // Up
            Fill(D, Color.Yellow);  // Down
            Fill(L, Color.Orange);  // Left
            Fill(R, Color.Red);     // Right
            Fill(F, Color.Green);   // Front
            Fill(B, Color.Blue);    // Back
        }
        private void Fill(Color[,] face, Color color)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    face[row, column] = color;
                }
            }
        }

        public void Rotate(Face face, RotationDirection direction)
        {
            var turns =
            direction == RotationDirection.Clockwise
            ? 1
            : 3;

            for (var i = 0; i < turns; i++)
            {
                RotateClockwise(face);
            }
        }

        public void Rotate(Move move)
        {
            Rotate(move.Face, move.Direction);

            //history for undo
            _history.Push(move);
        }

        private void RotateClockwise(Face face)
        {
            _rotations[face]();
        }

        private void RotateFaceClockwise(Color[,] face)
        {
            var temp = new Color[3, 3];

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    temp[column, 2 - row] = face[row, column];
                }
            }

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    face[row, column] = temp[row, column];
                }
            }
        }

        // FRONT (F)
        private void RotateFrontClockwise()
        {
            RotateFaceClockwise(F);

            Color[] temp = new Color[3];
            // temp = U bottom row
            temp[0] = U[2, 0]; temp[1] = U[2, 1]; temp[2] = U[2, 2];

            // U bottom row <- L right column (reversed)
            U[2, 0] = L[2, 2];
            U[2, 1] = L[1, 2];
            U[2, 2] = L[0, 2];

            // L right column <- D top row
            L[0, 2] = D[0, 0];
            L[1, 2] = D[0, 1];
            L[2, 2] = D[0, 2];

            // D top row <- R left column (reversed)
            D[0, 0] = R[2, 0];
            D[0, 1] = R[1, 0];
            D[0, 2] = R[0, 0];

            // R left column <- temp (U bottom row)
            R[0, 0] = temp[0];
            R[1, 0] = temp[1];
            R[2, 0] = temp[2];
        }

        // BACK (B)
        private void RotateBackClockwise()
        {
            RotateFaceClockwise(B);

            Color[] temp = new Color[3];
            // temp = U top row
            temp[0] = U[0, 0]; temp[1] = U[0, 1]; temp[2] = U[0, 2];

            // U top row <- R right column
            U[0, 0] = R[0, 2];
            U[0, 1] = R[1, 2];
            U[0, 2] = R[2, 2];

            // R right column <- D bottom row (reversed)
            R[0, 2] = D[2, 2];
            R[1, 2] = D[2, 1];
            R[2, 2] = D[2, 0];

            // D bottom row <- L left column
            D[2, 0] = L[0, 0];
            D[2, 1] = L[1, 0];
            D[2, 2] = L[2, 0];

            // L left column <- temp (U top row reversed)
            L[0, 0] = temp[2];
            L[1, 0] = temp[1];
            L[2, 0] = temp[0];
        }

        // LEFT (L)
        private void RotateLeftClockwise()
        {
            RotateFaceClockwise(L);

            Color[] temp = new Color[3];
            // temp = U left column
            temp[0] = U[0, 0]; temp[1] = U[1, 0]; temp[2] = U[2, 0];

            // U left column <- B right column
            U[0, 0] = B[2, 2];
            U[1, 0] = B[1, 2];
            U[2, 0] = B[0, 2];

            // B right column <- D left column
            B[0, 2] = D[2, 0];
            B[1, 2] = D[1, 0];
            B[2, 2] = D[0, 0];

            // D left column <- F left column
            D[0, 0] = F[0, 0];
            D[1, 0] = F[1, 0];
            D[2, 0] = F[2, 0];

            // F left column <- temp (U left column)
            F[0, 0] = temp[0];
            F[1, 0] = temp[1];
            F[2, 0] = temp[2];
        }

        // RIGHT (R)
        private void RotateRightClockwise()
        {
            RotateFaceClockwise(R);

            Color[] temp = new Color[3];
            // temp = U right column
            temp[0] = U[0, 2]; temp[1] = U[1, 2]; temp[2] = U[2, 2];

            // U right column <- F right column
            U[0, 2] = F[0, 2];
            U[1, 2] = F[1, 2];
            U[2, 2] = F[2, 2];

            // F right column <- D right column
            F[0, 2] = D[0, 2];
            F[1, 2] = D[1, 2];
            F[2, 2] = D[2, 2];

            // D right column <- B left column (reversed)
            D[0, 2] = B[2, 0];
            D[1, 2] = B[1, 0];
            D[2, 2] = B[0, 0];

            // B left column <- temp (U right column reversed)
            B[0, 0] = temp[2];
            B[1, 0] = temp[1];
            B[2, 0] = temp[0];
        }

        // UP (U)
        private void RotateUpClockwise()
        {
            RotateFaceClockwise(U);

            Color[] temp = new Color[3];
            // temp = F top row
            temp[0] = F[0, 0]; temp[1] = F[0, 1]; temp[2] = F[0, 2];

            // F top row <- R top row
            F[0, 0] = R[0, 0];
            F[0, 1] = R[0, 1];
            F[0, 2] = R[0, 2];

            // R top row <- B top row
            R[0, 0] = B[0, 0];
            R[0, 1] = B[0, 1];
            R[0, 2] = B[0, 2];

            // B top row <- L top row
            B[0, 0] = L[0, 0];
            B[0, 1] = L[0, 1];
            B[0, 2] = L[0, 2];

            // L top row <- temp (F top row)
            L[0, 0] = temp[0];
            L[0, 1] = temp[1];
            L[0, 2] = temp[2];
        }

        // DOWN (D)
        private void RotateDownClockwise()
        {
            RotateFaceClockwise(D);

            Color[] temp = new Color[3];
            // temp = F bottom row
            temp[0] = F[2, 0]; temp[1] = F[2, 1]; temp[2] = F[2, 2];

            // F bottom row <- L bottom row
            F[2, 0] = L[2, 0];
            F[2, 1] = L[2, 1];
            F[2, 2] = L[2, 2];

            // L bottom row <- B bottom row
            L[2, 0] = B[2, 0];
            L[2, 1] = B[2, 1];
            L[2, 2] = B[2, 2];

            // B bottom row <- R bottom row
            B[2, 0] = R[2, 0];
            B[2, 1] = R[2, 1];
            B[2, 2] = R[2, 2];

            // R bottom row <- temp (F bottom row)
            R[2, 0] = temp[0];
            R[2, 1] = temp[1];
            R[2, 2] = temp[2];
        }

        private static Move Reverse(Move move)
        {
            return new Move(move.Face, move.Direction == RotationDirection.Clockwise
            ? RotationDirection.CounterClockwise
            : RotationDirection.Clockwise);
        }

        public void Undo()
        {
            if (!_history.Any())
            {
                return;
            }

            var move = _history.Pop();

            var reverseMove = Reverse(move);

            Rotate(reverseMove.Face, reverseMove.Direction);
        }

        // Helpers for executing the challenge automatically
        private static readonly Move[] TtcMoves =
                                                [
                                                    new(Face.Front, RotationDirection.Clockwise),
                                                    new(Face.Right, RotationDirection.CounterClockwise),
                                                    new(Face.Up, RotationDirection.Clockwise),
                                                    new(Face.Back, RotationDirection.CounterClockwise),
                                                    new(Face.Left, RotationDirection.Clockwise),
                                                    new(Face.Down, RotationDirection.CounterClockwise)
                                                ];

        public void ExecuteTtcChallenge(Move[] moves)
        {
            Execute(moves);
        }
        public void Execute(IEnumerable<Move> moves)
        {
            foreach (var move in moves)
            {
                Rotate(move);
            }
        }

        public void ExecuteTtcChallenge()
        {
            Rotate(new Move(Face.Front, RotationDirection.Clockwise));
            Rotate(new Move(Face.Right, RotationDirection.CounterClockwise));
            Rotate(new Move(Face.Up, RotationDirection.Clockwise));
            Rotate(new Move(Face.Back, RotationDirection.CounterClockwise));
            Rotate(new Move(Face.Left, RotationDirection.Clockwise));
            Rotate(new Move(Face.Down, RotationDirection.CounterClockwise));
        }
    }
}
