using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactMangaer.Data.Enums;

namespace ContactMangaer.Core.DTOs.Emails
{
    public class EmailDto
    {
        public EmailType Type { get; set; }
        public string Email { get; set; }
    }
}
