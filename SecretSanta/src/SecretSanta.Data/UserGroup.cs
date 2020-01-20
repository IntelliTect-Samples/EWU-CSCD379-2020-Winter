using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
	public class UserGroup
	{
		public int UserId { get; set; }
		public User User { get => _User; set => _User = value ?? throw new ArgumentNullException(nameof(User)); }
		private User _User = new User();
		public int GroupId { get; set; }
		public Group Group { get => _Group; set => _Group = value ?? throw new ArgumentNullException(nameof(Group)); }
		private Group _Group = new Group();

	}
}
