using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Business.Dto
{
	public class Group : GroupInput, IEntity
	{
		[Required]
		public int Id { get; set; }
	}
}
