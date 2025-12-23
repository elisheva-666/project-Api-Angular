namespace ChineseAction.Api.NewFolder
{
    public class Winner
    {
        public int Id { get; set; }

        public int GiftId { get; set; }
        public Gift Gift { get; set; }

        public int PurchaserId { get; set; }
        public Purchaser Purchaser { get; set; }
    }
}
