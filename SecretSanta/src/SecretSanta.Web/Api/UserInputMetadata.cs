using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Web.Api
{
    [ModelMetadataType(typeof(UserInputMetadata))]
    public partial class UserInput
    {

    }
    public class UserInputMetadata
    {
        //Justification: Does not apply to metadata
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

    }
}
