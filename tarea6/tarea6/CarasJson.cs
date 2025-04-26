using OpenTK.Mathematics;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using tarea6;

namespace tarea6
{
    public class CaraJson
    {
        public List<VerticeJson> vertices { get; set; }
    }

    public class VerticeJson
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }
}