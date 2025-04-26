using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;


namespace tarea6
{
    internal class Objeto
    {
        public Vector3 Posicion { get; set; } = Vector3.Zero;
        public Vector3 Rotacion { get; set; } = Vector3.Zero;
        public float Escala { get; set; } = 1f;

        private List<Parte> partes = new();

        public Objeto(Vector3 centro)
        {
            Posicion = centro;
        }
        public Vector3 GetCentro()
        {
            return Posicion;
        }
        public Vector3 GetRot()
        {
            return Rotacion;
        }
        public List<Parte> GetPartes() => partes;
        public void AddParte(Parte parte) => partes.Add(parte);

        public void Dibujar()
        {
            GL.PushMatrix();
            GL.Translate(Posicion);
            GL.Rotate(Rotacion.Z, 0f, 0f, 1f);
            GL.Rotate(Rotacion.Y, 0f, 1f, 0f);
            GL.Rotate(Rotacion.X, 1f, 0f, 0f);
          
            GL.Scale(Escala, Escala, Escala);

            foreach (var parte in partes)
            {
                parte.Dibujar();
            }
            //GL.Rotate(90f, 0f, 1f, 0f);
            GL.PopMatrix();
        }
    }
}
