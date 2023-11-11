using ContactManager.DAL;
using ContactManager.DAL.Contexts;
using ContactManager.BAL.DTOs.Contacts;
using Microsoft.EntityFrameworkCore;
using ContactManager.BAL.Exceptions;
using ContactManager.BAL.DTOs.Adresses;
using ContactManager.BAL.DTOs.Emails;

namespace ContactManager.BAL;

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
    public async Task<IEnumerable<ContactPreviewDto>> GetContactsAsync()
    {
        var contacts = await _context.Contacts
                       .OrderBy(x => x.FirstName)
                       .Select(x => new ContactPreviewDto
                       {
                           Id = x.Id,
                           Title = x.Title,
                           FirstName = x.FirstName,
                           LastName = x.LastName,
                           PrimaryEmail = x.PrimaryEmailAddress != null ?
                                       x.PrimaryEmailAddress.Email :
                                       x.EmailAddresses.FirstOrDefault().Email ?? "N/A"
                       })
                       .ToListAsync();

        return contacts;
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

        AddEmailsAndAddresses(contact, model.Emails, model.Addresses);

        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    public async Task<Contact> UpdateContactAsync(CreateUpdateContactDto model)
    {
        var contact = await GetContactAsync(model.ContactId);

        contact.EmailAddresses.Clear();
        contact.Addresses.Clear();

        AddEmailsAndAddresses(contact, model.Emails, model.Addresses);

        contact.Title = model.Title;
        contact.FirstName = model.FirstName;
        contact.LastName = model.LastName;
        contact.DOB = model.DOB;

        await _context.SaveChangesAsync();
        return contact;
    }
    private void AddEmailsAndAddresses(Contact contact, List<EmailDto> emails, List<AddressDto> addresses)
    {
        if (emails.Count < 1)
            throw new NoItemsFoundException("No Email Found In Contact");

        contact.EmailAddresses.AddRange(emails.Select(email => new EmailAddress
        {
            Type = email.Type,
            Email = email.Email,
            Contact = contact,
            IsPrimary = email.IsPrimary
        }));

        if (addresses.Count < 1)
            throw new NoItemsFoundException("No Address Found In Contact");

        contact.Addresses.AddRange(addresses.Select(address => new Address
        {
            Street1 = address.Street1,
            Street2 = address.Street2,
            City = address.City,
            State = address.State,
            Zip = address.Zip,
            Type = address.Type
        }));
    }
}
