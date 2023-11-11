using ContactManager.Data;
using ContactManager.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using ContactMangaer.Core.DTOs.Contacts;
using ContactMangaer.Core;

namespace ContactManager.Controllers
{
    public class ContactsController : Controller
    {
        private ILogger<ContactsController> _logger;
        private readonly IContactAppService _contactAppService;
        private readonly IHubContext<ContactHub> _hubContext;

        public ContactsController(ILogger<ContactsController> logger,
            IContactAppService contactAppService,
            IHubContext<ContactHub> hubContext)
        {
            _logger = logger;
            _contactAppService = contactAppService;
            _hubContext = hubContext;
            _logger.LogInformation("Contact controller called!");
        }
        public async Task<IActionResult> GetContacts()
        {
            _logger.LogInformation("Get Contact Called!");
            var contactList = await _contactAppService.GetContactsAsync();
            _logger.LogInformation("Get Contact Completed!");
            return PartialView("_ContactTable", contactList);
        }

        public async Task<IActionResult> DeleteContact(Guid id)
        {
            _logger.LogInformation("Delete Contact Called!");
            await _contactAppService.DeleteAsync(id);
            _logger.LogInformation("Delete Contact Completed!");
            await _hubContext.Clients.All.SendAsync("Update");
            return Ok();
        }

        public async Task<IActionResult> EditContact(Guid id)
        {
            _logger.LogInformation("Edit Contact Called!");
            var editContact = await _contactAppService.GetEditContactByIdAsync(id);
            _logger.LogInformation("Edit Contact Completed!");
            return PartialView("_EditContact", editContact);
        }

      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewContact()
        {
            _logger.LogInformation("New Contact Called!");

            return PartialView("_EditContact", new EditContactDto());
        }

        [HttpPost]
        public async Task<IActionResult> SaveContact([FromBody] CreateUpdateContactDto model)
        {
            _logger.LogInformation("Save/Update Contact Called!");

            var contact = model.ContactId == Guid.Empty
                ? await _contactAppService.CreateContactAsync(model)
                : await _contactAppService.UpdateContactAsync(model);

            _logger.LogInformation("Save/Update Contact Completed!");

            await _hubContext.Clients.All.SendAsync("Update");
            _logger.LogInformation("Data Update Completed Email Notification Called!");
            SendEmailNotification(contact.Id);
            _logger.LogInformation("Data Update Completed Email Notification Completed!");

            return Ok();
        }

        private void SendEmailNotification(Guid contactId)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("noreply", "noreply@contactmanager.com"));
            message.To.Add(new MailboxAddress("SysAdmin", "Admin@contactmanager.com"));
            message.Subject = "ContactManager System Alert";

            message.Body = new TextPart("plain")
            {
                Text = "Contact with id:" + contactId.ToString() + " was updated"
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("127.0.0.1", 25, false);

                client.Send(message);
                client.Disconnect(true);
            }

        }

    }

}