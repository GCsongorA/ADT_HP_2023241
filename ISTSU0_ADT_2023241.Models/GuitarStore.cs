using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Models
{
    public class GuitarStore
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Guitar> Guitars { get; set; }
    }
}
