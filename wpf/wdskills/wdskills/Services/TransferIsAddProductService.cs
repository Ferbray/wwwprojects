using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdskills.Services
{
    public class TransferIsAddProductService
    {
        public bool IsAddProduct { get; private set; }
        public event Action<bool>? OnIsAddProductChanged;
        public void ChangeIsAddProduct(bool isAddProduct)
        {
            IsAddProduct = isAddProduct;
            OnIsAddProductChanged?.Invoke(IsAddProduct);
        }
    }
}
