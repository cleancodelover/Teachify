using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teachify.BL.BusinessLogic.Interfaces;
using Teachify.BLL.BusinessLogic.Interfaces;
using Teachify.DAL.DVM;
using Teachify.DAL.Entities;
using Teachify.DALS.Infrastructure.Interfaces;
using Teachify.DALS.Repositories;

namespace Teachify.BLL.BusinessLogic
{
    public class ProvidersBL : IProvidersBL
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ProviderRepository providerRepository;
        private readonly IUsersBL usersBL;
        private readonly IMapper mapper;

        public ProvidersBL(IUnitOfWork _unitOfWork, IMapper _mapper, IUsersBL _usersBL)
        {
            unitOfWork = _unitOfWork;
            providerRepository = new ProviderRepository(unitOfWork);
            usersBL = _usersBL;
            mapper = _mapper;
        }
        /// <summary>
        /// This method adds a new provider
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public async Task<int> AddProvider(ProviderDVM dvm)
        {
            int result = 0;
            if (dvm != null)
            {
                Provider a = mapper.Map<Provider>(dvm);
                try
                {
                    var insQ = await providerRepository.InsertAsync(a);
                    if (insQ != null)
                    {
                        result = insQ.Id;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// This method deletes a provider
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProvider(int Id)
        {
            bool result = false;
            if (Id > 0)
            {
                var a = await providerRepository.GetAsync(Id);
                if (a != null)
                {
                    try
                    {
                        providerRepository.DeleteById(a);

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
        /// This method deletes a provider
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> ProviderExists(int Id)
        {
            bool result = false;
            if (Id > 0)
            {
                var a = await providerRepository.GetAsync(Id);
                if (a != null)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// This method gets a provider by provider id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ProviderDVM> GetProviderByProviderId(int Id)
        {
            ProviderDVM av = new ProviderDVM();
            if (Id > 0)
            {
                Provider a = await providerRepository.GetAsync(Id);
                if (a != null)
                {
                    av = mapper.Map<ProviderDVM>(a);
                }
            }
            return av;
        }

        /// <summary>
        /// This method gets a provider by provider id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ProviderDVM> GetProviderByEmail(string Id)
        {
            ProviderDVM av = null;
            if (!string.IsNullOrEmpty(Id))
            {
                var a = await providerRepository.GetAllAsync();
                var aa = a.Where(x => x.Email == Id).FirstOrDefault();
                if (aa != null)
                {
                    av = mapper.Map<ProviderDVM>(aa);
                }
            }
            return av;
        }

        /// <summary>
        /// This method get all providers
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProviderDVM>> GetProviders()
        {
            List<ProviderDVM> fvmList = new List<ProviderDVM>();
            var aList = await providerRepository.GetAllAsync();
            if (aList != null)
            {
                fvmList = mapper.Map<List<ProviderDVM>>(aList.ToList());
            }
            return fvmList;
        }
        /// <summary>
        /// This method updates a provider and returns the id
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProvider(ProviderDVM dvm)
        {
            bool result = false;
            if (dvm != null && dvm.Id > 0)
            {
                var am = await providerRepository.GetAsync(dvm.Id);
                if (am != null)
                {
                    am.Phone = dvm.Phone;
                    am.Firstname = dvm.Lastname;
                    am.Email = dvm.Email;
                    am.City = dvm.City;
                    am.Firstname = dvm.Firstname;
                    am.Nationality = dvm.Nationality;
                    await providerRepository.UpdateAsync(am);
                    result = true;
                }
            }
            return result;
        }
    }
}
