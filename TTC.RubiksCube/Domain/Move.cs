using System;
using System.Collections.Generic;
using System.Text;

namespace TTC.RubiksCube.Domain
{
    public sealed record Move(Face Face, RotationDirection Direction);
}
