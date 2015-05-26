using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogyakarta_Effective_Route.Models
{
    public class TouristObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
    }
}
