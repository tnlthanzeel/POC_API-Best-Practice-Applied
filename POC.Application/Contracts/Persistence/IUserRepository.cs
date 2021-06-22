using POC.Domain.Entitities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
