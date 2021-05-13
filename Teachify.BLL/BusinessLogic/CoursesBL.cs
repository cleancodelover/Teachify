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
    public class CoursesBL : ICoursesBL
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly CourseRepository CourseRepository;
        private readonly IUsersBL usersBL;
        private readonly IMapper mapper;

        public CoursesBL(IUnitOfWork _unitOfWork, IMapper _mapper, IUsersBL _usersBL)
        {
            unitOfWork = _unitOfWork;
            CourseRepository = new CourseRepository(unitOfWork);
            usersBL = _usersBL;
            mapper = _mapper;
        }
        /// <summary>
        /// This method adds a new Course
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public async Task<int> AddCourse(CourseDVM dvm)
        {
            int result = 0;
            if (dvm != null)
            {
                Course a = mapper.Map<Course>(dvm);
                try
                {
                    var insQ = await CourseRepository.InsertAsync(a);
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
        /// This method deletes a Course
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCourse(int Id)
        {
            bool result = false;
            if (Id > 0)
            {
                var a = await CourseRepository.GetAsync(Id);
                if (a != null)
                {
                    try
                    {
                        CourseRepository.DeleteById(a);

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
        /// This method deletes a Course
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> CourseExists(int Id)
        {
            bool result = false;
            if (Id > 0)
            {
                var a = await CourseRepository.GetAsync(Id);
                if (a != null)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// This method gets a Course by Course id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CourseDVM> GetCourseByCourseId(int Id)
        {
            CourseDVM av = new CourseDVM();
            if (Id > 0)
            {
                Course a = await CourseRepository.GetAsync(Id);
                if (a != null)
                {
                    av = mapper.Map<CourseDVM>(a);
                }
            }
            return av;
        }

        /// <summary>
        /// This method get all Courses
        /// </summary>
        /// <returns></returns>
        public async Task<List<CourseDVM>> GetCourses()
        {
            List<CourseDVM> fvmList = new List<CourseDVM>();
            var aList = await CourseRepository.GetAllAsync();
            if (aList != null)
            {
                fvmList = mapper.Map<List<CourseDVM>>(aList.ToList());
            }
            return fvmList;
        }
        /// <summary>
        /// This method updates a Course and returns the id
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCourse(CourseDVM dvm)
        {
            bool result = false;
            if (dvm != null && dvm.Id > 0)
            {
                var am = await CourseRepository.GetAsync(dvm.Id);
                if (am != null)
                {
                    am.DateCreated = dvm.DateCreated;
                    am.Id = dvm.Id;
                    am.Name = dvm.Name;
                    
                    await CourseRepository.UpdateAsync(am);
                    result = true;
                }
            }
            return result;
        }
    }
}
