using ContactMangaer.Data.Entities.Base;
using ContactMangaer.Data.Enums;

namespace ContactManager.DAL;
public class EmailAddress : Entity
{
    public string Email { get; set; }
    public EmailType Type { get; set; }
    public bool IsPrimary { get; set; }
    public Contact Contact { get; set; }
    public Guid ContactId { get; set; }
}

