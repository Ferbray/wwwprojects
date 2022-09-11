using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdskills.Core;
using wdskills.Model;

namespace wdskills.Services
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
