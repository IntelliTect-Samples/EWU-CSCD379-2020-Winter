using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Business.Dto
{
    public class AuthorInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
