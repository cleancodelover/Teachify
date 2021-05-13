using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teachify.DAL.DVM;

namespace Teachify.BLL.BusinessLogic.Interfaces
{
    public interface ISetupBL
    {
        Task<string> AddStateAsync(StateDVM entity);
        Task<bool> DeleteCountryAsync(object Id);
        Task<bool> DeleteStateAsync(object Id);
        Task<List<StateDVM>> GetAllStatesAsync();
        Task<CountryDVM> GetCountryByIDAsync(object Id);
        Task<StateDVM> GetStateByIDAsync(object Id);
        Task<bool> UpdateCountryAsync(CountryDVM entity);
        Task<bool> UpdateStateAsync(StateDVM entity);
    }
}
