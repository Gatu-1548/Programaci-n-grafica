using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace tare5
{
    internal class Objeto
    {
        public Vector3 Posicion { get; set; } = Vector3.Zero;
        public Vector3 Rotacion { get; set; } = Vector3.Zero; // X, Y, Z
        public float Escala { get; set; } = 1f;

        public Vector3 Centro { get; private set; }
        private List<Parte> partes = new();

        public Objeto(Vector3 centro)
        {
            Centro = centro;
            Posicion = centro;
        }

        public void AddParte(Parte parte) => partes.Add(parte);
        public void RemoveParte(Parte parte) => partes.Remove(parte);
        public List<Parte> GetPartes() => partes;

        public void Dibujar()
        {
            GL.PushMatrix();

            GL.Translate(Posicion);

            //Rotación en los tres ejes
            GL.Rotate(Rotacion.Z, 0f, 0f, 1f);
            GL.Rotate(Rotacion.Y, 0f, 1f, 0f);
            GL.Rotate(Rotacion.X, 1f, 0f, 0f);

            // Escalado uniforme
            GL.Scale(Escala, Escala, Escala);

            GL.Color3(1f, 0f, 0f);
            GL.Begin(PrimitiveType.Quads);

            foreach (var parte in partes)
            {
                foreach (var cara in parte.GetCaras())
                {
                    foreach (var vertice in cara.GetVertices())
                    {
                        GL.Vertex3(vertice);
                    }
                }
            }

            GL.End();
            GL.PopMatrix();
        }
    }
}