using System.ComponentModel.DataAnnotations;

namespace wdskills.WebServer.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? UserPatronymic { get; set; }
        public string? UserLogin { get; set; }
        public string? UserPassword { get; set; }
        public int RoleId { get; set; }

        public override string ToString()
        {
            return $"{UserName} {UserSurname} {UserPatronymic}";
        }
    }
}
