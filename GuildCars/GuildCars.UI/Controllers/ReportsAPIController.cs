using GuildCars.Data.RepositoryFactories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class ReportsAPIController : ApiController
    {
        [Route("api/Reports/Sales/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSalesReport(string userEmail, DateTime? fromDate, DateTime? toDate)
        {
            if (string.IsNullOrEmpty(fromDate.ToString()))
            {
                fromDate = DateTime.MinValue;
            }
            if (string.IsNullOrEmpty(toDate.ToString()))
            {
                toDate = DateTime.Now;
            }

            var repo = PurchaseRepositoryFactory.GetRepository();
            try
            {

                var parameters = new SalesReportSearchParameters()
                {
                    UserEmail = userEmail,
                    FromDate = Convert.ToDateTime(fromDate),
                    ToDate = Convert.ToDateTime(toDate)
                };
                var salesReport = repo.GetSalesReport(parameters);
                return Ok(salesReport);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
