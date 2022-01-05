using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaymentMethodRepository : BaseRepository<DAL.App.DTO.PaymentMethod, Domain.App.PaymentMethod, AppDbContext>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PaymentMethodMapper(mapper))
        {
        }

    }
}