using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TyMiniProject.Models
{
    public class FeeTable
    {
        [Key]
        public int FeeId { get; set; }

        // --- Student Info ---
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        // --- Fee Details ---

        [Required(ErrorMessage = "Total amount is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Total amount must be a positive number")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Paid amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number")]
        [Display(Name = "Amount Paid")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Due date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Fee category is required")]
        [StringLength(50, ErrorMessage = "Fee category cannot exceed 50 characters")]
        [Display(Name = "Fee Category")]
        public string FeeCategory { get; set; }

        // --- Foreign Key ---
        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        // --- Navigation ---
        [ValidateNever]
        public Profile Profile { get; set; }
    }
}
