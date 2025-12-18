namespace ChineseAction.Api.NewFolder
{
    public class Purchaser
    {
        public int Id { get; set; } // ID רוכש
        public string Name { get; set; } // שם
        public string Phone { get; set; } // טלפון
        public string Email { get; set; } // מייל

        public List<Order> Orders { get; set; }
    }
}
