using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Teachify.API.Models;
using Teachify.API.VM;
using Teachify.BL.BusinessLogic.Interfaces;
using Teachify.BLL.BusinessLogic.Interfaces;

namespace Teachify.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IUsersBL usersBL;
        private readonly ISetupBL setupBL;
        private readonly IProvidersBL providersBL;
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration _configuration,
            IUsersBL _usersBL,
            IProvidersBL _providersBL,
            ISetupBL _setupBL)
        {
            usersBL = _usersBL;
            setupBL = _setupBL;
            providersBL = _providersBL;
            configuration = _configuration;
        }
        
        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetAllProviders")]
        public async Task<ActionResult<List<ProviderVM>>> GetAllProviders()
        {
            var providers = await providersBL.GetProviders();
            return providers.Select(x => new ProviderVM {
                City=x.City,
                CourseDomain=x.CourseDomain,
                Description=x.Description,
                Education=x.Education,
                Email=x.Email,
                Experience=x.Experience,
                Firstname=x.Firstname,
                Gender=x.Gender,
                HourlyRate=x.HourlyRate,
                Id=x.Id,
                ImageUrl=x.ImageUrl,
                Lastname=x.Lastname,
                LogoUrl=x.LogoUrl,
                Nationality=x.Nationality,
                OneLineTitle=x.OneLineTitle,
                Phone=x.Phone
            }).ToList();
        }

        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetProvider/{id}")]
        public async Task<ActionResult<ProviderVM>> GetProvider(int id)
        {
            ProviderVM p = new ProviderVM();
            if (id > 0)
            {
                var provider = await providersBL.GetProviderByProviderId(id);
                if (provider != null)
                {
                    p.City = provider.City;
                    p.CourseDomain = provider.CourseDomain;
                    p.Description = provider.Description;
                    p.Education = provider.Education;
                    p.Email = provider.Email;
                    p.Experience = provider.Experience;
                    p.Firstname = provider.Firstname;
                    p.Gender = provider.Gender;
                    p.HourlyRate = provider.HourlyRate;
                    p.Id = provider.Id;
                    p.ImageUrl = provider.ImageUrl;
                    p.Lastname = provider.Lastname;
                    p.LogoUrl = provider.LogoUrl;
                    p.Nationality = provider.Nationality;
                    p.OneLineTitle = provider.OneLineTitle;
                    p.Phone = provider.Phone;
                }
            }
            return p;
        }

        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpdateProvider}")]
        public async Task<ActionResult<bool>> UpdateProvider(ProviderVM p)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                var provider = await providersBL.GetProviderByProviderId(p.Id);
                if (provider != null)
                {
                    provider.City = p.City;
                    provider.CourseDomain = p.CourseDomain;
                    provider.Description = p.Description;
                    provider.Education = p.Education;
                    provider.Email = p.Email;
                    provider.Experience = p.Experience;
                    provider.Firstname = p.Firstname;
                    provider.Gender = p.Gender;
                    provider.HourlyRate = p.HourlyRate;
                    provider.Id = p.Id;
                    provider.ImageUrl = p.ImageUrl;
                    provider.Lastname = p.Lastname;
                    provider.LogoUrl = p.LogoUrl;
                    provider.Nationality = p.Nationality;
                    provider.OneLineTitle = p.OneLineTitle;
                    provider.Phone = p.Phone;
                    if(await providersBL.UpdateProvider(provider))
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
        [HttpGet("DeleteMyAccount")]
        public async Task<ActionResult<bool>> DeleteMyAccount(int id)
        {
            bool result = false;
            if(id > 0)
            {
                if(await providersBL.DeleteProvider(id))
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
