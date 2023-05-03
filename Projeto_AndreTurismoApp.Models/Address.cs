using Projeto_AndreTurismoApp.Models.DTO;

namespace Projeto_AndreTurismoApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public City City { get; set; }

        public Address() { }

        public Address(AddressDTO addressDTO)
        {
            Street = addressDTO.Street;
            PostalCode = addressDTO.PostalCode;
            City = new City() { CityName = addressDTO.City };
        }
    }
}
