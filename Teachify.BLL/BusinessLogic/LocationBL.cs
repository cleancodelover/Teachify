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
    public class LocationBL : ILocationBL
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly LocationRepository locationRepository;
		private readonly IMapper mapper;

		/// <summary>
		/// This constructor
		/// </summary>
		/// <param name="_unitOfWork"></param>
		public LocationBL(IUnitOfWork _unitOfWork, IMapper _mapper)
		{
			unitOfWork = _unitOfWork;
			locationRepository = new LocationRepository(unitOfWork);
			mapper = _mapper;
		}
		/// <summary>
		/// This method Adds a new Location and returns the id
		/// </summary>
		/// <param name="Location"></param>
		/// <returns></returns>
		public async Task<int> AddLocation(LocationDVM location)
		{
			int id = 0;
			if (location != null)
			{
				Location c = mapper.Map<Location>(location);
				try
				{
					var obj = await locationRepository.InsertAsync(c);
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
		/// This method deletes a Location details
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task<bool> DeleteLocation(int Id)
		{
			bool result = false;
			if (Id > 0)
			{
				Location c = await locationRepository.GetAsync(Id);
				if (c != null)
				{
					try
					{
						await locationRepository.UpdateAsync(c);
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
		/// This method returns all Locations
		/// </summary>
		/// <returns></returns>
		public List<LocationDVM> GetAllLocations()
		{
			List<LocationDVM> cdvmList = new List<LocationDVM>();
			var cList = locationRepository.GetAllAsync();
			if (cList != null)
			{
				cdvmList = new List<LocationDVM>();
				try
				{
					cdvmList = mapper.Map<List<LocationDVM>>(cList);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

			return cdvmList;
		}

		/// <summary>
		/// This method gets Location by id
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task<LocationDVM> GetLocationByID(int Id)
		{
			LocationDVM cdvm = new LocationDVM();
			if (Id > 0)
			{
				Location c = await locationRepository.GetAsync(Id);
				if (c != null)
				{
					cdvm = mapper.Map<LocationDVM>(c);
				}
			}
			return cdvm;
		}

		/// <summary>
		/// This method updates a Location and returns true if successful
		/// </summary>
		/// <param name="Location"></param>
		/// <returns></returns>
		public async Task<bool> UpdateLocation(LocationDVM Location)
		{
			bool result = false;
			if (Location != null)
			{
				Location c = await locationRepository.GetAsync(Location.Id);
				if (c != null)
				{
					c.City = Location.City;
					c.Latitude = Location.Latitude;
					c.Longitude = Location.Longitude;
					c.PostalCode = Location.PostalCode;
					c.State = Location.State;

					try
					{
						await locationRepository.UpdateAsync(c);
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
