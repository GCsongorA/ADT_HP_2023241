using ISTSU0_ADT_2023241.Models;

namespace ISTSU0_ADT_2023241.Endpoint.Dtos
{
    public class CreateGuitaristDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Guid BandId { get; set; } 
    }
}
