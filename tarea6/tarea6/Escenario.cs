using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;

namespace tarea6
{
    internal class Escenario
    {
        private List<Objeto> objetos = new();
        private Vector3 centroMasa;


        public Escenario(Vector3 centro)
        {
            centroMasa = centro;
        }

        public void AddObjeto(Objeto objeto) => objetos.Add(objeto);
        public void RemoveObjeto(Objeto objeto) => objetos.Remove(objeto);
        public List<Objeto> GetObjetos() => objetos;
        public Vector3 GetCentro() => centroMasa;
        public void SetCentro(Vector3 nuevoCentro) => centroMasa = nuevoCentro;

        public void Dibujar()
        {
            foreach (var objeto in objetos)
            {
                
                objeto.Dibujar();
            }
        }
    }
}
