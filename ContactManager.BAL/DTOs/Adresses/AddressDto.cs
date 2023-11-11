using ContactManager.Data;
using ContactMangaer.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMangaer.Core.DTOs.Adresses
{
    public class AddressDto
    {
        public AddressType Type { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
    }
}
