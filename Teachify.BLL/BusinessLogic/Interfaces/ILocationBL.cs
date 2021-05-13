using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teachify.DAL.DVM;

namespace Teachify.BLL.BusinessLogic.Interfaces
{
    public interface ILocationBL
    {
        Task<int> AddLocation(LocationDVM location);
        Task<bool> DeleteLocation(int Id);
        List<LocationDVM> GetAllLocations();
        Task<LocationDVM> GetLocationByID(int Id);
        Task<bool> UpdateLocation(LocationDVM Location);
    }
}
