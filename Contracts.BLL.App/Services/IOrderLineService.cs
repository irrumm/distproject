using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IOrderLineService: IBaseEntityService<BLLAppDTO.OrderLine, DALAppDTO.OrderLine>, IOrderLineRepositoryCustom<BLLAppDTO.OrderLine>
    {
        
    }
}