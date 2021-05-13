using FluentNHibernate.Automapping;
using Microsoft.AspNetCore.Identity;
using Teachify.DAL.DVM;
using Teachify.DAL.Entities;
using Teachify.DALS.Entities;

namespace FirstDigitCare.BL.Infrastructure
{
	public class AutoMapperWebProfile : AutoMapper.Profile
	{
		public AutoMapperWebProfile()
        {
            CreateMap<AspNetRoleClaim, AspNetRoleClaimDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AspNetRoleClaimDVM, AspNetRoleClaim>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AspNetRole, AspNetRoleDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AspNetRoleDVM, AspNetRole>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AspNetUserClaim, AspNetUserClaimDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AspNetUserClaimDVM, AspNetUserClaim>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AspNetUser, AspNetUserDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AspNetUserDVM, AspNetUser>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AspNetUserLogin, AspNetUserLoginDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AspNetUserLoginDVM, AspNetUserLogin>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AspNetUserRole, AspNetUserRoleDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AspNetUserRoleDVM, AspNetUserRole>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AspNetUserToken, AspNetUserTokenDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AspNetUserTokenDVM, AspNetUserToken>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<Country, CountryDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CountryDVM, Country>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<State, StateDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<StateDVM, State>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Customer, CustomerDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CustomerDVM, Customer>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Course, CourseDVM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CourseDVM, Course>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            //End Consumables

        }
    }
}