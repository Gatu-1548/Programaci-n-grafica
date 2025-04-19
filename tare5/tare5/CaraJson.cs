using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tare5
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
