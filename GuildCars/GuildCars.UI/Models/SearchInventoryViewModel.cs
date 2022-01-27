using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class SearchInventoryViewModel
    {
        public List<SelectListItem> Prices { get; set; }
        public List<SelectListItem> Years { get; set; }
        public string SearchParameter { get; set; }
        public bool IsUsed { get; set; }
        public bool IsSold { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        
    }
}