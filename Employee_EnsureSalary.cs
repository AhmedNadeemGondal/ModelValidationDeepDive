using System.ComponentModel.DataAnnotations;

namespace ModelValidationDeepDive
{
    public class Employee_EnsureSalary : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var employee = validationContext.ObjectInstance as Employee;

            if (employee is not null &&
                !string.IsNullOrWhiteSpace(employee.Position) &&
                employee.Position.Equals("Manager", StringComparison.OrdinalIgnoreCase))
            {
                if (employee.Salary < 100000)
                {
                    return new ValidationResult("Manager's salaray should be >=100000");
                }
            }
            return ValidationResult.Success;
        }
    }
}
