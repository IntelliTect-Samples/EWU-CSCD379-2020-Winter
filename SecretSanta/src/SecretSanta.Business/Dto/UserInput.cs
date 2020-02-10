using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{
    class UserInput
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }
    }
}
