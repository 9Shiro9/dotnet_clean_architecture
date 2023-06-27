using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer
    {
        public string Id { get;set;}
        public string Name { get; set; }
        public Address Address { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Product> Wishlist { get; set; }
    }
}
