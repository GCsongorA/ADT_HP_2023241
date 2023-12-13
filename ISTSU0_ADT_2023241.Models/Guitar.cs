using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        public BodyType BodyType { get; set; }
        [ForeignKey(nameof(Guitarist))]
        public Guid GuitaristId { get; set; }
        [JsonIgnore]
        public virtual Guitarist Guitarist { get; set; }
    }
}
