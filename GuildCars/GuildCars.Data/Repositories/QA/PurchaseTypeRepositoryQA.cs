using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class PurchaseTypeRepositoryQA : IPurchaseTypeRepository
    {
        private List<PurchaseType> _purchaseTypes = new List<PurchaseType>();

        public PurchaseTypeRepositoryQA()
        {
            SeedPurchaseTypes();
        }

        private void SeedPurchaseTypes()
        {
            PurchaseType purchaseType1 = new PurchaseType();
            purchaseType1.PurchaseTypeId = 1;
            purchaseType1.PurchaseTypeName = "Bank Finance";
            _purchaseTypes.Add(purchaseType1);

            PurchaseType purchaseType2 = new PurchaseType();
            purchaseType2.PurchaseTypeId = 2;
            purchaseType2.PurchaseTypeName = "Cash";
            _purchaseTypes.Add(purchaseType2);

            PurchaseType purchaseType3 = new PurchaseType();
            purchaseType3.PurchaseTypeId = 3;
            purchaseType3.PurchaseTypeName = "Dealer Finance";
            _purchaseTypes.Add(purchaseType3);
        }

        public List<PurchaseType> GetAllPurchaseTypes()
        {
            List<PurchaseType> purchaseTypes = new List<PurchaseType>();

            foreach (var type in _purchaseTypes)
            {
                purchaseTypes.Add(type);
            }
            return purchaseTypes;
        }
    }
}
