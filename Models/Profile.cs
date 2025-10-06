using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TyMiniProject.Models
{
    public class Profile
    {
        // Primary Key (Same as Identity User)
        [Key]
        [Required]
        public string UserId { get; set; }  // Foreign key & primary key (1:1 with User)

        [ValidateNever]
        public Users User { get; set; }

        // --- Personal Information ---

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "PRN is required")]
        [StringLength(10, ErrorMessage = "PRN cannot exceed 20 characters")]
        [Display(Name = "PRN Number")]
        public string PRN { get; set; }

        [Required(ErrorMessage = "Branch is required")]
        [Display(Name = "Branch")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Admission year is required")]
        [Display(Name = "Admission Year")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Admission year must be 4 digits (e.g. 2022)")]
        public string AdmissionYear { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of Birth")]
        public DateOnly Dob { get; set; }

        [Required(ErrorMessage = "Caste is required")]
        public string Caste { get; set; }

        [Required(ErrorMessage = "Fee category is required")]
        [Display(Name = "Fee Category")]
        public string FeeCategory { get; set; }

        // --- Image Handling ---
        [NotMapped]
        [Display(Name = "Upload Photo")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Profile Image")]
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";

        // --- Relationship: One Profile to Many FeeRecords ---
        [ValidateNever]
        public ICollection<FeeTable>? FeeTables { get; set; }
    }
}
