namespace Ecommerce.Models
{
    public class Command
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String Address { get; set; }

        public ICollection<CommandItem> Items { get; set; }
    }
}