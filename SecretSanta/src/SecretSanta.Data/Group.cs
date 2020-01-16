﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
		private string _Name = string.Empty;

		public string Name
		{
			get { return _Name; }
			set
			{
				_Name = value ?? throw new ArgumentNullException(nameof(Name));

			}
		}

		public ICollection<Link> Links { get; }

	}
}
