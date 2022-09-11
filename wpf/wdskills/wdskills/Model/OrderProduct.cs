using System.ComponentModel.DataAnnotations;
using wdskills.Core;

namespace wdskills.Model
{
    public class OrderProduct
    {
        [Key]
        public int OrderProductId { get; set; }
        public int OrderId { get; set; }
        public string? ProductArticleNumber { get; set; }
        public int ProductCount { get; set; }
    }
}
