using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class SpecialsViewModel:IValidatableObject
    {
        public Special Special { get; set; }
        public List<Special> SpecialsList { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (String.IsNullOrEmpty(Special.SpecialName)){
                errors.Add(new ValidationResult("Title cannot be left blank."));
            }

            if (String.IsNullOrEmpty(Special.SpecialDescription))
            {
                errors.Add(new ValidationResult("Description cannot be left blank."));
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

            return errors; 
        }
    }
}