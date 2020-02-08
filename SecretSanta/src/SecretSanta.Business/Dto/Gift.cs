using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{
    public class Gift : GiftInput
    {
        [Required]
        public int? Id { get; set; }
    }
}
