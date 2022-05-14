using ReactAsp.NetCoreWebAPIApp.Core.Common;
using ReactAsp.NetCoreWebAPIApp.Data.EntityModels;
using ReactAsp.NetCoreWebAPIApp.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactAsp.NetCoreWebAPIApp.Business.Interfaces
{
   public interface ICustomerService
    {
        ResponseModel RegisterCustomer(CustomerModel customerModel);

        List<CustomerId> GetCustomerList();


        bool UpdateCustomer(CustomerModel customerModel);

        bool DeleteCustomer(string customerId);
    }
}
