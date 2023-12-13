using ISTSU0_ADT_2023241.Models;

namespace ISTSU0_ADT_2023241.Endpoint.Dtos
{
    public class CreateGuitarDto
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public Guid GuitaristId { get; set; }
        public BodyType BodyType { get; set; }
    }
}
