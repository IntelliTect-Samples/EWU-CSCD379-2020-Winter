using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Business.Dto
{
	public class User : UserInput, IEntity
	{
		[Required]
		public int Id { get; set; }
	}
}
