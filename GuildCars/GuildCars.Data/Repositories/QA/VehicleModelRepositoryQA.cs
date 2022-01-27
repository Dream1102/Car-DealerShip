using GuildCars.Data.Interfaces.VehicleTables;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class VehicleModelRepositoryQA : IModelRepository
    {
        static DateTime date = new DateTime(2021, 11, 29);

        private List<VehicleModel> _models = new List<VehicleModel>();

        public VehicleModelRepositoryQA()
        {
            SeedModels();
        }

        private void SeedModels()
        {
            VehicleModel model = new VehicleModel();
            model.ModelId = 1;
            model.Make.MakeId = 1;
            model.Make.MakeName = "Ford";
            model.ModelName = "Edge";
            model.UserEmail = "jamie@guildcars.com";
            model.DateModelCreated = date;
            _models.Add(model);

            VehicleModel model1 = new VehicleModel();
            model1.ModelId = 2;
            model1.Make.MakeId = 1;
            model1.Make.MakeName = "Ford";
            model1.ModelName = "Explorer";
            model1.UserEmail = "jamie@guildcars.com";
            model1.DateModelCreated = date;
            _models.Add(model1);

            VehicleModel model3 = new VehicleModel();
            model3.ModelId = 3;
            model3.Make.MakeId = 2;
            model3.Make.MakeName = "GMC";
            model3.ModelName = "Acadia";
            model3.UserEmail = "jamie@guildcars.com";
            model3.DateModelCreated = date;
            _models.Add(model3);

            VehicleModel model4 = new VehicleModel();
            model4.ModelId = 4;
            model4.Make.MakeId = 2;
            model4.Make.MakeName = "GMC";
            model4.ModelName = "Terrain";
            model4.UserEmail = "jamie@guildcars.com";
            model4.DateModelCreated = date;
            _models.Add(model4);

            VehicleModel model5 = new VehicleModel();
            model5.ModelId = 5;
            model5.Make.MakeId = 3;
            model5.Make.MakeName = "Toyota";
            model5.ModelName = "ForeRunner";
            model5.UserEmail = "jamie@guildcars.com";
            model5.DateModelCreated = date;
            _models.Add(model5);

            VehicleModel model6 = new VehicleModel();
            model6.ModelId = 6;
            model6.Make.MakeId = 3;
            model6.Make.MakeName = "Toyota";
            model6.ModelName = "Highlander";
            model6.UserEmail = "jamie@guildcars.com";
            model6.DateModelCreated = date;
            _models.Add(model6);

            VehicleModel model7 = new VehicleModel();
            model7.ModelId = 7;
            model7.Make.MakeId = 4;
            model7.Make.MakeName = "Kia";
            model7.ModelName = "Sorento";
            model7.UserEmail = "jamie@guildcars.com";
            model7.DateModelCreated = date;
            _models.Add(model7);

            VehicleModel model8 = new VehicleModel();
            model8.ModelId = 8;
            model8.Make.MakeId = 4;
            model8.Make.MakeName = "Kia";
            model8.ModelName = "Telluride";
            model8.UserEmail = "jamie@guildcars.com";
            model8.DateModelCreated = date;
            _models.Add(model8);

            VehicleModel model9 = new VehicleModel();
            model9.ModelId = 9;
            model9.Make.MakeId = 5;
            model9.Make.MakeName = "Dodge";
            model9.ModelName = "Challenger";
            model9.UserEmail = "jamie@guildcars.com";
            model9.DateModelCreated = date;
            _models.Add(model9);

            VehicleModel model10 = new VehicleModel();
            model10.ModelId = 10;
            model10.Make.MakeId = 5;
            model10.Make.MakeName = "Dodge";
            model10.ModelName = "Durango";
            model10.UserEmail = "jamie@guildcars.com";
            model10.DateModelCreated = date;
            _models.Add(model10);

            VehicleModel model11 = new VehicleModel();
            model11.ModelId = 11;
            model11.Make.MakeId = 5;
            model11.Make.MakeName = "Dodge";
            model11.ModelName = "Gran Caravan";
            model11.UserEmail = "jamie@guildcars.com";
            model11.DateModelCreated = date;
            _models.Add(model11);
        }
            

        public List<VehicleModel> GetModels()
        {
            List<VehicleModel> models = new List<VehicleModel>();

            foreach (var model in _models)
            {
                models.Add(model);
            }

            return models;
        }

        public void Insert(VehicleModel model)
        {
            _models.Add(model);
        }
    }
}
