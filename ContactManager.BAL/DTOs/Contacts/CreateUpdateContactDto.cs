using ContactMangaer.Core.DTOs.Adresses;
using ContactMangaer.Core.DTOs.Emails;

namespace ContactMangaer.Core.DTOs.Contacts
{
    public class CreateUpdateContactDto
    {
        public Guid ContactId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public List<EmailDto> Emails { get; set; } = new List<EmailDto>();
        public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();

    }
}
