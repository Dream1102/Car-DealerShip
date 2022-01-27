using GuildCars.Data.Interfaces.VehicleTables;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class ExteriorColorRepositoryQA : IExteriorColorRepository
    {
        private List<ExteriorColor> _exteriorColors = new List<ExteriorColor>();
        
        public ExteriorColorRepositoryQA()
        {
            SeedExteriorColorRepository();
        }

        private void SeedExteriorColorRepository()
        {
            ExteriorColor color1 = new ExteriorColor();
            color1.ExteriorColorId = 1;
            color1.ExteriorColorName = "Black";
            _exteriorColors.Add(color1);

            ExteriorColor color2 = new ExteriorColor();
            color2.ExteriorColorId = 2;
            color2.ExteriorColorName = "Guild Red";
            _exteriorColors.Add(color2);

            ExteriorColor color3 = new ExteriorColor();
            color3.ExteriorColorId = 3;
            color3.ExteriorColorName = "Guild Gray";
            _exteriorColors.Add(color3);

            ExteriorColor color4 = new ExteriorColor();
            color4.ExteriorColorId = 4;
            color4.ExteriorColorName = "Blue";
            _exteriorColors.Add(color4);

            ExteriorColor color5 = new ExteriorColor();
            color5.ExteriorColorId = 5;
            color5.ExteriorColorName = "Silver";
            _exteriorColors.Add(color5);
        }

        public List<ExteriorColor> GetExteriorColors()
        {
            List<ExteriorColor> exteriorColors = new List<ExteriorColor>();

            foreach (var color in _exteriorColors)
            {
                exteriorColors.Add(color);
            }

            return exteriorColors;
        }
    }
}
