﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class ContactRequest
    {
        public int ContactRequestId { get; set; }
        public string  VehicleId { get; set; }
        public string  ContactName { get; set; }
        public string  ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMessage { get; set; }
        public DateTime DateContactRequestCreated { get; set; }
    }
}
