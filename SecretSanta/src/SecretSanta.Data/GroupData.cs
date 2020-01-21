using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class GroupData
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
#nullable disable // Properties are initialize and checked in the set method.
        private User _User;

        public User User
        {
            get => _User;
            set => _User = value ?? throw new ArgumentNullException(nameof(value));
        }
        private Group _Group;

        public Group Group
        {
            get => _Group;
            set => _Group = value ?? throw new ArgumentNullException(nameof(value));
        }
#nullable enable
    }
}
