using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Shared.ViewModels
{
    public class OrderViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string City { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Note { get; set; }

   

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }

        public string ExpirationYear { get; set; }

        public string Cvc { get; set; }
        public ShoppingCartViewModel ShoppingCart { get; set; }
    }
}
