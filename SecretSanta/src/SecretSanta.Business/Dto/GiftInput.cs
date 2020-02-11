using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{

    public class GiftInput
    {

        [Required]
        public string? Title { get; set; }

        public string? Url { get; set; }

        public string? Description { get; set; }

        [Required]
        public int? UserId { get; set; }

    }

}
