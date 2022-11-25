using Domain.Core.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models.Basket
{
    public class Basket
    {
        public Client Client { get; set; }
        public List<BasketItems> BasketItems { get; set; }
        public decimal Price { get; set; }

        public Basket(Client client, List<BasketItems> basketItems, decimal price)
        {
            Client = client;
            BasketItems = basketItems;
            Price = price;
        }
       
    }
}
