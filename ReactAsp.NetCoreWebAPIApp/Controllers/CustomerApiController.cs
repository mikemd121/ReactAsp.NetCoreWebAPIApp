using ReactAsp.NetCoreWebAPIApp.Business.Interfaces;
using ReactAsp.NetCoreWebAPIApp.Model.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace ReactAsp.NetCoreWebAPIApp.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        /// <summary>
        /// The customer service/
        /// </summary>
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerApiController"/> class.
        /// </summary>
        /// <param name="customerService">The customer service.</param>
        public CustomerApiController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Registers the customer.
        /// </summary>
        /// <param name="customerModel">The customer model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-customer")]
        public IActionResult RegisterCustomer(CustomerModel customerModel)
        {
            var model = _customerService.RegisterCustomer(customerModel);
            return Ok(model);

        }

        /// <summary>
        /// Gets the customer ids.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("customer-ids")]
        [ResponseType(typeof(List<CustomerId>))]
        public IActionResult GetCustomerIds()
        {
            var model = _customerService.GetCustomerList();
            if (model == null)
                return BadRequest();
            return Ok(model);

        }


        /// <summary>
        /// Gets the customer ids.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("update-customer")]
        [ResponseType(typeof(List<CustomerId>))]
        public IActionResult UpdateCustomer(CustomerModel customerModel)
        {
            var result = _customerService.UpdateCustomer(customerModel);
            if (!result)
                return BadRequest();
            return Ok();
        }


        /// <summary>
        /// Gets the customer ids.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-customer/{id}")]
        [ResponseType(typeof(List<CustomerId>))]
        public IActionResult DeleteCustomer(string id)
        {
            var result = _customerService.DeleteCustomer(id);
            if (!result)
                return BadRequest();
            return Ok();
        }


    }
}
