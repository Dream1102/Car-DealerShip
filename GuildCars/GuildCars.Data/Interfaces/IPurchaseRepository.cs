using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IPurchaseRepository
    {
        List<SalesReport> GetSalesReport(SalesReportSearchParameters parameters);
        void Insert(Purchase purchase);
        List<Purchase> GetPurchases();
    }
}
