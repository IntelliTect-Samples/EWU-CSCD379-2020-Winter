using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Web.Api
{

    [ModelMetadataType(typeof(UserInputMetadata))]
    public partial class UserInput
    {

    }

    public class UserInputMetadata
    {

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string? LastName { get; set; }

        [Display(Name = "Santa Id")]
        [Required]
        public int? SantaId { get; set; }

    }

}
