namespace Projeto_AndreTurismoApp.Models
{
    public class Package
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime RegistrationDate { get; set; }
        public decimal PackageValue { get; set; }
        public Customer Customer { get; set; }
    }
}
