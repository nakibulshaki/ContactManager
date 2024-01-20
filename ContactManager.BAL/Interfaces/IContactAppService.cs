﻿using ContactManager.DAL;
using ContactManager.BAL.DTOs.Contacts;

namespace ContactManager.BAL;
public interface IContactAppService
{
    Task DeleteAsync(Guid id);
    Task <EditContactDto>GetEditContactByIdAsync(Guid id);
    Task<IEnumerable<ContactPreviewDto>> GetContactsAsync();
    Task<Contact> UpdateContactAsync(CreateUpdateContactDto model);
    Task<Contact> CreateContactAsync(CreateUpdateContactDto model);
}

