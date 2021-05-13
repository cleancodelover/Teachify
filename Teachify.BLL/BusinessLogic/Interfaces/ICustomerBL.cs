using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teachify.DAL.DVM;

namespace Teachify.BLL.BusinessLogic.Interfaces
{
    public interface ICustomerBL
    {
        Task<int> AddCustomer(CustomerDVM customer);
        Task<bool> DeleteCustomer(int Id);
        Task<List<CustomerDVM>> GetAllCustomers();
        Task<CustomerDVM> GetCustomerByID(int Id);
        Task<string> GetCustomerNameByID(int Id);
        Task<bool> UpdateCustomer(CustomerDVM customer);
    }
}
