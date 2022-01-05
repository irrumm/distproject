using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PublisherRepository : BaseRepository<DAL.App.DTO.Publisher, Domain.App.Publisher, AppDbContext>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PublisherMapper(mapper))
        {
        }

    }
}