using System;

namespace wdskills.WPF.Services
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
