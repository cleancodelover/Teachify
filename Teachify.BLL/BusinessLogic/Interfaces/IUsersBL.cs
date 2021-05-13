using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Teachify.DAL.DVM;

namespace Teachify.BL.BusinessLogic.Interfaces
{
    public interface IUsersBL
    {
        //=========================================Users Methods====================================
        /// <summary>
        /// This method adds a new user and logs him in
        /// </summary>
        /// <param name="dvm"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> AddUserAndSignIn(AspNetUserDVM dvm, string role);
        /// <summary>
        /// This method adds a new user
        /// </summary>
        /// <param name="dvm"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> AddUser(AspNetUserDVM dvm, string role);
		/// <summary>
		/// This method gets a user by UserId
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		AspNetUserDVM GetUserByUserId(string id);
		/// <summary>
		/// This method gets a user by UserId
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		bool LockUser(string id);
		/// <summary>
		/// This method gets a user by UserId
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		bool UnLockUser(string id);
		/// <summary>
		/// This method gets a user by UserId
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		AspNetUserDVM GetUserByEmail(string email);
		/// <summary>
		/// This method get users
		/// </summary>
		/// <returns></returns>
		List<AspNetUserDVM> GetUsers();
		/// <summary>
		/// This method Deletes a User
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		Task<bool> DeleteUser(string Id);
		/// <summary>
		/// This method Updates a user
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		bool UpdateUser(AspNetUserDVM dvm);
		/// <summary>
		/// This method Logs a user in
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		AspNetUserDVM Login(AspNetUserDVM dvm);
		/// <summary>
		/// This method Logs a user in
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		bool EmailExists(string email);
		/// <summary>
		/// This method Logs a user in
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		AspNetUserDVM GetLoggedInUser(string email);
		/// <summary>
		/// This method Logs a user in
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		string GetUserId(ClaimsPrincipal cm);
		//=========================================Roles Methods====================================
		/// <summary>
		/// This method adds a new Role
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		Task<bool> AddRole(AspNetRoleDVM dvm);
		/// <summary>
		/// This method gets a role by RoleId
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		AspNetRoleDVM GetRoleByRoleId(string id);
		/// <summary>
		/// This method gets a role by RoleId
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		AspNetRoleDVM GetRoleByRoleName(string name);
		/// <summary>
		/// This method checks if a role exists by Role name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		bool RoleExists(string name);
		/// <summary>
		/// This method checks if a role exists by Role id
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		bool RoleExistsByRoleId(string id);
		/// <summary>
		/// This method get roles
		/// </summary>
		/// <returns></returns>
		List<AspNetRoleDVM> GetRoles();
		/// <summary>
		/// This method Deletes a role
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		Task<bool> DeleteRole(string name);
		/// <summary>
		/// This method Deletes a role by role id
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		Task<bool> DeleteRoleByRoleId(string id);
		/// <summary>
		/// This method Updates a role
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		bool UpdateRole(AspNetRoleDVM dvm);
		/// <summary>
		/// This method gets users in a role by role id, it returns a list of objects of users
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		List<AspNetUserDVM> GetUsersInRoleByRoleId(string Id);
		/// <summary>
		/// This method gets users in a role by role name, it returns a list of objects of users
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		List<AspNetUserDVM> GetUsersInRoleByRoleName(string name);
		/// <summary>
		/// This method gets user count in a role by role id, it returns an integer value
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		int GetCountUsersInRoleByRoleId(string Id);
		/// <summary>
		/// This method gets users in a role by role name, it returns an integer value
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		int GetCountUsersInRoleByRoleName(string name);
		/// <summary>
		/// This method gets a user roles by user id
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		List<string> GetUserRolesByUserId(string Id);
		//=========================================User Roles Methods====================================
		/// <summary>
		/// This method adds a user to role
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		Task<bool> AddUserToRoles(AspNetUserDVM dvm);
		/// <summary>
		/// This method adds a user to role
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		bool AddUserToRole(AspNetUserRoleDVM dvm);
		/// <summary>
		/// This method removes a user from a role by role id and user id
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		bool RemoveUserFromRoleByRoleIdAndUserId(string RoleId, string UserId);
		/// <summary>
		/// TThis method removes a user from a multiple roles by role name and user id
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		bool RemoveUserFromRoleByRoleNameAndRoleId(string RoleName, string UserId);
		/// <summary>
		/// This method removes a user from a multiple roles by role id and user id
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		bool RemoveUserFromRolesByRoleIdAndUserId(List<string> roleIds, string UserId);
		/// <summary>
		/// This method removes a user from a multiple roles by role name and user id
		/// </summary>
		/// <param name="dvm"></param>
		/// <returns></returns>
		bool RemoveUserFromRolesByRoleNameAndRoleId(List<string> roleNames, string UserId);
        Task<bool> ChangePassword(AspNetUserDVM dvm);
        Task<string> ForgotPassword(AspNetUserDVM dvm);
        Task<bool> SignOut();
    }
}
