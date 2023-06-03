using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace UST_Platform.Models
{
    public class RegistrationViewModel
    {
        public enum Role // Define an enum for roles
        {
            Student,
            Teacher,
        }

        public enum Group // Define an enum for groups
        {
            MI31,
            MI32,
        }

        public IEnumerable<SelectListItem> Roles // Define a property that returns a list of select list items for roles
        {
            get
            {
                // Use LINQ to convert the enum values to select list items
                return Enum.GetValues(typeof(Role))
                           .Cast<Role>()
                           .Select(r => new SelectListItem
                           {
                               Value = r.ToString(),
                               Text = r.ToString(),
                           });
            }
        }

        public IEnumerable<SelectListItem> Groups // Define a property that returns a list of select list items for groups
        {
            get
            {
                // Use LINQ to convert the enum values to select list items
                return Enum.GetValues(typeof(Group))
                           .Cast<Group>()
                           .Select(g => new SelectListItem
                           {
                               Value = g.ToString(),
                               Text = g.ToString(),
                           });
            }
        }

        // Properties for the form fields
        [Required] // Add a validation attribute to make the field required
        [Display(Name = "First Name")] // Add a display attribute to specify the label name
        [StringLength(24, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed for first name")]
        public string FirstName { get; set; } = string.Empty; // Define a property with getter and setter

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(24, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed for last name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Middle Name")]
        [StringLength(24, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed for middle name")]
        public string MiddleName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Role")]
        public Role SelectedRole { get; set; }

        [Required]
        [DataType(DataType.Password)] // Add a data type attribute to specify the input type
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")] // Add a compare attribute to validate the password confirmation
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Choose group from the list or check teacher's key")]
        [Display(Name = "Group")]
        public Group SelectedGroup { get; set; }
    }
}