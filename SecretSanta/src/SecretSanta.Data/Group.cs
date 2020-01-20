using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
	public class Group : FingerPrintEntityBase
	{
		public string Name { get => _Name; set => _Name = value ?? throw new ArgumentNullException(nameof(Name)); }
		private string _Name = string.Empty;
		public List<UserGroup> UserGroups { get => _UserGroups; set => _UserGroups = value ?? throw new ArgumentNullException(nameof(UserGroups)); }
		private List<UserGroup> _UserGroups = new List<UserGroup>();
	}
}
