using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ChineseAction.Api.NewFolder
{
    public class Gift
    {
        public int Id { get; set; } // ID מתנה

        [Required,MaxLength(100)]
        public string Name { get; set; } // תיאור/שם המתנה

        [MaxLength(500)]
        public string Description { get; set; } 

        [Range(0,1000)]
        public decimal TicketPrice { get; set; } // מחיר כרטיס הגרלה 
        public string ImageUrl { get; set; } // אופציה להוסיף תמונה 

        public int CategoryId { get; set; } // קטגוריה

        public Category Category { get; set; } // קטגוריה

        public int DonorId { get; set; }
        public Donor Donor { get; set; }

        // קשר לזכיות ורכישות
        public ICollection<OrderItem> OrderItems { get; set; }
        public Winner? Winner { get; set; } // דוח מי הזוכה
    }
}
