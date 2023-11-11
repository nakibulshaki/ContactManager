using ContactManager.DAL;

namespace ContactManager.BAL.DTOs.Contacts;

public class ContactPreviewDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PrimaryEmail { get; set; }
}

