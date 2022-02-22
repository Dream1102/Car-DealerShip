using GuildCars.Data.RepositoryFactories;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class ModelsViewModel:IValidatableObject
    {
        public VehicleModel VehicleModel { get; set; }
        public List<Make> Makes { get; set; }
        public List<VehicleModel> Models { get; set; }
        public IEnumerable<SelectListItem> MakesSelectList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var modelExists = CheckModelList(VehicleModel.ModelName);

            if (String.IsNullOrEmpty(VehicleModel.ModelName))
            {
                errors.Add(new ValidationResult("New Model field cannot be left blank."));
            }
            if (modelExists)
            {
                errors.Add(new ValidationResult("Model already exists."));
            }

            return errors;
        }

        private bool CheckModelList(string modelName)
        {
            var repo = ModelRepositoryFactory.GetRepository();
            var allModels = repo.GetAllVehicleModels();

            foreach (var model in allModels)
            {
                if (modelName == model.ModelName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}