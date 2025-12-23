using System.ComponentModel.DataAnnotations;

namespace ChineseAction.Api.NewFolder
{
    public class Purchaser
    {
        public int Id { get; set; } // ID רוכש
        [Required,MaxLength(100)]
        public string Name { get; set; } // שם
        [Phone,Required]
        public string Phone { get; set; } // טלפון
        [EmailAddress]
        public string Email { get; set; } // מייל

        [Required,MinLength(4)]
        public string Password { get; set; }

        public List<Order> Orders { get; set; }
    }
}
