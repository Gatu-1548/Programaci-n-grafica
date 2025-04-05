using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Tarea3._1
{
    internal class Objeto
    {
        public float x, y, z; // Coordenadas del centro individuales
        private List<Vector3> vertices;

        public Objeto(Vector3 centro, List<Vector3> vertices)
        {
            this.x = centro.X;
            this.y = centro.Y;
            this.z = centro.Z;
            this.vertices = vertices;
        }

        public void Dibujar()
        {
            GL.Color3(0.9f, 0.6f, 0.2f); // Naranja claro
            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < vertices.Count; i += 4)
            {
                GL.Vertex3(vertices[i].X + x, vertices[i].Y + y, vertices[i].Z + z);
                GL.Vertex3(vertices[i + 1].X + x, vertices[i + 1].Y + y, vertices[i + 1].Z + z);
                GL.Vertex3(vertices[i + 2].X + x, vertices[i + 2].Y + y, vertices[i + 2].Z + z);
                GL.Vertex3(vertices[i + 3].X + x, vertices[i + 3].Y + y, vertices[i + 3].Z + z);
            }

            GL.End();
        }
    }
}