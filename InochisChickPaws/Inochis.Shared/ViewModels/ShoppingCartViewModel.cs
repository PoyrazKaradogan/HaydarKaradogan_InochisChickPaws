﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Shared.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }


        public List<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }


        public decimal TotalPrice()
        {
            return ShoppingCartItems.Sum(x=>x.ProductPrice *  x.Quantity);
        }

    }
}
