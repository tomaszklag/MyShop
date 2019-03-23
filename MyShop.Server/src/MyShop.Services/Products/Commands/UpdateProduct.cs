using System;
using MyShop.Core.Domain;
using Newtonsoft.Json;

namespace MyShop.Services.Products.Commands
{
    public class UpdateProduct : ICommand, IIdentifiable
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Vendor { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public UpdateProduct(Guid id, string name, 
            string description, string vendor, 
            decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Vendor = vendor;
            Price = price;
            Quantity = quantity;
        }
    }
}