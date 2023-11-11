using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactMangaer.Data.Entities.Base;
using ContactMangaer.Data.Enums;

namespace ContactManager.Data
{
    public class Address : Entity
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public AddressType Type { get; set; }
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }

    }
}
