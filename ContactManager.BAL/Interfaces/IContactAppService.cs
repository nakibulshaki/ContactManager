using ContactManager.DAL;
using ContactManager.BAL.DTOs.Contacts;

namespace ContactManager.BAL;
public interface IContactAppService
{
    Task DeleteAsync(Guid id);
    Task<IEnumerable<EditContactDto>> GetEditContactByIdAsync(Guid id);
    Task<ContactPreviewDto> GetContactsAsync();
    Task<Contact> UpdateContactAsync(CreateUpdateContactDto model);
    Task<Contact> CreateContactAsync(CreateUpdateContactDto model);
}

