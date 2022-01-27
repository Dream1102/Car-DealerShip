using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class ContactRequestAddViewModel :IValidatableObject
    {
        public int ContactRequestId { get; set; }
        public string VehicleId { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMessage { get; set; }
        public DateTime DateContactRequestCreated { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(ContactName))
            {
                errors.Add(new ValidationResult("Customer Name is required."));
            }

            if(string.IsNullOrEmpty(ContactPhone) && string.IsNullOrEmpty(ContactEmail))
            {
                errors.Add(new ValidationResult("You must provide a phone number OR an email address."));
            }

            if (string.IsNullOrEmpty(ContactMessage))
            {
                errors.Add(new ValidationResult("A message is required."));
            }

            return errors;
        }
    }
}