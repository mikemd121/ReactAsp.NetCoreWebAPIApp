using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactAsp.NetCoreWebAPIApp.Model.Property
{
  public  class PropertyModel
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public int? PropertyId { get; set; }
        public string Name { get; set; }
        public string No { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public string Type { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
  
        public String BuyerName { get; set; }

        public String BuyerAddress { get; set; }

        public DateTime? CreatedDateTime { get; set; }

    }
}
