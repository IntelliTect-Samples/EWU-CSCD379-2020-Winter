using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.Api
{
    [ModelMetadataType(typeof(GiftInputMetadata))]
    public partial class GiftInput
    {
    }

    public class GiftInputMetadata
    {
        [Display(Name = "User Id")]
        public int? UserId { get; set; }
    }
}
