using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Teachify.BL.BusinessLogic.Interfaces;
using Teachify.DAL.DVM;
using Teachify.DALS.Entities;
using Teachify.DALS.Infrastructure.Interfaces;

namespace FirstDigitCare.BL.BusinessLogic
{
    public class UsersBL : IUsersBL
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AspNetUser> userManager;
        private readonly RoleManager<AspNetRole> roleManager;
        private readonly SignInManager<AspNetUser> signInManager;
        private readonly IMapper mapper;

        public UsersBL(IUnitOfWork _unitOfWork,
            UserManager<AspNetUser> _userManager,
            RoleManager<AspNetRole> _roleManager,
            SignInManager<AspNetUser> _signInManager,
            IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
            mapper = _mapper;
        }
        /// <summary>
        /// This method adds a new role
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public async Task<bool> AddRole(AspNetRoleDVM dvm)
        {
            bool status = false;
            if (dvm != null)
            {
                AspNetRole a = mapper.Map<AspNetRole>(dvm);
                try
                {
                    var result = await roleManager.CreateAsync(a);
                    if (result.Succeeded)
                    {
                        status = true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return status;
        }

        /// <summary>
        /// This method adds a new user
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(AspNetUserDVM dvm, string role)
        {
            bool status = false;
            if (dvm != null && !string.IsNullOrEmpty(role))
            {
                AspNetUser a = mapper.Map<AspNetUser>(dvm);
                try
                {
                    var roleD = roleManager.FindByNameAsync(role);
                    if (roleD != null)
                    {
                        var d = userManager.CreateAsync(a);
                        if (d.Result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(a, role);
                            status = true;
                        }
                    }
                    else
                    {
                        var roleDVM = new AspNetRoleDVM
                        {
                            Name = role
                        };
                        if (await AddRole(roleDVM))
                        {
                            var d = userManager.CreateAsync(a);
                            if (d.Result.Succeeded)
                            {
                                await userManager.AddToRoleAsync(a, role);
                                status = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return status;
        }

        /// <summary>
        /// This method adds a new user
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public async Task<bool> AddUserAndSignIn(AspNetUserDVM dvm, string role)
        {
            bool status = false;
            if (dvm != null && !string.IsNullOrEmpty(role))
            {
                AspNetUser a = mapper.Map<AspNetUser>(dvm);
                a.UserName = dvm.Email;
                try
                {
                    var roleD = roleManager.FindByNameAsync(role).Result;
                    AspNetUser chk = userManager.FindByEmailAsync(dvm.Email).Result;
                    if (roleD != null && chk != null)
                    {
                        var rolechk = userManager.GetRolesAsync(chk).Result;
                        if (rolechk.Count() > 0)
                        {
                            if (signInManager.PasswordSignInAsync(chk, dvm.PasswordHash, true, false).Result.Succeeded)
                                status = true;
                        }
                        else
                        {
                            var rdvm = new AspNetUserRoleDVM {
                                RoleId = roleD.Id,
                                UserId = chk.Id
                            };
                            if (AddUserToRole(rdvm))
                            {
                                if (signInManager.PasswordSignInAsync(a, dvm.PasswordHash, true, false).Result.Succeeded)
                                    status = true;
                            }
                        }
                    }
                    else if(roleD != null && chk == null)
                    {
                        var d = userManager.CreateAsync(a, dvm.PasswordHash);
                        if (d.Result.Succeeded)
                        {
                            var luser = userManager.FindByEmailAsync(dvm.Email).Result;
                            var rdvm = new AspNetUserRoleDVM
                            {
                                RoleId = roleD.Id,
                                UserId = luser.Id
                            };
                            if (AddUserToRole(rdvm))
                            {
                                if (signInManager.PasswordSignInAsync(a, dvm.PasswordHash, false, false).Result.Succeeded)
                                    status = true;
                            }
                        }
                    }
                    else if (roleD == null && chk == null)
                    {
                        var roleDVM = new AspNetRoleDVM
                        {
                            Name = role
                        };
                        if (await AddRole(roleDVM))
                        {
                            var d = userManager.CreateAsync(a, dvm.PasswordHash);
                            if (d.Result.Succeeded)
                            {
                                await userManager.AddToRoleAsync(a, role);
                                if (signInManager.PasswordSignInAsync(a, dvm.PasswordHash, true, false).Result.Succeeded)
                                    status = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return status;
        }

        /// <summary>
        /// This method adds a user to a role
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public async Task<bool> AddUserToRoles(AspNetUserDVM dvm)
        {
            bool status = false;
            if (dvm != null)
            {
                AspNetUser a = mapper.Map<AspNetUser>(dvm);
                try
                {
                    var result = await userManager.AddToRolesAsync(a, dvm.Roles);
                    if (result.Succeeded)
                    {
                        status = true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return status;
        }
        /// <summary>
        /// This method adds a user to a role
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public bool AddUserToRole(AspNetUserRoleDVM dvm)
        {
            bool status = false;
            if (dvm != null)
            {
                AspNetUser a = userManager.FindByIdAsync(dvm.UserId).Result;
                string roleName = roleManager.FindByIdAsync(dvm.RoleId).Result.Name ?? "";
                try
                {
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        ;
                        if (userManager.AddToRoleAsync(a, roleName).Result.Succeeded)
                        {
                            status = true;
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return status;
        }
        /// <summary>
        /// This method deletes a role
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRole(string name)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(name))
            {
                var ur = userManager.GetUsersInRoleAsync(name).Result.ToList();
                int count = 0;
                int total = ur.Count;
                foreach (var item in ur)
                {
                    var result = await userManager.RemoveFromRoleAsync(item, name);
                    if (result.Succeeded)
                    {
                        count++;
                    }
                }
                if (count == total)
                {
                    var r = roleManager.FindByNameAsync(name).Result;
                    await roleManager.DeleteAsync(r);
                    status = true;
                }
            }
            return status;
        }

        /// <summary>
        /// This method deletes a role
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRoleByRoleId(string id)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(id))
            {
                var r = roleManager.FindByIdAsync(id);
                if (r != null)
                {
                    string name = r.Result.Name;
                    var ur = userManager.GetUsersInRoleAsync(name).Result.ToList();
                    int count = 0;
                    int total = ur.Count;
                    foreach (var item in ur)
                    {
                        var result = await userManager.RemoveFromRoleAsync(item, name);
                        if (result.Succeeded)
                        {
                            count++;
                        }
                    }
                    if (count == total)
                    {
                        await roleManager.DeleteAsync(r.Result);
                        status = true;
                    }
                }
            }
            return status;
        }


        /// <summary>
        /// This method deletes a user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(string Id)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(Id))
            {
                AspNetUser u = userManager.FindByIdAsync(Id).Result;
                var ur = userManager.GetRolesAsync(u).Result.ToList();
                int count = 1;
                int total = ur.Count;
                foreach (var item in ur)
                {
                    var result = await userManager.RemoveFromRoleAsync(u, item);
                    if (result.Succeeded)
                    {
                        count++;
                    }
                }
                if (count == total)
                {
                    await userManager.DeleteAsync(u);
                    status = true;
                }
            }
            return status;
        }

        /// <summary>
        /// This method gets count users in role by roleid and returns an integer value
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int GetCountUsersInRoleByRoleId(string Id)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(Id))
            {
                var r = roleManager.FindByIdAsync(Id).Result;
                if (r != null)
                {
                    string roleName = r.Name;
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        count = userManager.GetUsersInRoleAsync(roleName).Result.Count;
                    }
                }

            }
            return count;
        }


        /// <summary>
        /// This method gets count users in role by roleid and returns an integer value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetCountUsersInRoleByRoleName(string name)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(name))
            {
                count = userManager.GetUsersInRoleAsync(name).Result.Count;
            }
            return count;
        }

        /// <summary>
        /// This method gets role by role name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AspNetRoleDVM GetRoleByRoleName(string name)
        {
            AspNetRoleDVM dvm = new AspNetRoleDVM();
            if (!string.IsNullOrEmpty(name))
            {
                var m = roleManager.FindByNameAsync(name).Result;
                if (m != null)
                {
                    dvm = mapper.Map<AspNetRoleDVM>(m);
                }
            }
            return dvm;
        }

        /// <summary>
        /// This method gets role by role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AspNetRoleDVM GetRoleByRoleId(string id)
        {
            AspNetRoleDVM dvm = new AspNetRoleDVM();
            if (!string.IsNullOrEmpty(id))
            {
                AspNetRole m = roleManager.FindByIdAsync(id).Result;
                if (m != null)
                {
                    dvm = mapper.Map<AspNetRoleDVM>(m);
                }
            }
            return dvm;
        }

        /// <summary>
        /// This method gets all roles
        /// </summary>
        /// <returns></returns>
        public List<AspNetRoleDVM> GetRoles()
        {
            List<AspNetRoleDVM> rL = new List<AspNetRoleDVM>();
            List<AspNetRoleDVM> roles = roleManager.Roles.Select(x => new AspNetRoleDVM
            {
                ConcurrencyStamp = x.ConcurrencyStamp,
                Name = x.Name,
                Id = x.Id,
                ProviderId = x.ProviderId,
                NormalizedName = x.NormalizedName
            }).ToList();
            if (roles != null)
            {
                rL = mapper.Map<List<AspNetRoleDVM>>(roles);
            }
            return rL;
        }

        /// <summary>
        /// This method gets a user by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AspNetUserDVM GetUserByUserId(string id)
        {
            AspNetUserDVM dvm = new AspNetUserDVM();
            if (!string.IsNullOrEmpty(id))
            {
                var m = userManager.FindByIdAsync(id).Result;
                if (m != null)
                {
                    dvm = mapper.Map<AspNetUserDVM>(m);
                }
            }
            return dvm;
        }
        /// <summary>
        /// This method gets a user by email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AspNetUserDVM GetUserByEmail(string email)
        {
            AspNetUserDVM dvm = new AspNetUserDVM();
            if (!string.IsNullOrEmpty(email))
            {
                var m = userManager.FindByEmailAsync(email).Result;
                if (m != null)
                {
                    dvm = mapper.Map<AspNetUserDVM>(m);
                }
            }
            return dvm;
        }


        /// <summary>
        /// This method locks a user by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool LockUser(string id)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(id))
            {
                var m = userManager.FindByIdAsync(id).Result;
                if (m != null)
                {

                    m.LockoutEnabled = true;
                    m.LockoutEnd = DateTimeOffset.MaxValue;
                    if (userManager.UpdateAsync(m).Result.Succeeded)
                    {
                        status = true;
                    }
                }
            }
            return status;
        }
        /// <summary>
        /// This method unlocks a user by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UnLockUser(string id)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(id))
            {
                var m = userManager.FindByIdAsync(id).Result;
                if (m != null && m.LockoutEnabled)
                {
                    if (!m.LockoutEnabled)
                    {
                        m.LockoutEnd = DateTimeOffset.Now;
                        if (userManager.UpdateAsync(m).Result.Succeeded)
                        {
                            status = true;
                        }
                    }
                    else
                    {
                        m.LockoutEnabled = true;
                        m.LockoutEnd = DateTimeOffset.Now;
                        if (userManager.UpdateAsync(m).Result.Succeeded)
                        {
                            status = true;
                        }
                    }
                }
            }
            return status;
        }

        /// <summary>
        /// This method gets user roles by user id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<string> GetUserRolesByUserId(string Id)
        {
            List<string> uRoles = new List<string>();
            if (!string.IsNullOrEmpty(Id))
            {
                AspNetUser u = userManager.FindByIdAsync(Id).Result;
                if (u != null)
                {
                    uRoles = userManager.GetRolesAsync(u).Result.ToList();
                }
            }
            return uRoles;
        }

        /// <summary>
        /// This method gets users
        /// </summary>
        /// <returns></returns>
        public List<AspNetUserDVM> GetUsers()
        {
            List<AspNetUserDVM> list = new List<AspNetUserDVM>();
            var uList = userManager.Users.ToList();
            if (uList != null)
            {
                list = mapper.Map<List<AspNetUserDVM>>(uList);
            }
            return list;
        }

        /// <summary>
        /// This method gets users in a role by role id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<AspNetUserDVM> GetUsersInRoleByRoleId(string Id)
        {
            List<AspNetUserDVM> list = new List<AspNetUserDVM>();
            if (!string.IsNullOrEmpty(Id))
            {
                var role = roleManager.FindByIdAsync(Id).Result;
                string roleName = role != null ? role.Name : "";
                var uR = userManager.GetUsersInRoleAsync(roleName).Result;
                list = mapper.Map<List<AspNetUserDVM>>(uR);
            }
            return list;
        }

        /// <summary>
        /// This method gets users in a role by role name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<AspNetUserDVM> GetUsersInRoleByRoleName(string name)
        {
            List<AspNetUserDVM> list = new List<AspNetUserDVM>();
            if (!string.IsNullOrEmpty(name))
            {
                var ulist = userManager.GetUsersInRoleAsync(name).Result.ToList();
                list = mapper.Map<List<AspNetUserDVM>>(list);
            }
            return list;
        }

        /// <summary>
        /// This method removes a user from a role by role name and role id
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool RemoveUserFromRoleByRoleIdAndUserId(string RoleId, string UserId)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(RoleId) && !string.IsNullOrEmpty(UserId))
            {
                string roleName = "";
                var role = roleManager.FindByIdAsync(RoleId).Result;
                if (role != null)
                {
                    roleName = role.Name;
                    var user = userManager.FindByIdAsync(UserId).Result;
                    if (user != null)
                        if (userManager.RemoveFromRoleAsync(user, roleName).Result.Succeeded)
                            status = true;
                }
            }
            return status;
        }

        /// <summary>
        /// This method removes a user from role by role name and role id
        /// </summary>
        /// <param name="RoleName"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool RemoveUserFromRoleByRoleNameAndRoleId(string RoleName, string UserId)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(RoleName) && !string.IsNullOrEmpty(UserId))
            {
                var user = userManager.FindByIdAsync(UserId).Result;
                if (user != null)
                {
                    if (userManager.RemoveFromRoleAsync(user, RoleName).Result.Succeeded)
                        status = true;
                }
            }
            return status;
        }

        /// <summary>
        /// This method removes a user from multiple roles by role id and role id
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool RemoveUserFromRolesByRoleIdAndUserId(List<string> roleIds, string UserId)
        {
            bool status = false;
            if (roleIds.Count > 0 && !string.IsNullOrEmpty(UserId))
            {
                int count = 0;
                int total = roleIds.Count;
                foreach (string roleId in roleIds)
                {
                    string roleName = "";
                    var role = roleManager.FindByIdAsync(roleId).Result;
                    if (role != null)
                    {
                        roleName = role.Name;
                        var user = userManager.FindByIdAsync(UserId).Result;
                        if (user != null)
                            if (userManager.RemoveFromRoleAsync(user, roleName).Result.Succeeded)
                                count++;
                    }
                }
                if (count == total)
                    status = true;
            }
            return status;
        }

        /// <summary>
        /// This method removes a user from multiple roles by role name and role id
        /// </summary>
        /// <param name="roleNames"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool RemoveUserFromRolesByRoleNameAndRoleId(List<string> roleNames, string UserId)
        {
            bool status = false;
            if (roleNames.Count > 0 && !string.IsNullOrEmpty(UserId))
            {
                var user = userManager.FindByIdAsync(UserId).Result;
                if (user != null)
                {
                    int count = 0;
                    int total = roleNames.Count;
                    foreach (string roleName in roleNames)
                        if (userManager.RemoveFromRoleAsync(user, roleName).Result.Succeeded)
                            count++;
                    if (count == total)
                        status = true;
                }
            }
            return status;
        }
        /// <summary>
        /// This method updates a role
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public bool UpdateRole(AspNetRoleDVM dvm)
        {
            bool status = false;
            if (dvm != null)
            {
                var m = roleManager.FindByIdAsync(dvm.Id).Result;
                if (m != null)
                {
                    m.Name = dvm.Name;
                    if (roleManager.UpdateAsync(m).Result.Succeeded)
                        status = true;
                }
            }
            return status;
        }

        /// <summary>
        /// This method updates user
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public bool UpdateUser(AspNetUserDVM dvm)
        {
            bool status = false;
            if (dvm != null)
            {
                var m = userManager.FindByIdAsync(dvm.Id).Result;
                if (m != null)
                {
                    m.PhoneNumber = dvm.PhoneNumber;
                    m.UserName = dvm.UserName;
                    if (userManager.UpdateAsync(m).Result.Succeeded)
                        status = true;
                }
            }
            return status;
        }

        /// <summary>
        /// This method logs a user in
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public AspNetUserDVM Login(AspNetUserDVM dvm)
        {
            AspNetUserDVM ndvm = new AspNetUserDVM();
            if (dvm != null)
            {
                if (signInManager.PasswordSignInAsync(dvm.Email, dvm.PasswordHash, dvm.RememberMe, false).Result.Succeeded)
                    ndvm = mapper.Map<AspNetUserDVM>(userManager.FindByEmailAsync(dvm.Email).Result);
                ndvm.Roles = userManager.GetRolesAsync(userManager.FindByEmailAsync(dvm.Email).Result).Result.ToList();
            }
            return ndvm;
        }

        /// <summary>
        /// This method checks if an email exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool EmailExists(string email)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(email))
            {
                var m = userManager.FindByEmailAsync(email).Result;
                if (m != null && !string.IsNullOrEmpty(m.Email))
                    status = true;
            }
            return status;
        }

        /// <summary>
        /// This method gets a user that has logged in
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AspNetUserDVM GetLoggedInUser(string email)
        {
            AspNetUserDVM ndvm = new AspNetUserDVM();
            if (!string.IsNullOrEmpty(email))
            {
                ndvm = mapper.Map<AspNetUserDVM>(userManager.FindByEmailAsync(email).Result);
            }
            return ndvm;
        }

        /// <summary>
        /// This method gets a user that has logged in
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> SignOut()
        {
            await signInManager.SignOutAsync();
            return true;
        }
        public async Task<bool> ChangePassword(AspNetUserDVM dvm)
        {
            if(dvm != null)
            {
                var user = await userManager.FindByEmailAsync(dvm.Email);
                if(user != null)
                {
                    var result = await userManager.ChangePasswordAsync(user, dvm.OldPassword, dvm.NewPassword);
                    if (result.Succeeded)
                    {
                        return result.Succeeded;
                    }
                }
            }
            return false;
        }
        public async Task<string> ForgotPassword(AspNetUserDVM dvm)
        {
            string password = string.Empty;
            if (dvm != null)
            {
                var user = await userManager.FindByEmailAsync(dvm.Email);
                if (user != null)
                {
                    string token = await userManager.GeneratePasswordResetTokenAsync(user);
                    if (!string.IsNullOrEmpty(token))
                    {
                        var result = await userManager.ResetPasswordAsync(user, token, dvm.NewPassword);
                        if (result.Succeeded)
                        {
                            return dvm.NewPassword;
                        }
                    }
                }
            }
            return password;
        }
        /// <summary>
        /// This method gets a user that has logged in
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetUserId(ClaimsPrincipal cm)
        {
            string Id = "";
            if (cm != null)
            {
                Id = userManager.GetUserId(cm);
            }
            return Id;
        }

        /// <summary>
        /// This method checks if a role exists by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RoleExists(string name)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(name))
            {
                var m = roleManager.FindByNameAsync(name).Result;
                if (m != null)
                {
                    status = true;
                }
            }
            return status;
        }
        /// <summary>
        /// This method checks if a role exists by role id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RoleExistsByRoleId(string id)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(id))
            {
                var m = roleManager.FindByIdAsync(id).Result;
                if (m != null)
                {
                    status = true;
                }
            }
            return status;
        }
    }
}