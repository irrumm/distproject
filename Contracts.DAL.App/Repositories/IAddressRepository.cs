using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IAddressRepository : IBaseRepository<DALAppDTO.Address>, IAddressRepositoryCustom<DALAppDTO.Address>
    {
    }
    
    public interface IAddressRepositoryCustom<TEntity>
    {
    }

}