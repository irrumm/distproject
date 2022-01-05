using System.Linq;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CategoryRepository : BaseRepository<DAL.App.DTO.Category, Domain.App.Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CategoryMapper(mapper))
        {
        }
    }
}