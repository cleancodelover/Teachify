using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teachify.DAL.Entities;
using Teachify.DALS.Entities;
using Teachify.DALS.Infrastructure;
using Teachify.DALS.Infrastructure.Interfaces;

namespace Teachify.DALS.Repositories
{
    
    /// <summary>
    /// AspNetRole Repository class
    /// </summary>
    public class AspNetRoleRepository : GenericRepository<AspNetRole>
    {
        public AspNetRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    /// <summary>
    /// AspNetRoleClaim Repository class
    /// </summary>
    public class AspNetRoleClaimRepository : GenericRepository<AspNetRoleClaim>
    {
        public AspNetRoleClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    /// <summary>
    /// AspNetUser Repository class
    /// </summary>
    public class AspNetUserRepository : GenericRepository<AspNetUser>
    {
        public AspNetUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    /// <summary>
    /// AspNetUserClaim Repository class
    /// </summary>
    public class AspNetUserClaimRepository : GenericRepository<AspNetUserClaim>
    {
        public AspNetUserClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    /// <summary>
    /// AspNetUserLogin Repository class
    /// </summary>
    public class AspNetUserLoginRepository : GenericRepository<AspNetUserLogin>
    {
        public AspNetUserLoginRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    /// <summary>
    /// AspNetUserRole Repository class
    /// </summary>
    public class AspNetUserRoleRepository : GenericRepository<AspNetUserRole>
    {
        public AspNetUserRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    /// <summary>
    /// AspNetUserToken Repository class
    /// </summary>
    public class AspNetUserTokenRepository : GenericRepository<AspNetUserToken>
    {
        public AspNetUserTokenRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    /// <summary>
    ///  ConsumableTypesRepository Repository class
    /// </summary>
    public class CourseRepository : GenericRepository<Course>
    {
        public CourseRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
    /// <summary>
    ///  ConsumableStockInventoryRepository Repository class
    /// </summary>
    public class ProviderRepository : GenericRepository<Provider>
    {
        public ProviderRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
    /// <summary>
    ///  ConsumableStockInventoryRepository Repository class
    /// </summary>
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
    /// <summary>
    ///  ConsumableStockInventoryRepository Repository class
    /// </summary>
    public class LocationRepository : GenericRepository<Location>
    {
        public LocationRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
    /// <summary>
    ///  ConsumableStockInventoryRepository Repository class
    /// </summary>
    public class CountryRepository : GenericRepository<Country>
    {
        public CountryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
    /// <summary>
    ///  ConsumableStockInventoryRepository Repository class
    /// </summary>
    public class StatesRepository : GenericRepository<State>
    {
        public StatesRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}