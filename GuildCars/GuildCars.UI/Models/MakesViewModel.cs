using GuildCars.Data.RepositoryFactories;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class MakesViewModel: IValidatableObject
    {
        public Make Make { get; set; }
        public List<Make> Makes { get; set; }

        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            List<ValidationResult> errors = new List<ValidationResult>();
            var makeExists = CheckMakeList(Make.MakeName); 

            if (String.IsNullOrEmpty(Make.MakeName))
            {
                errors.Add(new ValidationResult("Make cannot be blank."));
            }
            if (makeExists)
            {
                errors.Add(new ValidationResult("Make already exists."));
            }
            
            return errors;
        }

        private bool CheckMakeList(string makeName)
        {
            var repo = MakeRepositoryFactory.GetRepository();
            var allMakes = repo.GetAllVehicleMakes();

            foreach(var make in allMakes)
            {
                if(make.MakeName == makeName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}