using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class GameAwardService: BaseEntityService<IAppUnitOfWork, IGameAwardRepository, BLLAppDTO.GameAward, DALAppDTO.GameAward>, IGameAwardService
    {
        public GameAwardService(IAppUnitOfWork serviceUow, IGameAwardRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameAwardMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.GameAward>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByGameApiAsync(gameId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.GameAward>> GetAllByAwardApiAsync(Guid awardId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByAwardApiAsync(awardId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.GameAward>> GetAllApiAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllApiAsync(noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.GameAward?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultApiAsync(id, noTracking));
        }
    }
}