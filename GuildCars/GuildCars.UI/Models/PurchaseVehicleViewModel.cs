using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class PurchaseVehicleViewModel :IValidatableObject
    {
        public decimal PurchasePrice { get; set; }
        public string PurchaseType { get; set; }
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> PurchaseTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            //validate phone number or email address have been entered

            if (string.IsNullOrEmpty(Customer.CustomerPhone) && string.IsNullOrEmpty(Customer.CustomerEmail))
            {
                errors.Add(new ValidationResult("You must provide a phone number OR an email address."));
            }

            if (!string.IsNullOrEmpty(Customer.CustomerEmail)){
                var isValidEmail = IsValidEmail(Customer.CustomerEmail);
                if(isValidEmail == false)
                {
                    errors.Add(new ValidationResult("Email address provided is invalid."));
                }
            }

            //validate address 1 field is filled out
            if (string.IsNullOrEmpty(Customer.CustomerAddress1))
            {
                errors.Add(new ValidationResult("You must provide an address."));
            }

            //validate customer name is filled out
            if (string.IsNullOrEmpty(Customer.CustomerName))
            {
                errors.Add(new ValidationResult("You must provide a name."));
            }

            //validate customer city is filled out
            if (string.IsNullOrEmpty(Customer.City))
            {
                errors.Add(new ValidationResult("City is required."));
            }
            
            //validate zip code is filled out
            if (string.IsNullOrEmpty(Customer.Zip))
            {
                errors.Add(new ValidationResult("Zip code is required."));
            }
            else
            {
                //validate zip code meets length requirement
                if (Customer.Zip.Length < 5)
                {
                    errors.Add(new ValidationResult("Zip code requires 5 digits."));
                }
                //validate zip code contains digits
                if (Customer.Zip.Length == 5)
                {
                    Regex regex = new Regex("^[0-9]+$");
                    if (!regex.IsMatch(Customer.Zip))
                    {
                        errors.Add(new ValidationResult("Zip code requires 5 DIGITS."));
                    }
                }
            }

            if (string.IsNullOrEmpty(Customer.CustomerPhone))
            {
                errors.Add(new ValidationResult("Phone number is required."));
            }

            if(PurchasePrice <= Vehicle.MSRP)
            {
                if(PurchasePrice < Vehicle.ListedPrice * .95m)
                {
                    errors.Add(new ValidationResult("Purchase Price cannot be less than 95% of the listed price of the vehicle. "));
                }
            }
            
            if(PurchasePrice > Vehicle.MSRP)
            {
                errors.Add(new ValidationResult("Purchase Price cannot exceed MSRP"));
            }

            return errors;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, 
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch(RegexMatchTimeoutException)
            {
                return false;
            }
            catch(ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}