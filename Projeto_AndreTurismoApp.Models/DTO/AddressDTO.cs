using Newtonsoft.Json;

namespace Projeto_AndreTurismoApp.Models.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }

        [JsonProperty("pais")]
        public string? Country { get; set; }

        [JsonProperty("cep")]
        public string PostalCode { get; set; }

        [JsonProperty("bairro")]
        public string Neighborhood { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }

        [JsonProperty("uf")]
        public string State { get; set; }

        [JsonProperty("logradouro")]
        public string Street { get; set; }

        [JsonProperty("gia")]
        public int Number { get; set; }

        [JsonProperty("complemento")]
        public string Complement { get; set; }
    }
}
