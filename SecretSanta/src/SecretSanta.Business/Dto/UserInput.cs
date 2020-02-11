using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{
    public class UserInput
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? SantaId { get; set; }
    }
}
