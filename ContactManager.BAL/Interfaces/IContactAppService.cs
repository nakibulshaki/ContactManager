using ContactManager.Data;
using ContactMangaer.Core.DTOs.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMangaer.Core
{
    public interface IContactAppService
    {
        Task DeleteAsync(Guid id);
        Task<IEnumerable<EditContactDto>> GetEditContactByIdAsync(Guid id);
        Task<ContactPreviewDto> GetContactsAsync();
        Task<Contact> UpdateContactAsync(CreateUpdateContactDto model);
        Task<Contact> CreateContactAsync(CreateUpdateContactDto model);
    }
}
