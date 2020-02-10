using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{
    public class GiftInput : IEntity
    {
        public int Id { get; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Url { get; set; }
    }
}
