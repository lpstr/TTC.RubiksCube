using System;
using System.Collections.Generic;
using System.Text;
using TTC.RubiksCube.Domain;

namespace TTC.RubiksCube.Contracts
{
    public interface ICubeRendering
    {
        void Print(Cube cube);
    }
}
