using System.ComponentModel.DataAnnotations;
using wdskills.Core;

namespace wdskills.Model
{
    public class Point
    {
        [Key]
        public int PointId { get; set; }
        public string? PointName { get; set; }
    }
}
