using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MovieRentalProject.ViewModels
{
    //public class ExternalLoginConfirmationViewModel
    //{
    //    [Required]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }
    //    [Required]
    //    [Display(Name = "Drivers Licence Number")]
    //    public string DrivingLicense { get; set; }
    //    [Required]
    //    [Display(Name = "Phone Number")]
    //    [StringLength(50)]
    //    public string Phone { get; set; }
    //}
    //public class RegisterViewModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }

    //    [Required]
    //    [Display(Name = "Drivers Licence Number")]
    //    public string DrivingLicence { get; set; }

    //    [Required]
    //    [Display(Name = "Phone Number")]
    //    [StringLength(50)]
    //    public string Phone { get; set; }
    //}
}