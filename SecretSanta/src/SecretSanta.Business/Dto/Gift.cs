using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{
    public class Gift : GiftInput, IEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
