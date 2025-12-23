using ChineseAction.Api.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ChineseAction.Api.NewFolder
{
    public class Donor
    {
        public int Id { get; set; } // ID תורם

        [Required,MaxLength(1000)]
        public string Name { get; set; } // שם

        [Required,EmailAddress]
        public string Email { get; set; } // מייל - לצורך סינון
        [Phone]
        public string? Phone { get; set; } // פרטים נוספים

        // קשר למתנות: כל תורם מכיל את רשימת התרומות שלו 
        public ICollection<Gift> Gifts { get; set; }

        public static explicit operator Donor(CreateDonorDtoWithGift v)
        {
            throw new NotImplementedException();
        }
    }
}
