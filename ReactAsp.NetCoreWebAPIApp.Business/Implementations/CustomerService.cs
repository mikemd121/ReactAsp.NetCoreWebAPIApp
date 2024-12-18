using AutoMapper;
using ReactAsp.NetCoreWebAPIApp.Business.Interfaces;
using ReactAsp.NetCoreWebAPIApp.Core.Common;
using ReactAsp.NetCoreWebAPIApp.Data.EntityModels;
using ReactAsp.NetCoreWebAPIApp.Model.Customer;
using ReactAsp.NetCoreWebAPIApp.Repository.BaseRepository;
using ReactAsp.NetCoreWebAPIApp.Repository.BaseRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactAsp.NetCoreWebAPIApp.Business.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReactAsp.NetCoreWebAPIApp.Business.Interfaces.ICustomerService" />
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
        private readonly IUnitOfWork unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IGenericRepository<Customer> genericRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
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
                //  unitOfWork.GetRepository<Customer>().Insert(customer); this method also can be used to insert.
                _genericRepository.Insert(customer);
                model.IsSuccess = true;
                model.Messsage = "Customer registration completed.";
            }
            // unitOfWork.SaveChanges(); this method also can be used to save.
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
            //  var isExist = unitOfWork.GetRepository<Customer>().Get(x => x.Email.Contains(Email)).Any(); this code also can be used
            var isExist= _genericRepository.Get(x => x.Email.Contains(Email)).Any();

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
            return unitOfWork.GetRepository<Customer>().Get().Select(v => new CustomerId
            {
                Id = (int)v.CustomerId,
                Name = v.Name,
                Address=v.Address,
                Email=v.Email
            }).ToList(); ;
        }


        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <returns></returns>
        public bool UpdateCustomer(CustomerModel customerModel)
        {
            var custId = Convert.ToInt32(customerModel.CustomerId);
            var customer = unitOfWork.GetRepository<Customer>().Get(x => x.CustomerId == custId).FirstOrDefault();
            customer.Name = customerModel.Name;
            customer.Address = customerModel.Address;
            customer.Email = customerModel.Email;
            unitOfWork.SaveChanges();
            return true;
        }



        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <returns></returns>
        public bool DeleteCustomer(string customerId)
        {
            var custId = Convert.ToInt32(customerId);
            var cus = unitOfWork.GetRepository<Customer>().Get(x => x.CustomerId == custId).FirstOrDefault();     
            unitOfWork.GetRepository<Customer>().Delete(custId);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}
