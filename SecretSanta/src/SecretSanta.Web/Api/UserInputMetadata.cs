using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

		[Display(Name = "Secret Santa Id")]
		[Required]
		public int? SantaId { get; set; }
	}
}
