using System.ComponentModel;

namespace Contact.API.Enumerables
{
    public enum InformationType
    {
        [Description("Telefon Numarası")]
        PhoneNumber,
        [Description("E-Mail Adresi")]
        EmailAddress,
        [Description("Konum")]
        Location
    }
}
