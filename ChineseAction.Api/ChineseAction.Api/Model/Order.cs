namespace ChineseAction.Api.NewFolder
{
    public class Order
    {
        public int Id { get; set; } // ID הזמנה
        public DateTime OrderDate { get; set; }

        // שדה קריטי: נשמר כטיוטה ורק לאחר אישור נרכש בפועל 
        public bool IsDraft { get; set; }

        public int PurchaserId { get; set; }
        public Purchaser Purchaser { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }=new List<OrderItem>();
    }
}
