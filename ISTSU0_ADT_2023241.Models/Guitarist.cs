using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Models
{
    public class Guitarist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [ForeignKey(nameof(Band))]
        public Guid BandId { get; set; }
        //[JsonIgnore]
        public virtual List<Guitar> Guitars { get; set; }
        [JsonIgnore]
        public virtual Band Band { get; set; }
    }
}
