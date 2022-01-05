using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IPublisherService: IBaseEntityService<BLLAppDTO.Publisher, DALAppDTO.Publisher>, IPublisherRepositoryCustom<BLLAppDTO.Publisher>
    {
        
    }
}