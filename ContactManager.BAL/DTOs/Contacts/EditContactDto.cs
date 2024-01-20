using ContactManager.DAL;
namespace ContactManager.BAL.DTOs.Contacts;

public class EditContactDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DOB { get; set; }
    public List<EmailAddress> EmailAddresses { get; set; }
    public List<Address> Addresses { get; set; }
}

