using System;
using SIMS.Models;

namespace SIMS.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        SIMSWebEntities Init();
    }
}
