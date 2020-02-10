using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{
    class GroupInput
    {
        [Required]
        public string? Title { get; set; }
    }
}
