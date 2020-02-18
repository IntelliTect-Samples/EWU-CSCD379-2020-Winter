using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Web.Api
{
    [ModelMetadataType(typeof(UserInputMetaData))]
    public partial class UserInput
    {

    }

    public class UserInputMetaData
    {
        // Justification: This is meta-data so it being null isn't a problem
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Santa Id")]
        public int SantaId { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    }
}
