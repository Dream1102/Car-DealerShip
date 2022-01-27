using GuildCars.Data.RepositoryFactories;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class InventoryAPIController : ApiController
    {   
        [Route("api/Inventory/Used/")]
        [Route("api/Inventory/New/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetVehicles(string searchParameter, bool isUsed, bool? isSold, decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    SearchParameter = searchParameter,
                    IsUsed = isUsed,
                    IsSold = (bool)isSold,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear
                };

                var vehicles = repo.VehicleSearch(parameters).Take(20).OrderByDescending(m => m.MSRP);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
