using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IGameInfoService: IBaseEntityService<BLLAppDTO.GameInfo, DALAppDTO.GameInfo>, IGameInfoRepositoryCustom<BLLAppDTO.GameInfo>
    {
        public Task<IEnumerable<BLLAppDTO.GameInfo>> GetAllFiltered(List<Guid> categories, List<Guid> languages,
            List<Guid> publishers, int minCost, int maxCost, bool noTracking = true);
    }
}