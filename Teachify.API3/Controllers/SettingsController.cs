using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teachify.API.Models;
using Teachify.API.VM;
using Teachify.BL.BusinessLogic.Interfaces;
using Teachify.BLL.BusinessLogic.Interfaces;

namespace Teachify.API2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly IUsersBL usersBL;
        private readonly ISetupBL setupBL;
        private readonly TextInfo textInfo;
        private readonly IProvidersBL providersBL;
        private readonly IWebHostEnvironment hostingEnvironment; 
        
        public SettingsController(
             IUsersBL _usersBL,
             IProvidersBL _providersBL,
             IWebHostEnvironment _hostingEnvironment,
             ISetupBL _setupBL)
        {
            usersBL = _usersBL;
            setupBL = _setupBL;
            providersBL = _providersBL;
            textInfo = new CultureInfo("en-US", false).TextInfo;
            hostingEnvironment = _hostingEnvironment;
        }

        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetAllStates")]
        public async Task<ActionResult<List<StateVM>>> GetAllStates()
        {
            var courses = await setupBL.GetAllStatesAsync();
            return courses.Select(x => new StateVM
            {
                Name = x.Name,
                Code=x.Code,
                CountryId=x.CountryId
            }).ToList();
        }

        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetState/{id}")]
        public async Task<ActionResult<StateVM>> GetState(int id)
        {
            StateVM p = new StateVM();
            if (id > 0)
            {
                var state = await setupBL.GetStateByIDAsync(id);
                if (state != null)
                {
                    p.Name = state.Name;
                    p.Code = state.Code;
                    p.CountryId = state.CountryId;
                }
            }
            return p;
        }

        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpdateState/{id}")]
        public async Task<ActionResult<bool>> UpdateState(StateVM p)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                var state = await setupBL.GetStateByIDAsync(p.Code);
                if (state != null)
                {
                    state.Name = p.Name;
                    if (await setupBL.UpdateStateAsync(state))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetAllCities")]
        public ActionResult<List<CityVM>> GetAllCities()
        {
            string contentRootPath = hostingEnvironment.ContentRootPath;
            var JSON = System.IO.File.ReadAllText(contentRootPath + "/json/ng.json");

            List<CityVM> cities = JsonSerializer.Deserialize<List<CityVM>>(JSON);
            return cities;
        }


        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetAllCitiesByStateId")]
        public async Task<ActionResult<List<CityVM>>> GetAllCitiesByStateId(string code)
        {
            string contentRootPath = hostingEnvironment.ContentRootPath;
            var JSON = System.IO.File.ReadAllText(contentRootPath + "/json/ng.json");

            List<CityVM> cities = JsonSerializer.Deserialize<List<CityVM>>(JSON);
            var state = await setupBL.GetStateByIDAsync(code);
            if(state != null)
            {
                cities = cities.Where(x => x.admin == state.Name).ToList();
            }
            return cities;
        }
    }
}
