using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IAwardRepository : IBaseRepository<DALAppDTO.Award>, IAwardRepositoryCustom<DALAppDTO.Award>
    {

    }

    public interface IAwardRepositoryCustom<TEntity>
    {
    }
}