using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Models
{
    public enum BodyType
    {
        FlyingV, LesPaul, Stratocaster, SuperStrat, Telecaster
    }
    public class Guitar
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public Guitarist Guitarist { get; set; }
        public GuitarStore GuitarStore { get; set; }
    }
}
