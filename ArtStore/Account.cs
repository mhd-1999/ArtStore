using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArtStore.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Account : IdentityUser
    {
        [Required(ErrorMessage = "Please enter name!"), StringLength(50, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 5)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter address!"), StringLength(50, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 5)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter card!"), StringLength(12, ErrorMessage = "Please enter {0} characters")]
        public string NumberCard { get; set; }

        [Required(ErrorMessage = "Please enter cvv!"), StringLength(4, ErrorMessage = "Please enter {0} characters")]
        public string Cvv { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Art> Arts { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Account> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}