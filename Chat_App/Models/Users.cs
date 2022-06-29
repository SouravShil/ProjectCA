using System.ComponentModel.DataAnnotations;

namespace Chap_App.Models
{
    public class Users
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string? DOB { get; set; }
    }
}