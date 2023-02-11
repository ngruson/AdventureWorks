using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Store.ViewModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? CustomerNumber { get; set; }
    }
}