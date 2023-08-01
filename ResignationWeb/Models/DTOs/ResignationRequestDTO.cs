using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ResignationWeb.Models.DTOs
{
    public class ResignationRequestDTO
    {
        [Required(ErrorMessage = "Resignation Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Resignation Date")]
        [CustomValidation(typeof(ResignationRequestDTO), "ValidateResignationDate")]

        public DateTime ResignationDate { get; set; }

        [Required(ErrorMessage = "Reason is required.")]
        public string? Reason { get; set; }
        public string? Comments { get; set; }

        public static ValidationResult ValidateResignationDate(DateTime date, ValidationContext context)
        {
            if (date <= DateTime.Now)
            {
                return new ValidationResult("Resignation Date must be greater than the current date.", new[] { context.MemberName! });
            }

            return ValidationResult.Success!;
        }
    }
}
