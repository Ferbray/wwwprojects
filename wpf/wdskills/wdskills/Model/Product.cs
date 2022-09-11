using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdskills.Core;

namespace wdskills.Model
{
    public class Product : ObservableObject
    {
        private string? productArticleNumber;
        private string? productName;
        private string? productUnits;
        private decimal productCost;
        private int productMaxDiscount;
        private string? productManufacture;
        private string? productProvider;
        private string? productCategory;
        private int productDiscount;
        private int productStockRoom;
        private string? productDescription;
        private string? productImage;
        [Key]
        public string? ProductArticleNumber
        {
            get => productArticleNumber;
            set
            {
                productArticleNumber = value;
                OnPropertyChanged("ProductArticleNumber");
            }
        }
        public string? ProductName
        {
            get => productName;
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }
        public string? ProductUnits
        {
            get => productUnits;
            set
            {
                productUnits = value;
                OnPropertyChanged("ProductUnits");
            }
        }
        [Precision(18, 4)]
        public decimal ProductCost
        {
            get => productCost;
            set
            {
                productCost = value;
                OnPropertyChanged("ProductCost");
            }
        }
        public int ProductMaxDiscount
        {
            get => productMaxDiscount;
            set
            {
                productMaxDiscount = value;
                OnPropertyChanged("ProductMaxDiscount");
            }
        }
        public string? ProductManufacture
        {
            get => productManufacture;
            set
            {
                productManufacture = value;
                OnPropertyChanged("ProductManufacture");
            }
        }
        public string? ProductProvider
        {
            get => productProvider;
            set
            {
                productProvider = value;
                OnPropertyChanged("ProductProvider");
            }
        }
        public string? ProductCategory
        {
            get => productCategory;
            set
            {
                productCategory = value;
                OnPropertyChanged("ProductCategory");
            }
        }
        public int ProductDiscount
        {
            get => productDiscount;
            set
            {
                productDiscount = value;
                OnPropertyChanged("ProductDiscount");
            }
        }
        public int ProductStockRoom
        {
            get => productStockRoom;
            set
            {
                productStockRoom = value;
                OnPropertyChanged("ProductStockRoom");
            }
        }
        public string? ProductDescription
        {
            get => productDescription;
            set
            {
                productDescription = value;
                OnPropertyChanged("ProductDescription");
            }
        }
        public string? ProductImage
        {
            get => productImage;
            set
            {
                productImage = value;
                OnPropertyChanged("ProductImage");
            }
        }
    }
}
