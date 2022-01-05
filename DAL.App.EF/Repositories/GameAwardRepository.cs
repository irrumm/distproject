using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class GameAwardRepository : BaseRepository<DAL.App.DTO.GameAward, Domain.App.GameAward, AppDbContext>, IGameAwardRepository
    {
        public GameAwardRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameAwardMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.GameAward>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Award)
                .Include(g => g.GameInfo)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();

            return res!;
        }
        
        public override async Task<DAL.App.DTO.GameAward?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(g => g.Award)
                .Include(g => g.GameInfo);
                
            var res = Mapper.Map(await query.FirstOrDefaultAsync(f => f.Id == id));

            return res;
        }  
        
        public async Task<IEnumerable<DTO.GameAward>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking).Where(ga => ga.GameInfoId.Equals(gameId));

            var resQuery = query.Select(gameaward => new DAL.App.DTO.GameAward()
            {
                Id = gameaward.Id,
                AwardId = gameaward.Award!.Id,
                AwardName = gameaward.Award!.Name,
                YearWon = gameaward.YearWon,
                GameInfoId = gameaward.GameInfo!.Id,
                GameInfoTitle = gameaward.GameInfo!.Title,
                AwardHost = gameaward.Award!.Host
            });
            var res = await resQuery.ToListAsync();

            return res;
        }
        public async Task<IEnumerable<DTO.GameAward>> GetAllByAwardApiAsync(Guid awardId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking).Where(ga => ga.AwardId.Equals(awardId));

            var resQuery = query.Select(gameaward => new DAL.App.DTO.GameAward()
            {
                Id = gameaward.Id,
                AwardId = gameaward.Award!.Id,
                AwardName = gameaward.Award!.Name,
                YearWon = gameaward.YearWon,
                GameInfoId = gameaward.GameInfo!.Id,
                GameInfoTitle = gameaward.GameInfo!.Title,
                AwardHost = gameaward.Award!.Host
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<IEnumerable<DTO.GameAward>> GetAllApiAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(gameaward => new DAL.App.DTO.GameAward()
            {
                Id = gameaward.Id,
                AwardId = gameaward.Award!.Id,
                AwardName = gameaward.Award!.Name,
                YearWon = gameaward.YearWon,
                GameInfoId = gameaward.GameInfo!.Id,
                GameInfoTitle = gameaward.GameInfo!.Title,
                AwardHost = gameaward.Award!.Host
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<DTO.GameAward?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);
            
            var resQuery = query.Select(gameaward => new DAL.App.DTO.GameAward()
            {
                Id = gameaward.Id,
                AwardId = gameaward.Award!.Id,
                AwardName = gameaward.Award!.Name,
                YearWon = gameaward.YearWon,
                GameInfoId = gameaward.GameInfo!.Id,
                GameInfoTitle = gameaward.GameInfo!.Title,
                AwardHost = gameaward.Award!.Host
            });
            var res = await resQuery.FirstOrDefaultAsync(e => e!.Id.Equals(id));

            return res;
        }
    }
}