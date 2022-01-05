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
    public class GamePictureService: BaseEntityService<IAppUnitOfWork, IGamePictureRepository, BLLAppDTO.GamePicture, DALAppDTO.GamePicture>, IGamePictureService
    {
        public GamePictureService(IAppUnitOfWork serviceUow, IGamePictureRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GamePictureMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.GamePicture>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByGameApiAsync(gameId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.GamePicture>> GetAllApiAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllApiAsync(noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.GamePicture?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultApiAsync(id, noTracking));
        }
    }
}