namespace Projeto_AndreTurismoApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
