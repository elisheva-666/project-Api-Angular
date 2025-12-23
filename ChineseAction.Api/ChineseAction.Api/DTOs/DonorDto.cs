using ChineseAction.Api.NewFolder;

namespace ChineseAction.Api.DTOs
{
    public class DonorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
    }

    public class CreateDonorDtoWithGift
    {
        public string Name { get; set; }
        public string? Phone { get; set; }

        public string? Email { get; set; }= null;

    }
    public class AddGiftToDonorDTO {
        public string Email { get; set; }
        //public GiftDTO  Giftdto { get; set; }
    }
}