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
    public class GameService: BaseEntityService<IAppUnitOfWork, IGameRepository, BLLAppDTO.Game, DALAppDTO.Game>, IGameService
    {
        public GameService(IAppUnitOfWork serviceUow, IGameRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.Game>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByGameApiAsync(gameId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Game>> GetAllAvailableByGameApiAsync(Guid gameId, bool available = true, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllAvailableByGameApiAsync(gameId, available, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Game>> GetAllApiAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllApiAsync(noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.Game?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultApiAsync(id, noTracking));
        }

        public async Task<BLLAppDTO.Game?> GetAvailableGameApiAsync(Guid gameId, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetAvailableGameApiAsync(gameId, noTracking));
        }
    }
}