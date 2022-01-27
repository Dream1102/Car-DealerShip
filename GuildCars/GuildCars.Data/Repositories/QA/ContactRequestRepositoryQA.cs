using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class ContactRequestRepositoryQA : IContactRequestRepository
    {
        private List<ContactRequest> _contactRequests = new List<ContactRequest>();

        public ContactRequestRepositoryQA()
        {
            SeedContactRequests();
        }

        private void SeedContactRequests()
        {
            ContactRequest request1 = new ContactRequest();
            request1.ContactRequestId = 1;
            request1.VehicleId = "1FMEE5BH1MLA96289";
            request1.ContactName = "Bob Bilby";
            request1.ContactPhone = "502-555-5555";
            request1.ContactMessage = "I would like to buy this car!";
            request1.DateContactRequestCreated = DateTime.Now;
            _contactRequests.Add(request1);

            ContactRequest request2 = new ContactRequest();
            request2.ContactRequestId = 2;
            request2.VehicleId = "2C4RDGDG4GR321504";
            request2.ContactName = "Burt McHandsome";
            request2.ContactEmail = "bert@mchandsome.com";
            request2.ContactMessage = "I will gladly pay you Tuesday for this car today!";
            request2.DateContactRequestCreated = DateTime.Now;
            _contactRequests.Add(request2);
        }

        public List<ContactRequest> GetAll()
        {
            List<ContactRequest> contactRequests = new List<ContactRequest>();

            foreach (var style in _contactRequests)
            {
                contactRequests.Add(style);
            }
            return contactRequests;
        }

        public void Insert(ContactRequest contactRequest)
        {
            _contactRequests.Add(contactRequest);
        }
    }
}
