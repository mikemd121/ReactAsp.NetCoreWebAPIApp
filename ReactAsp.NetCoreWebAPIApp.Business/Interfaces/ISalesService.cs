using ReactAsp.NetCoreWebAPIApp.Core.Common;
using ReactAsp.NetCoreWebAPIApp.Model.Property;
using ReactAsp.NetCoreWebAPIApp.Model.SalesViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactAsp.NetCoreWebAPIApp.Business.Interfaces
{
    public interface ISalesService
    {
        ResponseModel SellProperty(SalesModel propertyModel);

        List<PropertyModel> GetSoldPropertyList();
    }
}
