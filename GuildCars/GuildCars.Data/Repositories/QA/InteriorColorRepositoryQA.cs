using GuildCars.Data.Interfaces.VehicleTables;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class InteriorColorRepositoryQA : IInteriorColorRepository
    {
        private List<InteriorColor> _interiorColors = new List<InteriorColor>();

        public InteriorColorRepositoryQA()
        {
            SeedInteriorColorRepository();
        }

        private void SeedInteriorColorRepository()
        {
            InteriorColor color1 = new InteriorColor();
            color1.InteriorColorId = 1;
            color1.InteriorColorName = "Black";
            _interiorColors.Add(color1);

            InteriorColor color2 = new InteriorColor();
            color2.InteriorColorId = 2;
            color2.InteriorColorName = "Guild Red";
            _interiorColors.Add(color2);

            InteriorColor color3 = new InteriorColor();
            color3.InteriorColorId = 3;
            color3.InteriorColorName = "Guild Gray";
            _interiorColors.Add(color3);

            InteriorColor color4 = new InteriorColor();
            color4.InteriorColorId = 4;
            color4.InteriorColorName = "Blue";
            _interiorColors.Add(color4);

            InteriorColor color5 = new InteriorColor();
            color5.InteriorColorId = 5;
            color5.InteriorColorName = "Yellow";
            _interiorColors.Add(color5);
        }

        public List<InteriorColor> GetInteriorColors()
        {
            List<InteriorColor> interiorColors = new List<InteriorColor>();

            foreach (var color in _interiorColors)
            {
                interiorColors.Add(color);
            }
            return interiorColors;
        }
    }
}
