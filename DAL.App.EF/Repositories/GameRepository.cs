using System;
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
    public class GameRepository : BaseRepository<DAL.App.DTO.Game, Domain.App.Game, AppDbContext>, IGameRepository
    {
        public GameRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameMapper(mapper))
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Game>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.GameInfo)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();

            return res!;
        }
        
        public override async Task<DAL.App.DTO.Game?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(g => g.GameInfo);
                
            var res = Mapper.Map(await query.FirstOrDefaultAsync(g => g.Id == id));

            return res;
        }

        public async Task<IEnumerable<DTO.Game>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking).Where(g => g.GameInfoId.Equals(gameId));

            var resQuery = query.Select(game => new DAL.App.DTO.Game()
            {
                Id = game.Id,
                Available = game.Available,
                GameInfoId = game.GameInfo!.Id,
                GameInfoTitle = game.GameInfo!.Title,
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<IEnumerable<DTO.Game>> GetAllAvailableByGameApiAsync(Guid gameId, bool available = true, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking).Where(g => g.GameInfoId.Equals(gameId) && g.Available == available);

            var resQuery = query.Select(game => new DAL.App.DTO.Game()
            {
                Id = game.Id,
                Available = game.Available,
                GameInfoId = game.GameInfo!.Id,
                GameInfoTitle = game.GameInfo!.Title,
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<IEnumerable<DTO.Game>> GetAllApiAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking).OrderBy(game => game.GameInfo!.Title);

            var resQuery = query.Select(game => new DAL.App.DTO.Game()
            {
                Id = game.Id,
                Available = game.Available,
                GameInfoId = game.GameInfo!.Id,
                GameInfoTitle = game.GameInfo!.Title,
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<DTO.Game?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);
            
            var resQuery = query.Select(game => new DAL.App.DTO.Game()
            {
                Id = game.Id,
                Available = game.Available,
                GameInfoId = game.GameInfo!.Id,
                GameInfoTitle = game.GameInfo!.Title,
            });
            var res = await resQuery.FirstOrDefaultAsync(e => e!.Id.Equals(id));

            return res;
        }

        public async Task<Game?> GetAvailableGameApiAsync(Guid gameId, bool noTracking = true)
        {
            var games = await GetAllAvailableByGameApiAsync(gameId, noTracking);
            var enumerable = games as Game[] ?? games.ToArray();
            if (enumerable.Any())
            {
                return enumerable.First();
            }

            return null;
        }
    }
}