using System.ComponentModel.DataAnnotations;

namespace wdskills.WebServer.Model
{
    public class Point
    {
        [Key]
        public int PointId { get; set; }
        public string? PointName { get; set; }
    }
}
