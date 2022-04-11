using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models
{
    public class Addon
    { 
        public List<string> addons { get; set; }
        public string addonSection { get; set; }
        public bool allowMultiple { get; set; }
    }
}
