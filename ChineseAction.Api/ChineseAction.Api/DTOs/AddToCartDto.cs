namespace ChineseAction.Api.DTOs
{
    // DTO לקריאת בקשת הוספה לסל
    public class AddToCartDto
    {
        public int PurchaserId { get; set; } // מי הרוכש שמוסיף לסל
        public int GiftId { get; set; } // איזו מתנה
        public int Quantity { get; set; } = 1; // כמות (ברירת מחדל 1)
    }
}