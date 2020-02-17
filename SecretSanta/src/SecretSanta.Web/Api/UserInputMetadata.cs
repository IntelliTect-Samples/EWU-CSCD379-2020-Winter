using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
    }
}
