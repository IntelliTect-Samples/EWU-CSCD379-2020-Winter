using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.Api
{
    [ModelMetadataType(typeof(GroupInputMetadata))]
    public partial class GroupInput
    {
    }

    public class GroupInputMetadata
    {
        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}
