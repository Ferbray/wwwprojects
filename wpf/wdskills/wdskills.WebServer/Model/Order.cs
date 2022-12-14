using System;
using System.ComponentModel.DataAnnotations;

namespace wdskills.WebServer.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public int PointId { get; set; }
        public string? OrderCodeGet { get; set; }
        public string? OrderSurnameClient { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
