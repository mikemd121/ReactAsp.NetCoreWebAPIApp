using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ReactAsp.NetCoreWebAPIApp.Model.Property
{
    public class PropertyViewModel : PropertyModel
    {
        public IEnumerable<SelectListItem> customerList { get; set; }

    }

}
