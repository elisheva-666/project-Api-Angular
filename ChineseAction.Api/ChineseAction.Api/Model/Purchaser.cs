using System.ComponentModel.DataAnnotations;

namespace ChineseAction.Api.Model
{
    public enum Role
    {
        Admin,
        Purchaser
    }

    public class Purchaser
    {
        public int Id { get; set; } // ID רוכש

        [Required, MaxLength(100)]
        public string Name { get; set; } // שם

        [Phone, Required]
        public string Phone { get; set; } // טלפון

        [EmailAddress]
        public string Email { get; set; } // מייל

        [Required, MinLength(4)]
        public string Password { get; set; } // סיסמה

        [Required]
        public Role Role { get; set; } = Role.Purchaser; // תפקיד (ברירת מחדל: Purchaser)

        public List<Order> Orders { get; set; } = new List<Order>(); // קשר להזמנות: כל רוכש מכיל את רשימת ההזמנות שלו
    }
}
