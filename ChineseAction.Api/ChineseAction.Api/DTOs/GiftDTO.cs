using ChineseAction.Api.NewFolder;
using System.ComponentModel.DataAnnotations;

namespace ChineseAction.Api.DTOs
{
    public class GetGiftDTO
    {

        public string Name { get; set; } // שם
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal TicketPrice { get; set; } // מחיר
        public int CategoryId { get; set; } // קטגוריה
    }

    public class AddGiftDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } // תיאור/שם המתנה

        [MaxLength(500)]
        public string Description { get; set; }

        [Range(0, 1000)]
        public decimal TicketPrice { get; set; } // מחיר כרטיס הגרלה 
        public string ImageUrl { get; set; } // אופציה להוסיף תמונה 

        public int CategoryId { get; set; } // קטגוריה
    }
}
