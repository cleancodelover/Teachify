using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teachify.BLL.BusinessLogic.Interfaces;
using Teachify.DAL.DVM;
using Teachify.DAL.Entities;
using Teachify.DALS.Infrastructure.Interfaces;
using Teachify.DALS.Repositories;

namespace Teachify.BLL.BusinessLogic
{
    public class SetupBL : ISetupBL
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly CountryRepository countryRepository;
        private readonly StatesRepository StateRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// This constructor
        /// </summary>
        /// <param name="_unitOfWork"></param>
        public SetupBL(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            countryRepository = new CountryRepository(_unitOfWork);
            StateRepository = new StatesRepository(_unitOfWork);
            mapper = _mapper;
        }

        public async Task<bool> AddCountryAsync(CountryDVM entity)
        {
            bool id = false;
            if (entity != null)
            {
                Country c = mapper.Map<Country>(entity);
                try
                {
                    var obj = await countryRepository.InsertAsync(c);
                    id = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return id;
        }

        public async Task<List<CountryDVM>> GetAllCountries()
        {
            List<CountryDVM> cdvmList = null;
            var cList = await countryRepository.GetAllAsync();
            if (cList != null)
            {
                cdvmList = new List<CountryDVM>();
                try
                {
                    cdvmList = mapper.Map<List<CountryDVM>>(cList.ToList());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return cdvmList;
        }

        public async Task<CountryDVM> GetCountryByIDAsync(object Id)
        {
            try
            {
                CountryDVM cdvm = null;
                if (Id != null)
                {
                    Country d = await countryRepository.GetAsync(Id);
                    if (d != null)
                    {
                        cdvm = mapper.Map<CountryDVM>(d);
                    }
                }
                return cdvm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<List<StateDVM>> GetAllStatesAsync()
        {
            List<StateDVM> cdvmList = null;
            var cList = await StateRepository.GetAllAsync();
            if (cList != null)
            {
                cdvmList = new List<StateDVM>();
                try
                {
                    cdvmList = mapper.Map<List<StateDVM>>(cList.ToList());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return cdvmList;
        }
        public async Task<bool> DeleteCountryAsync(object Id)
        {
            bool result = false;
            if (Id != null)
            {
                Country d = await countryRepository.GetAsync(Id);
                if (d != null)
                {
                    try
                    {
                        countryRepository.DeleteById(Id);
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

        public async Task<bool> UpdateCountryAsync(CountryDVM entity)
        {
            bool result = false;
            if (entity != null)
            {
                Country d = countryRepository.Get(entity.Code);
                if (d != null)
                {
                    d.Name = entity.Name;
                    try
                    {
                        await countryRepository.UpdateAsync(d);
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
        public async Task<string> AddStateAsync(StateDVM entity)
        {
            string code = "";
            if (entity != null)
            {
                State c = mapper.Map<State>(entity);
                try
                {
                    var obj = await StateRepository.InsertAsync(c);
                    code = obj.Code;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return code;
        }


        public async Task<StateDVM> GetStateByIDAsync(object Id)
        {
            try
            {
                StateDVM pdvm = null;
                if (Id != null)
                {
                    State d = await StateRepository.GetAsync(Id);
                    if (d != null)
                    {
                        pdvm = mapper.Map<StateDVM>(d);
                    }
                }
                return pdvm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteStateAsync(object Id)
        {
            bool result = false;
            if (Id != null)
            {
                State d = await StateRepository.GetAsync(Id);
                if (d != null)
                {
                    try
                    {
                        StateRepository.DeleteById(Id);
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

        public async Task<bool> UpdateStateAsync(StateDVM entity)
        {
            bool result = false;
            if (entity != null)
            {
                State d = StateRepository.Get(entity.Code);
                if (d != null)
                {
                    d.CountryId = entity.CountryId;
                    d.Name = entity.Name;
                    d.RegionCode = entity.RegionCode;

                    try
                    {
                        await StateRepository.UpdateAsync(d);
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
        public async Task<List<StateDVM>> GetAllStatesByCountryIdAsync(string Id)
        {
            try
            {
                List<StateDVM> pdvm = null;
                if (Id != null)
                {
                    var d = await StateRepository.GetAllAsync();
                    if (d != null)
                    {
                        List<State> states = d.Where(m => m.CountryId == Id).ToList();
                        pdvm = mapper.Map<List<StateDVM>>(states);
                    }
                }
                return pdvm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<StateDVM>> GetAllStatesByRegionCodeAsync(string Id)
        {
            try
            {
                List<StateDVM> pdvm = null;
                if (Id != null)
                {
                    var d = await StateRepository.GetAllAsync().Result.Where(m => m.RegionCode == Id).AsQueryable().ToListAsync();
                    if (d != null)
                    {
                        pdvm = mapper.Map<List<StateDVM>>(d);
                    }
                }
                return pdvm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
