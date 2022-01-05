using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class GameInfoRepository : BaseRepository<DAL.App.DTO.GameInfo, Domain.App.GameInfo, AppDbContext>,
        IGameInfoRepository
    {
        public GameInfoRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameInfoMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.GameInfo>> GetAllAsync(Guid userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Language)
                .Include(g => g.Publisher)
                .Include(g => g.GamePictures)
                .Include(g => g.GameFeedbacks)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<DAL.App.DTO.GameInfo?> FirstOrDefaultAsync(Guid id, Guid userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(g => g.Language)
                .Include(g => g.Publisher)
                .Include(g => g.GamePictures)
                .Include(g => g.GameFeedbacks);

            var res = Mapper.Map(await query.FirstOrDefaultAsync(f => f.Id == id));

            return res;
        }

        public async Task<IEnumerable<DTO.GameInfo>> GetAllByTitleApiAsync(string title, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query
                .Where(g => g.Title.ToLower().Contains(title.ToLower()))
                .Select(gameinfo => new DAL.App.DTO.GameInfo()
                {
                    Id = gameinfo.Id,
                    Title = gameinfo.Title,
                    Description = gameinfo.Description,
                    DateAdded = gameinfo.DateAdded,
                    RentalCost = gameinfo.RentalCost,
                    ReplacementCost = gameinfo.ReplacementCost,
                    MainPictureUrl = gameinfo.MainPictureUrl,
                    PublisherId = gameinfo.Publisher!.Id,
                    PublisherName = gameinfo.Publisher!.Name,
                    LanguageId = gameinfo.Language!.Id,
                    LanguageName = gameinfo.Language!.Name,
                    ProductCode = gameinfo.ProductCode
                });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<IEnumerable<DTO.GameInfo>> GetAllWithCountsAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(gameinfo => new DAL.App.DTO.GameInfo()
            {
                Id = gameinfo.Id,
                Title = gameinfo.Title,
                Description = gameinfo.Description,
                DateAdded = gameinfo.DateAdded,
                RentalCost = gameinfo.RentalCost,
                ReplacementCost = gameinfo.ReplacementCost,
                MainPictureUrl = gameinfo.MainPictureUrl,
                PublisherId = gameinfo.Publisher!.Id,
                PublisherName = gameinfo.Publisher!.Name,
                LanguageId = gameinfo.Language!.Id,
                LanguageName = gameinfo.Language!.Name,
                ProductCode = gameinfo.ProductCode
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<DTO.GameInfo?> FirstOrDefaultWithCountsAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(gameinfo => new DAL.App.DTO.GameInfo()
            {
                Id = gameinfo.Id,
                Title = gameinfo.Title,
                Description = gameinfo.Description,
                DateAdded = gameinfo.DateAdded,
                RentalCost = gameinfo.RentalCost,
                ReplacementCost = gameinfo.ReplacementCost,
                PublisherId = gameinfo.Publisher!.Id,
                MainPictureUrl = gameinfo.MainPictureUrl,
                PublisherName = gameinfo.Publisher!.Name,
                LanguageId = gameinfo.Language!.Id,
                LanguageName = gameinfo.Language!.Name,
                ProductCode = gameinfo.ProductCode

            });
            var res = await resQuery.FirstOrDefaultAsync(e => e!.Id.Equals(id));

            return res;
        }
    }
}
