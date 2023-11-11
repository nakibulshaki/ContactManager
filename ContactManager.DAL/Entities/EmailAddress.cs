using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactMangaer.Data.Entities.Base;
using ContactMangaer.Data.Enums;

namespace ContactManager.Data
{
    public class EmailAddress : Entity
    {
        public string Email { get; set; }
        public EmailType Type { get; set; }
        public Guid ContactId { get; set; }
        public  Contact Contact { get; set; }
    }
}
