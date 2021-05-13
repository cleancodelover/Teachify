using System.Collections.Generic;
using System.Threading.Tasks;
using Teachify.DAL.DVM;

namespace Teachify.BLL.BusinessLogic.Interfaces
{
    public interface IProvidersBL
    {
        Task<int> AddProvider(ProviderDVM dvm);
        Task<bool> DeleteProvider(int Id);
        Task<ProviderDVM> GetProviderByEmail(string Id);
        Task<ProviderDVM> GetProviderByProviderId(int Id);
        Task<List<ProviderDVM>> GetProviders();
        Task<bool> ProviderExists(int Id);
        Task<bool> UpdateProvider(ProviderDVM dvm);
    }
}
