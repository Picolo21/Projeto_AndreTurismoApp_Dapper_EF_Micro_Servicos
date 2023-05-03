namespace Projeto_AndreTurismoApp.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public Address Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public decimal HotelValue { get; set; }
    }
}
