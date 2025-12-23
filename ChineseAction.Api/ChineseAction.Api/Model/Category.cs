namespace ChineseAction.Api.NewFolder
{
    public class Category
    {
        public int Id { get; set; } // ID תורם
        public string Name { get; set; } // שם
        public ICollection<Gift> Gifts { get; set; }=new List<Gift>();
    }
}
