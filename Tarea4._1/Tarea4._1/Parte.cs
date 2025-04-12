using OpenTK.Mathematics;
using System.Collections.Generic;
using System.IO;

namespace Tarea4._1
{
    internal class Parte
    {
        private List<Cara> caras = new();

        public Parte(string rutaArchivo)
        {
            CargarDesdeArchivo(rutaArchivo);
        }

        private void CargarDesdeArchivo(string ruta)
        {
            try
            {
                using StreamReader sr = new StreamReader(ruta);
                string? linea;

                while ((linea = sr.ReadLine()) != null)
                {
                    linea = linea.Trim();

                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    
                    string[] datos = linea.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    if (datos.Length != 12)
                    {
                        Console.WriteLine($"⚠️ Línea inválida (esperado 12 valores): \"{linea}\"");
                        continue;
                    }

                    List<Vector3> verticesCara = new();

                    for (int i = 0; i < datos.Length; i += 3)
                    {
                        float x = float.Parse(datos[i]);
                        float y = float.Parse(datos[i + 1]);
                        float z = float.Parse(datos[i + 2]);
                        verticesCara.Add(new Vector3(x, y, z));
                    }

                    caras.Add(new Cara(verticesCara));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al leer el archivo: {ex.Message}");
            }
        }

        public List<Cara> GetCaras() => caras;

        public void AddCara(Cara cara) => caras.Add(cara);
        public void RemoveCara(Cara cara) => caras.Remove(cara);
    }
}
