using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]\\w{3,14}$",ErrorMessage ="Password must have 1 Uppercase,1 Lowercase,1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }
    }
}