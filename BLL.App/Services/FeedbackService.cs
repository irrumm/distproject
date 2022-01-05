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
    public class FeedbackService: BaseEntityService<IAppUnitOfWork, IFeedbackRepository, BLLAppDTO.Feedback, DALAppDTO.Feedback>, IFeedbackService
    {
        public FeedbackService(IAppUnitOfWork serviceUow, IFeedbackRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new FeedbackMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.Feedback>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByGameApiAsync(gameId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Feedback>> GetAllByUserApiAsync(Guid user, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByUserApiAsync(user, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Feedback>> GetAllApiAsync(Guid userId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllApiAsync(userId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.Feedback?> FirstOrDefaultApiAsync(Guid id, Guid userId, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultApiAsync(id, userId, noTracking));
        }
    }
}