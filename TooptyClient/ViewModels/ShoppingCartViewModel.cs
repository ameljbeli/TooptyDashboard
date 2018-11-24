using TooptyClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TooptyClient.ViewModel
{
    public class ShoppingCartViewModel
    {
        [Key]
        public List<ShoppingCartItem> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}