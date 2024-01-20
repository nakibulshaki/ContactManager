using ContactMangaer.Data.Entities.Base;

namespace ContactManager.DAL;
public class Contact : Entity
{
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DOB { get; set; }
    public virtual List<EmailAddress> EmailAddresses { get; set; } = new List<EmailAddress>();
    public virtual List<Address> Addresses { get; set; } = new List<Address>();

    // New property to store the primary email address
    public Guid? PrimaryEmailAddressId { get; set; }
    public virtual EmailAddress PrimaryEmailAddress { get; set; }
}


