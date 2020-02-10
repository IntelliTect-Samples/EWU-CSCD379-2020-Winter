using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{
    public class GroupInput : IEntity
    {
        public int Id { get; }

        [Required]
        public string? Title { get; set; }
    }
}
