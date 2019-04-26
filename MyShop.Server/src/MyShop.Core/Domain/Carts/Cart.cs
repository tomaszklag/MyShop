using System;
using System.Collections.Generic;
using System.Linq;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products;

namespace MyShop.Core.Domain.Carts
{
    public class Cart : BaseEntity
    {
        private ISet<CartItem> _items = new HashSet<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get => _items;
            set => _items = new HashSet<CartItem>(value);
        }

        public Cart(Guid userId) : base(userId)
        {
        }

        public void AddProduct(Product product, int quantity)
        {
            var item = GetCartItem(product.Id);
            if (item != null)
            {
                item.IncreaseQuantity(quantity);
                return;
            }
            
            item = new CartItem(product, quantity);
            _items.Add(item);
        }

        public void DeleteProduct(Guid productId)
        {
            var item = GetCartItem(productId);
            if (item is null)
            {
                throw new MyShopException("product_not_found",
                    $"Product with id: '{productId}' was not found.");
            }

            _items.Remove(item);
        }

        public void UpdateProduct(Product product)
        {
            var item = GetCartItem(product.Id);
            if (item is null)
            {
                throw new MyShopException("product_not_found",
                    $"Product with id: '{product.Id}' was not found.");
            }

            item.UpdateProduct(product);
        }

        private CartItem GetCartItem(Guid productId)
            => _items.SingleOrDefault(x => x.ProductId == productId);
    }
}