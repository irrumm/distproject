using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPublisherRepository : IBaseRepository<DALAppDTO.Publisher>, IPublisherRepositoryCustom<DALAppDTO.Publisher>
    {
        
    }
    
    public interface IPublisherRepositoryCustom<TEntity>
    {
        
    }
}