namespace ChineseAction.Api.NewFolder
{
    public class Donor
    {
        public int Id { get; set; } // ID תורם
        public string Name { get; set; } // שם
        public string Email { get; set; } // מייל - לצורך סינון
        public string Phone { get; set; } // פרטים נוספים

        // קשר למתנות: כל תורם מכיל את רשימת התרומות שלו 
        public ICollection<Gift> Gifts { get; set; }
    }
}
