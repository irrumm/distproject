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
    public class FeedbackRepository : BaseRepository<DAL.App.DTO.Feedback, Domain.App.Feedback, AppDbContext>, IFeedbackRepository
    {
        public FeedbackRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new FeedbackMapper(mapper))
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Feedback>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            
            if (!userId.Equals(default))
            {
                query = query.Where(f => f.AppUserId == userId);
            }

            var resQuery = query
                .Include(f => f.GameInfo)
                .Include(f => f.AppUser)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();

            return res!;
        }
        
        public override async Task<DAL.App.DTO.Feedback?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            
            if (!userId.Equals(default))
            {
                query = query.Where(f => f.AppUserId == userId);
            }

            query = query
                .Include(f => f.GameInfo)
                .Include(f => f.AppUser);
                
            var res = Mapper.Map(await query.FirstOrDefaultAsync(f => f.Id == id));

            return res;
        }

        public async Task<IEnumerable<DTO.Feedback>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking).Where(feedback => feedback.GameInfoId.Equals(gameId));

            var resQuery = query.Select(feedback => new DAL.App.DTO.Feedback()
            {
                Id = feedback.Id,
                AppUserId = feedback.AppUserId,
                Comment = feedback.Comment,
                GameInfoId = feedback.GameInfo!.Id,
                GameInfoTitle = feedback.GameInfo!.Title,
                Rating = feedback.Rating,
                TimeAdded = feedback.TimeAdded,
                UserName = feedback.AppUser!.Firstname
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<IEnumerable<DTO.Feedback>> GetAllByUserApiAsync(Guid user, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking).Where(f => f.AppUserId.Equals(user));

            var resQuery = query.Select(feedback => new DAL.App.DTO.Feedback()
            {
                Id = feedback.Id,
                AppUserId = feedback.AppUserId,
                Comment = feedback.Comment,
                GameInfoId = feedback.GameInfo!.Id,
                GameInfoTitle = feedback.GameInfo!.Title,
                Rating = feedback.Rating,
                TimeAdded = feedback.TimeAdded,
                UserName = feedback.AppUser!.Firstname
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<IEnumerable<DTO.Feedback>> GetAllApiAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            
            if (!userId.Equals(default))
            {
                query = query.Where(f => f.AppUserId == userId);
            }

            var resQuery = query.Select(feedback => new DAL.App.DTO.Feedback()
            {
                Id = feedback.Id,
                AppUserId = feedback.AppUserId,
                Comment = feedback.Comment,
                GameInfoId = feedback.GameInfo!.Id,
                GameInfoTitle = feedback.GameInfo!.Title,
                Rating = feedback.Rating,
                TimeAdded = feedback.TimeAdded,
                UserName = feedback.AppUser!.Firstname
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<DTO.Feedback?> FirstOrDefaultApiAsync(Guid id, Guid userId, bool noTracking = true)
        {
            IQueryable<Domain.App.Feedback> query;
            if (userId != default)
            {
                query = CreateQuery(userId, noTracking);
            }
            else
            {
                query = CreateQuery(default, noTracking);
            }
            
            var resQuery = query.Select(feedback => new DAL.App.DTO.Feedback()
            {
                Id = feedback.Id,
                AppUserId = feedback.AppUserId,
                Comment = feedback.Comment,
                GameInfoId = feedback.GameInfo!.Id,
                GameInfoTitle = feedback.GameInfo!.Title,
                Rating = feedback.Rating,
                TimeAdded = feedback.TimeAdded,
                UserName = feedback.AppUser!.Firstname
            });
            var res = await resQuery.FirstOrDefaultAsync(e => e!.Id.Equals(id));

            return res;
        }
    }
}