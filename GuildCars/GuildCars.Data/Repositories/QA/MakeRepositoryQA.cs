using GuildCars.Data.Interfaces.VehicleTables;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class MakeRepositoryQA : IMakeRepository
    {
        private List<Make> _makes = new List<Make>();

        public MakeRepositoryQA()
        {
            SeedMakeRepository();
        }

        private void SeedMakeRepository()
        {
            Make make1 = new Make();
            make1.MakeId = 1;
            make1.MakeName = "Ford";
            make1.UserEmail = "jamie@guildcars.com";
            make1.DateMakeCreated = DateTime.Now;
            _makes.Add(make1);

            Make make2 = new Make();
            make2.MakeId = 2;
            make2.MakeName = "GMC";
            make2.UserEmail = "jamie@guildcars.com";
            make2.DateMakeCreated = DateTime.Now;
            _makes.Add(make2);

            Make make3 = new Make();
            make3.MakeId = 3;
            make3.MakeName = "Toyota";
            make3.UserEmail = "jamie@guildcars.com";
            make3.DateMakeCreated = DateTime.Now;
            _makes.Add(make3);

            Make make4 = new Make();
            make4.MakeId = 4;
            make4.MakeName = "Kia";
            make4.UserEmail = "jamie@guildcars.com";
            make4.DateMakeCreated = DateTime.Now;
            _makes.Add(make4);

            Make make5 = new Make();
            make5.MakeId = 5;
            make5.MakeName = "Dodge";
            make5.UserEmail = "jamie@guildcars.com";
            make5.DateMakeCreated = DateTime.Now;
            _makes.Add(make5);
        }
        public List<Make> GetAllVehicleMakes()
        {
            List<Make> makes = new List<Make>();

            foreach (var make in _makes)
            {
                makes.Add(make);
            }
            return makes;
        }

        public void InsertVehicleMake(Make make)
        {
            _makes.Add(make);
        }
    }
}
