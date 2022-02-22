using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class SpecialsRepositoryQA : ISpecialsRepository
    {
        private List<Special> _specials = new List<Special>();

        public SpecialsRepositoryQA()
        {
            SeedSpecials();
        }

        private void SeedSpecials()
        {
            Special special1 = new Special();
            special1.SpecialId = 1;
            special1.SpecialName = "Free Tires For Life!";
            special1.SpecialDescription = "You can earn free tires for the life of your vehicle if you purchase today.";
            special1.SpecialImageFilename = "freeTires.jpg";
            special1.DateSpecialCreated = DateTime.Now;
            special1.UserEmail = "jamie@guildcars.com";

            _specials.Add(special1);

            Special special2 = new Special();
            special2.SpecialId = 2;
            special2.SpecialName = "Buy this vehicle!";
            special2.SpecialDescription = "Seriously, we need to sell it. Make us an offer!";
            special2.SpecialImageFilename = "inventory-2C4RDGDG4GR321504.jpg";
            special2.DateSpecialCreated = DateTime.Now;
            special2.UserEmail = "jamie@guildcars.com";

            _specials.Add(special2);

        }

        public void DeleteSpecial(int specialId)
        {
            if(_specials[specialId] != null)
            {
                _specials.RemoveAt(specialId);
            }
            else
            {
                _specials.RemoveAt(specialId);
            }
        }

        public List<Special> GetAllSpecials()
        {
            List<Special> specials = new List<Special>();

            foreach (var special in _specials)
            {
                specials.Add(special);
            }

            return specials;
        }

        public void InsertSpecial(Special specialItem)
        {
            _specials.Add(specialItem);
        }
    }
}
