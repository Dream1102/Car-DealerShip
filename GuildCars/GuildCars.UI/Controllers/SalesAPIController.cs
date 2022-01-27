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
    public class SalesAPIController : ApiController
    {
        [Route("api/Sales/Index/")]
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
    }
}
