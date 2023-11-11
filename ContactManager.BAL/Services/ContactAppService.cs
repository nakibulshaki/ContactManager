using ContactManager.Data;
using ContactManager.Data.Contexts;
using ContactMangaer.Core.DTOs.Contacts;
using Microsoft.EntityFrameworkCore;

namespace ContactMangaer.Core;

public class ContactAppService : IContactAppService
{
    private readonly ApplicationContext _context;
    public ContactAppService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task DeleteAsync(Guid id)
    {
        await _context.Contacts
       .Where(x => x.Id == id)
       .ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }
    private async Task<Contact> GetContactAsync(Guid contactId)
    {
        var contact = await _context.Contacts
            .Include(x => x.EmailAddresses)
            .Include(x => x.Addresses)
            .FirstOrDefaultAsync(x => x.Id == contactId);
        return contact;
    }
    public async Task<IEnumerable<EditContactDto>> GetEditContactByIdAsync(Guid id)
    {
        var filteredContacts = await _context.Contacts
                            .Where(x => x.Id == id)
                            .Select(contact => new EditContactDto
                            {
                                Id = contact.Id,
                                Title = contact.Title,
                                FirstName = contact.FirstName,
                                LastName = contact.LastName,
                                DOB = contact.DOB,
                                EmailAddresses = contact.EmailAddresses,
                                Addresses = contact.Addresses
                            }).ToListAsync();

        return filteredContacts;
    }
    public async Task<ContactPreviewDto> GetContactsAsync()
    {
        var contacts = await _context.Contacts
                       .OrderBy(x => x.FirstName)
                       .ToListAsync();

        return new ContactPreviewDto { Contacts = contacts };
    }
    public async Task<Contact> CreateContactAsync(CreateUpdateContactDto model)
    {
        var contact = new Contact
        {
            Title = model.Title,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DOB = model.DOB
        };
        // Add  email addresses
        contact.EmailAddresses.AddRange(model.Emails.Select(email => new EmailAddress
        {
            Type = email.Type,
            Email = email.Email,
            Contact = contact
        }));

        // Add  addresses
        contact.Addresses.AddRange(model.Addresses.Select(address => new Address
        {
            Street1 = address.Street1,
            Street2 = address.Street2,
            City = address.City,
            State = address.State,
            Zip = address.Zip,
            Type = address.Type
        }));
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
        return contact;
    }
    public async Task<Contact> UpdateContactAsync(CreateUpdateContactDto model)
    {
        var contact = await GetContactAsync(model.ContactId);

        // Clear existing email addresses and addresses
        contact.EmailAddresses.Clear();
        contact.Addresses.Clear();

        // Add new email addresses
        contact.EmailAddresses.AddRange(model.Emails.Select(email => new EmailAddress
        {
            Type = email.Type,
            Email = email.Email,
            Contact = contact
        }));

        // Add new addresses
        contact.Addresses.AddRange(model.Addresses.Select(address => new Address
        {
            Street1 = address.Street1,
            Street2 = address.Street2,
            City = address.City,
            State = address.State,
            Zip = address.Zip,
            Type = address.Type
        }));

        // Update contact details
        contact.Title = model.Title;
        contact.FirstName = model.FirstName;
        contact.LastName = model.LastName;
        contact.DOB = model.DOB;

        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
        return contact;
    }
}
