using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teachify.API.Models;
using Teachify.API.VM;

namespace Teachify.API.Services.Interfaces
{
    public interface IAuthenticateService
    {
        AspNetUserVM Authenticate(AspNetUserVM vm);
    }
}
