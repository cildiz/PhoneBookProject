using Contact.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contact.API.Contexts
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<ContactInformation> ContactInformations { get; set; }
    }
}
