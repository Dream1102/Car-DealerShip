using GuildCars.Data.RepositoryFactories;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class AddVehicleViewModel: IValidatableObject
    {
        public Vehicle Vehicle { get; set; }
        public IEnumerable<SelectListItem> MakeList { get; set; }
        public IEnumerable<SelectListItem> IsUsed { get; set; }
        public IEnumerable<SelectListItem> BodyStyleList { get; set; }
        public IEnumerable<SelectListItem> TransmissionTypeList { get; set; }
        public IEnumerable<SelectListItem> ExteriorColorList { get; set; }
        public IEnumerable<SelectListItem> InteriorColorList { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var vehicleExists = CheckVehicleList(Vehicle.VehicleId);

            if (String.IsNullOrEmpty(Vehicle.Year.ToString()))
            {
                errors.Add(new ValidationResult("Year is a required field."));
            }
            else
            {
                if (Vehicle.Year.ToString().Length < 4)
                {
                    errors.Add(new ValidationResult("Year must be 4 digits in length."));
                }

                if (Vehicle.Year.ToString().Length == 4)
                {
                    var maxYear = DateTime.Now.AddYears(1).Year;
                    Regex regex = new Regex("^[0-9]+$");
                    if (!regex.IsMatch(Vehicle.Year.ToString()))
                    {
                        errors.Add(new ValidationResult("Year must be 4 DIGITS."));
                    }
                    if (Vehicle.Year < 2000 || Vehicle.Year > maxYear)
                    {
                        errors.Add(new ValidationResult("Year must be after 2000 and before" + maxYear + "."));
                    }
                }
            }

            if (Vehicle.IsUsed == false && Vehicle.Mileage > 1000)
            {
                errors.Add(new ValidationResult("New vehicles can have no more than 1000 miles."));
            }

            if (String.IsNullOrEmpty(Vehicle.VehicleId))
            {
                errors.Add(new ValidationResult("VIN # field cannot be blank."));
            }
            else
            {
                if (Vehicle.VehicleId.Length < 17)
                {
                    errors.Add(new ValidationResult("A Vehicle VIN # contains 17 characters."));
                }
            }
            

            if (Vehicle.ListedPrice < 0 || Vehicle.MSRP < 0)
            {
                errors.Add(new ValidationResult("Dollar amounts must be greater than zero."));
            }

            if (Vehicle.ListedPrice > 0 || Vehicle.MSRP > 0)
            {
                Regex regex = new Regex("^[0-9]+$");
                if (!regex.IsMatch(Vehicle.ListedPrice.ToString()) || !regex.IsMatch(Vehicle.MSRP.ToString()))
                {
                    errors.Add(new ValidationResult("Prices must be in DIGITS."));
                }
            }

            if (Vehicle.ListedPrice > Vehicle.MSRP)
            {
                errors.Add(new ValidationResult("Sale Price cannot exceed MSRP"));
            }

            if (String.IsNullOrEmpty(Vehicle.Description))
            {
                errors.Add(new ValidationResult("A Description is required."));
            }

            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".JPG", ".PNG", ".GIF", ".JPEG" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if (!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file must be a .jpg, .png, .gif, or .jpeg."));
                }
            }
            else
            {
                errors.Add(new ValidationResult("Image file is required."));
            }

            if (vehicleExists)
            {
                errors.Add(new ValidationResult("VIN Number already exists. Please check and try again."));
            }

            return errors;
        }

        private bool CheckVehicleList(string vehicleId)
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            var allVehicles = repo.GetAllVehicles();

            foreach (var vehicle in allVehicles)
            {
                if (vehicleId == vehicle.VehicleId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}