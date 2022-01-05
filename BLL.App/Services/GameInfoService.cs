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
    public class GameInfoService: BaseEntityService<IAppUnitOfWork, IGameInfoRepository, BLLAppDTO.GameInfo, DALAppDTO.GameInfo>, IGameInfoService
    {
        public GameInfoService(IAppUnitOfWork serviceUow, IGameInfoRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameInfoMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.GameInfo>> GetAllByTitleApiAsync(string title, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByTitleApiAsync(title, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.GameInfo>> GetAllWithCountsAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllWithCountsAsync(noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.GameInfo?> FirstOrDefaultWithCountsAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultWithCountsAsync(id, noTracking));
        }

        public async Task<IEnumerable<BLLAppDTO.GameInfo>> GetAllFiltered(List<Guid> categories, List<Guid> languages,
            List<Guid> publishers, int minCost, int maxCost, bool noTracking = true)
        {
            List<BLLAppDTO.GameInfo> resFiltered = new();
            var gameInfos = await GetAllWithCountsAsync();

            foreach (var gameInfo in gameInfos)
            {
                if (gameInfo.RentalCost < minCost || gameInfo.RentalCost > maxCost)
                {
                    continue;
                }
                
                if (publishers.Any())
                {
                    if (!publishers.Contains(gameInfo.PublisherId))
                    {
                        continue;
                    }
                }
                if (languages.Any())
                {
                    if (!languages.Contains(gameInfo.LanguageId))
                    {
                        continue;
                    }
                }
                if (categories.Any())
                {
                    var gameCategories = await ServiceUow.GameCategories.GetAllByGameApiAsync(gameInfo.Id);
                    var categoryExists = categories.Any(categoryId => gameCategories.Any(c => c.CategoryId == categoryId));

                    if (!categoryExists)
                    {
                        continue;
                    }
                }
                resFiltered.Add(gameInfo);
            }
            return resFiltered;
        }
    }
}