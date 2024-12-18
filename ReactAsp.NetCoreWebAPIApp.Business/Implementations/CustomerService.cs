using AutoMapper;
using ReactAsp.NetCoreWebAPIApp.Business.Interfaces;
using ReactAsp.NetCoreWebAPIApp.Core.Common;
using ReactAsp.NetCoreWebAPIApp.Data.EntityModels;
using ReactAsp.NetCoreWebAPIApp.Model.Customer;
using ReactAsp.NetCoreWebAPIApp.Repository.BaseRepository.Interfaces;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;

namespace ReactAsp.NetCoreWebAPIApp.Business.Implementations
{
    /// <summary>
    /// Customer service.
    /// </summary>
    public class CustomerService : ICustomerService
    {

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper mapper;

        private IGenericRepository<Customer> _genericRepository;


        /// <summary>
        /// The unit of work
        /// </summary>
      //   private readonly IUnitOfWork unitOfWork;

        public CustomerService(
            //IUnitOfWork unitOfWork,
            IMapper mapper,
            IGenericRepository<Customer> genericRepository)
        {
            this.mapper = mapper;
          //  this.unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        /// <summary>
        /// Registers the customer.
        /// </summary>
        /// <param name="customerModel">The customer model.</param>
        /// <returns></returns>
        public ResponseModel RegisterCustomer(CustomerModel customerModel)
        {

            var model = new ResponseModel();
            var _temp = IsCustomerRegistered(customerModel.Email);
            if (_temp)
            {
                model.Messsage = "Customer already registered.";
                model.IsSuccess = false;
            }
            else
            {
                var customer = mapper.Map<Customer>(customerModel);
                _genericRepository.Insert(customer);
                model.IsSuccess = true;
                model.Messsage = "Customer registration completed.";
            }
            _genericRepository.Save();

            return model;
        }


        /// <summary>
        /// Determines whether [is vehicle registered] [the specified identifier].
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is vehicle registered] [the specified identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCustomerRegistered(string Email)
        {
            var isExist = _genericRepository.Get(x => x.Email.Contains(Email)).Any();
            if (isExist)
                return true;
            return false;
        }


        /// <summary>
        /// Gets the customer list.
        /// </summary>
        /// <returns></returns>
        public List<CustomerId> GetCustomerList()
        {
            return _genericRepository.GetAll().Select(v => new CustomerId
            {
                Id = (int)v.CustomerId,
                Name = v.Name,
                Address = v.Address,
                Email = v.Email
            }).ToList();
        }


        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <returns></returns>
        public bool UpdateCustomer(CustomerModel customerModel)
        {
            var existingUser = _genericRepository.Get(x => x.Email==customerModel.Email).FirstOrDefault();
            if (existingUser == null)
                return false;
            existingUser.Name = customerModel.Name;
            existingUser.Address = customerModel.Address;
            _genericRepository.Update(existingUser);
            _genericRepository.Save();
            return true;
        }



        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <returns></returns>
        public bool DeleteCustomer(string customerId)
        {

            // Validate if customerId can be converted to an integer
            if (!int.TryParse(customerId, out int custId))
                return false;

            // Check if the customer exists
            var customer = _genericRepository.GetByID(custId);
            if (customer == null)
                return false;
 
            _genericRepository.Delete(custId);
            _genericRepository.Save();
            return true;
        }
    }
}
