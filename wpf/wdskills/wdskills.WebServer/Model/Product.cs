using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace wdskills.WebServer.Model
{
    public class Product
    {
        [Key]
        public string? ProductArticleNumber { get; set; }
        public string? ProductName { get; set; }
        public string? ProductUnits { get; set; }
        [Precision(18, 4)]
        public decimal ProductCost { get; set; }
        public int ProductMaxDiscount { get; set; }
        public string? ProductManufacture { get; set; }
        public string? ProductProvider { get; set; }
        public string? ProductCategory { get; set; }
        public int ProductDiscount { get; set; }
        public int ProductStockRoom { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImage { get; set; }
    }
}
