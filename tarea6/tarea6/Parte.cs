using System.Text.Json;
using OpenTK.Mathematics;

using OpenTK.Graphics.OpenGL;
namespace tarea6
{
    internal class Parte
    {
        public Vector3 Posicion { get; set; } = Vector3.Zero;
        public Vector3 Rotacion { get; set; } = Vector3.Zero;
        public float Escala { get; set; } = 1f;
        private List<Cara> caras = new();

        public Parte(string rutaArchivo)
        {
            if (rutaArchivo.EndsWith(".json"))
                CargarDesdeJson(rutaArchivo);
            else
                CargarDesdeArchivoPlano(rutaArchivo);
        }

        private void CargarDesdeArchivoPlano(string ruta)
        {
            try
            {
                using StreamReader sr = new StreamReader(ruta);
                string? linea;

                while ((linea = sr.ReadLine()) != null)
                {
                    linea = linea.Trim();
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    string[] datos = linea.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (datos.Length != 12) continue;

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
                Console.WriteLine($"❌ Error al leer el archivo TXT: {ex.Message}");
            }
        }

        private void CargarDesdeJson(string ruta)
        {
            try
            {
                string json = File.ReadAllText(ruta);
                var carasJson = JsonSerializer.Deserialize<List<CaraJson>>(json);

                foreach (var caraJson in carasJson)
                {
                    List<Vector3> vertices = new();
                    foreach (var v in caraJson.vertices)
                    {
                        vertices.Add(new Vector3(v.x, v.y, v.z));
                    }
                    caras.Add(new Cara(vertices));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al leer JSON: {ex.Message}");
            }
        }

        public List<Cara> GetCaras() => caras;

        public void AddCara(Cara cara) => caras.Add(cara);

        public void RemoveCara(Cara cara) => caras.Remove(cara);

        public void Dibujar()
        {
            GL.PushMatrix();

            GL.Translate(Posicion);
            
            GL.Rotate(Rotacion.Z, 0f, 0f, 1f);
            GL.Rotate(Rotacion.Y, 0f, 1f, 0f);
            GL.Rotate(Rotacion.X, 1f, 0f, 0f);
            GL.Scale(Escala, Escala, Escala);
           
            GL.Color3(1f, 1f, 0f);
            GL.Begin(PrimitiveType.Quads);

            foreach (var cara in caras)
            {
                cara.Dibujar();
            }

            GL.End();
            GL.PopMatrix();
        }
       
    }
}
