using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.DependencyInjection;
// using System.Collections.Generic;

public class Customer : IValidatableObject
{
    [Key]
    public int customer_id { get; set; }

    public string? customer_name { get; set; }
    public string? customer_phone { get; set; }
    public string? customer_email { get; set; }
    public string? customer_password { get; set; }

    [NotMapped]
    public string? customer_repassword { get; set; }

    public string? customer_address { get; set; }
    public string? customer_city { get; set; }
    public string? customer_country { get; set; }
    public string? customer_pincode { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var httpContextAccessor = validationContext.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
        var requestPath = httpContextAccessor?.HttpContext?.Request.Path.Value;

        if (requestPath != null)
        {
            // Validation for Profile Update
            if (requestPath.Contains("ProfileEdit", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(customer_name))
                    yield return new ValidationResult("*", new[] { nameof(customer_name) });

                if (string.IsNullOrEmpty(customer_email))
                    yield return new ValidationResult("*", new[] { nameof(customer_email) });

                if (string.IsNullOrEmpty(customer_phone))
                    yield return new ValidationResult("*", new[] { nameof(customer_phone) });

                if (string.IsNullOrEmpty(customer_address))
                    yield return new ValidationResult("*", new[] { nameof(customer_address) });

                if (string.IsNullOrEmpty(customer_city))
                    yield return new ValidationResult("*", new[] { nameof(customer_city) });

                if (string.IsNullOrEmpty(customer_country))
                    yield return new ValidationResult("*", new[] { nameof(customer_country) });

                if (string.IsNullOrEmpty(customer_pincode))
                    yield return new ValidationResult("*", new[] { nameof(customer_pincode) });
            }

            // Validation for Checkout
            if (requestPath.Contains("Checkout", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(customer_name))
                    yield return new ValidationResult("*", new[] { nameof(customer_name) });

                if (string.IsNullOrEmpty(customer_email))
                    yield return new ValidationResult("*", new[] { nameof(customer_email) });

                if (string.IsNullOrEmpty(customer_phone))
                    yield return new ValidationResult("*", new[] { nameof(customer_phone) });

                if (string.IsNullOrEmpty(customer_address))
                    yield return new ValidationResult("*", new[] { nameof(customer_address) });

                if (string.IsNullOrEmpty(customer_city))
                    yield return new ValidationResult("*", new[] { nameof(customer_city) });

                if (string.IsNullOrEmpty(customer_country))
                    yield return new ValidationResult("*", new[] { nameof(customer_country) });

                if (string.IsNullOrEmpty(customer_pincode))
                    yield return new ValidationResult("*", new[] { nameof(customer_pincode) });
            }
        }
    }
}
