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
    public class CoursesController : ControllerBase
    {
        private readonly IUsersBL usersBL;
        private readonly ISetupBL setupBL;
        private readonly IProvidersBL providersBL;
        private readonly ICoursesBL coursesBL;
        private readonly IConfiguration configuration;

        public CoursesController(IConfiguration _configuration,
            IUsersBL _usersBL,
            IProvidersBL _providersBL,
            ICoursesBL _coursesBL,
            ISetupBL _setupBL)
        {
            usersBL = _usersBL;
            setupBL = _setupBL;
            coursesBL = _coursesBL;
            providersBL = _providersBL;
            configuration = _configuration;
        }

        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetAllCourses")]
        public async Task<ActionResult<List<CourseVM>>> GetAllCourses()
        {
            var courses = await coursesBL.GetCourses();
            return courses.Select(x => new CourseVM
            {
                Name=x.Name,
                Id=x.Id,
                DateCreated=x.DateCreated
            }).ToList();
        }

        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetCourse/{id}")]
        public async Task<ActionResult<CourseVM>> GetCourse(int id)
        {
            CourseVM p = new CourseVM();
            if (id > 0)
            {
                var course = await coursesBL.GetCourseByCourseId(id);
                if (course != null)
                {
                    p.Name = course.Name;
                    p.Id = course.Id;
                    p.DateCreated = course.DateCreated;
                }
            }
            return p;
        }

        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpdateCourse/{id}")]
        public async Task<ActionResult<bool>> UpdateCourse(CourseVM p)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                var course = await coursesBL.GetCourseByCourseId(p.Id);
                if (course != null)
                {
                    course.Name = p.Name;
                    if (await coursesBL.UpdateCourse(course))
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
        [HttpGet("DeleteMyCourse")]
        public async Task<ActionResult<bool>> DeleteMyCourse(int id)
        {
            bool result = false;
            if (id > 0)
            {
                if (await providersBL.DeleteProvider(id))
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
