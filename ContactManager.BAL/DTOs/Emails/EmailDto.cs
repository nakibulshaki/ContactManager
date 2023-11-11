using ContactMangaer.Data.Enums;
namespace ContactManager.BAL.DTOs.Emails;

public class EmailDto
{
    public EmailType Type { get; set; }
    public string Email { get; set; }
    public bool IsPrimary { get; set; }
}

