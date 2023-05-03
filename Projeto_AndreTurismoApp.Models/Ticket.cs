namespace Projeto_AndreTurismoApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Address Origin { get; set; }
        public Address Destination { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public decimal TicketValue { get; set; }
    }
}
