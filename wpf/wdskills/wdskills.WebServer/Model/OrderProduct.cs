using System.ComponentModel.DataAnnotations;

namespace wdskills.WebServer.Model
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
