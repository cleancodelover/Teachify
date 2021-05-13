using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teachify.BLL.BusinessLogic.Interfaces;
using Teachify.DAL.DVM;
using Teachify.DAL.Entities;
using Teachify.DALS.Infrastructure.Interfaces;
using Teachify.DALS.Repositories;

namespace Teachify.BLL.BusinessLogic
{
    public class CustomerBL : ICustomerBL
    {

		private readonly IUnitOfWork unitOfWork;
		private readonly CustomerRepository customerRepository;
		private readonly IMapper mapper;

		/// <summary>
		/// This constructor
		/// </summary>
		/// <param name="_unitOfWork"></param>
		public CustomerBL(IUnitOfWork _unitOfWork, IMapper _mapper)
		{
			unitOfWork = _unitOfWork;
			customerRepository = new CustomerRepository(unitOfWork);
			mapper = _mapper;
		}
		/// <summary>
		/// This method Adds a new customer and returns the id
		/// </summary>
		/// <param name="customer"></param>
		/// <returns></returns>
		public async Task<int> AddCustomer(CustomerDVM customer)
		{
			int id = 0;
			if (customer != null)
			{
				Customer c = mapper.Map<Customer>(customer);
				try
				{
					var obj = await customerRepository.InsertAsync(c);
					id = obj.Id;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			return id;
		}

		/// <summary>
		/// This method deletes a customer details
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task<bool> DeleteCustomer(int Id)
		{
			bool result = false;
			if (Id > 0)
			{
				Customer c = await customerRepository.GetAsync(Id);
				if (c != null)
				{
					try
					{
						await customerRepository.UpdateAsync(c);
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
			}
			return result;
		}

		/// <summary>
		/// This method returns all customers
		/// </summary>
		/// <returns></returns>
		public async Task<List<CustomerDVM>> GetAllCustomers()
		{
			List<CustomerDVM> cdvmList = new List<CustomerDVM>();
			var cList = await customerRepository.GetAllAsync();
			if (cList != null)
			{
				cdvmList = new List<CustomerDVM>();
				try
				{
					cdvmList = mapper.Map<List<CustomerDVM>>(cList);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

			return cdvmList;
		}

		/// <summary>
		/// This method gets customer by id
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task<CustomerDVM> GetCustomerByID(int Id)
		{
			CustomerDVM cdvm = new CustomerDVM();
			if (Id > 0)
			{
				Customer c = await customerRepository.GetAsync(Id);
				if (c != null)
				{
					cdvm = mapper.Map<CustomerDVM>(c);
				}
			}
			return cdvm;
		}

		/// <summary>
		/// This method gets customer by id
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task<string> GetCustomerNameByID(int Id)
		{
			string name = string.Empty;
			if (Id > 0)
			{
				Customer c = await customerRepository.GetAsync(Id);
				if (c != null)
				{
					name = c.Firstname + " " + c.Lastname;
				}
			}
			return name;
		}

		/// <summary>
		/// This method updates a customer and returns true if successful
		/// </summary>
		/// <param name="customer"></param>
		/// <returns></returns>
		public async Task<bool> UpdateCustomer(CustomerDVM customer)
		{
			bool result = false;
			if (customer != null)
			{
				Customer c = await customerRepository.GetAsync(customer.Id);
				if (c != null)
				{
					c.Firstname = customer.Firstname;
					c.Email = customer.Email;
					c.Lastname = customer.Lastname;
					c.Phone = customer.Phone;

					try
					{
						await customerRepository.UpdateAsync(c);
						result = true;
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
			}
			return result;
		}
	}
}
