using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TyMiniProject.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
        [ValidateNever]
        public Profile? Profile { get; set; }
    }
}
