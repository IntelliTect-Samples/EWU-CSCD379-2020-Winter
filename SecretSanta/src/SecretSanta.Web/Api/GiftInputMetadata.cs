using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//Added comment here to create pull request, delete later
namespace SecretSanta.Web.Api
{
    [ModelMetadataType(typeof(GiftInputMetadata))]
    public partial class GiftInput
    {
    }
    public class GiftInputMetadata
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Display(Name = "Url")]
        [Url]
        public string? Url { get; set; }
    }
}
