using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Tarea4._1
{
    internal class Cara
    {
        private List<Vector3> vertices = new();

        public Cara(List<Vector3> vertices)
        {
            this.vertices = vertices;
        }

        public List<Vector3> GetVertices() => vertices;

        public void SetVertices(List<Vector3> nuevosVertices) => vertices = nuevosVertices;
    }
}
