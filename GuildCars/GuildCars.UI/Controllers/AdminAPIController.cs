using GuildCars.Data.RepositoryFactories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class AdminAPIController : ApiController
    {
        [Route("api/Admin/Vehicles/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetVehicles(string searchParameter, bool? isSold, decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var parameters = new SalesVehicleSearchParameters()
                {
                    SearchParameter = searchParameter,
                    IsSold = (bool)isSold,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear
                };

                var vehicles = repo.SalesVehicleSearch(parameters).Take(20).OrderByDescending(m => m.MSRP);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Admin/AddVehicle/{makeId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModelsSelectList(int makeId)
        {
            var modelsRepo = ModelRepositoryFactory.GetRepository();
            var modelList = modelsRepo.GetModels();

            try
            {
                List<VehicleModel> models = new List<VehicleModel>();
                foreach (var model in modelList)
                {
                    if (model.Make.MakeId == makeId)
                    {
                        models.Add(model);
                    }
                }

                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
