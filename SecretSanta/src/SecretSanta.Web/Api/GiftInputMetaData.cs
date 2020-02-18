using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Web.Api
{

    [ModelMetadataType(typeof(UserInputMetadata))]
    public partial class GiftInput
    {

    }
    public class GiftInputMetaData
    {
        //Justification: Does not matter for meta data
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Display(Name = "User Id")]
        public string UserId { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    }
}
