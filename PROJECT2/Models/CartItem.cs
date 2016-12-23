using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace PROJECT2.Models
{
    [Serializable]
    public class CartItem
    {
        public Sach productOrder { get; set; }        
        public int Quantity { get; set; }
    }
}