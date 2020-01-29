using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Data
{
    public class EntityBase
    {
        [Required]
        public int? Id { get; protected set; }
    }
}
