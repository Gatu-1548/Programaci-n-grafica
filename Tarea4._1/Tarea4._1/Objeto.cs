using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Tarea4._1
{
    internal class Objeto
    {
        public float RotacionY = 0f;
        public Vector3 Centro { get; private set; }
        private List<Parte> partes = new();

        public Objeto(Vector3 centro)
        {
            Centro = centro;
        }

        public void AddParte(Parte parte) => partes.Add(parte);
        public void RemoveParte(Parte parte) => partes.Remove(parte);
        public List<Parte> GetPartes() => partes;

        public void Dibujar()
        {
            GL.PushMatrix(); 

            GL.Translate(Centro.X, Centro.Y, Centro.Z);
            GL.Rotate(RotacionY, 0f, 1f, 0f); 

            GL.Color3(0.9f, 0.6f, 0.2f);
            GL.Begin(PrimitiveType.Quads);

            float escala = 0.03f;

            foreach (var parte in partes)
            {
                foreach (var cara in parte.GetCaras())
                {
                    foreach (var vertice in cara.GetVertices())
                    {
                        Vector3 v = new Vector3(
                            vertice.X * escala,
                            vertice.Y * escala,
                            vertice.Z * escala
                        );
                        GL.Vertex3(v);
                    }
                }
            }

            GL.End();
            GL.PopMatrix(); // Restaura para no afectar a los otros objetos
        }
    }
}