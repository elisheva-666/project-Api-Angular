using System.Reflection;

namespace ChineseAction.Api.NewFolder
{
    public class Gift
    {
        public int Id { get; set; } // ID מתנה
        public string Name { get; set; } // תיאור/שם המתנה
        public string Category { get; set; } // קטגוריה 
        public decimal TicketPrice { get; set; } // מחיר כרטיס הגרלה 
        public string ImageUrl { get; set; } // אופציה להוסיף תמונה 

        public int DonorId { get; set; }
        public Donor Donor { get; set; }

        // קשר לזכיות ורכישות
        public ICollection<OrderItem> OrderItems { get; set; }
        public Winner Winner { get; set; } // דוח מי הזוכה
    }
}
