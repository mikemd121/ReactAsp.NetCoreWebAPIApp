using ReactAsp.NetCoreWebAPIApp.Core.Common;
using ReactAsp.NetCoreWebAPIApp.Data.EntityModels;
using ReactAsp.NetCoreWebAPIApp.Model.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactAsp.NetCoreWebAPIApp.Business.Interfaces
{
   public interface IPropertyService
    {
        ResponseModel RegisterProperty(PropertyModel propertyModel);

        List<Property> GetAvailablePropertyByCustomerId(int customerId);

        List<PropertyModel> GetPropertyList();
    }
}
