using GuildCars.Data.Interfaces.VehicleTables;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class BodyStyleRepositoryQA : IBodyStyleRepository
    {
        private List<BodyStyle> _bodyStyles = new List<BodyStyle>();

        public BodyStyleRepositoryQA()
        {
            SeedBodyStyles();
        }

        private void SeedBodyStyles()
        {
            BodyStyle bodyStyle1 = new BodyStyle();
            bodyStyle1.BodyStyleId = 1;
            bodyStyle1.BodyStyleName = "Car";
            _bodyStyles.Add(bodyStyle1);

            BodyStyle bodyStyle2 = new BodyStyle();
            bodyStyle2.BodyStyleId = 2;
            bodyStyle2.BodyStyleName = "SUV";
            _bodyStyles.Add(bodyStyle2);

            BodyStyle bodyStyle3 = new BodyStyle();
            bodyStyle3.BodyStyleId = 3;
            bodyStyle3.BodyStyleName = "Truck";
            _bodyStyles.Add(bodyStyle3);

            BodyStyle bodyStyle4 = new BodyStyle();
            bodyStyle4.BodyStyleId = 4;
            bodyStyle4.BodyStyleName = "Van";
            _bodyStyles.Add(bodyStyle4);
        }

            public List<BodyStyle> GetBodyStyles()
        {
            List<BodyStyle> bodystyles = new List<BodyStyle>();

            foreach(var style in _bodyStyles)
            {
                bodystyles.Add(style);
            }

            return bodystyles;
        }
    }
}
