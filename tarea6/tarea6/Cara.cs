using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace tarea6
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

        public void Dibujar()
        {
            GL.Begin(PrimitiveType.Quads);  // Asumiendo que todas las caras son cuadriláteros
            foreach (var vertice in vertices)
            {
                GL.Vertex3(vertice);
            }
            GL.End();
        }
        public void DibujarSinBeginEnd()
        {
            foreach (var vertice in vertices)
            {
                GL.Vertex3(vertice);
            }
        }
    }
}
