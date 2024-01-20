using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.BAL.Exceptions
{
    public class NoItemsFoundException : Exception
    {
        public NoItemsFoundException() : base("No items found.")
        {
            // You can add custom initialization code here if needed.
        }

        public NoItemsFoundException(string message) : base(message)
        {
            // You can add custom initialization code here if needed.
        }

        public NoItemsFoundException(string message, Exception innerException) : base(message, innerException)
        {
            // You can add custom initialization code here if needed.
        }
    }
}
