using System;
using wdskills.WebServer.Model;
using wdskills.WPF.Core;

namespace wdskills.WPF.Services
{
    public class TransferProductService : ObservableObject
    {
        public Product? Product { get; private set; }
        public event Action<Product>? OnProductChanged;
        public void ChangeProduct(Product? product)
        {
            Product = product;
            OnProductChanged?.Invoke(Product ?? new());
        }
    }
}
