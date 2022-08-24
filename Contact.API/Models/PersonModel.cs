namespace Contact.API.Models
{
    public class PersonModel
    {
        public Guid UUID { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Company { get; set; } = "";
    }
}
