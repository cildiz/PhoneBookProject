namespace Contact.API.Models
{
    public class PersonDetailModel
    {
        public PersonModel Person { get; set; }
        public List<ContactInformationModel> ContactInformations { get; set; }
    }
}
