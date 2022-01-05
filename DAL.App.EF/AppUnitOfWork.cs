using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }
        
        public IAddressRepository Addresses => 
            GetRepository(() => new AddressRepository(UowDbContext, Mapper));
        public IAwardRepository Awards => 
            GetRepository(() => new AwardRepository(UowDbContext, Mapper));
        public ICategoryRepository Categories => 
            GetRepository(() => new CategoryRepository(UowDbContext, Mapper));
        public IFeedbackRepository Feedbacks => 
            GetRepository(() => new FeedbackRepository(UowDbContext, Mapper));
        public IGameAwardRepository GameAwards => 
            GetRepository(() => new GameAwardRepository(UowDbContext, Mapper));
        public IGameCategoryRepository GameCategories => 
            GetRepository(() => new GameCategoryRepository(UowDbContext, Mapper));
        public IGameInfoRepository GameInfos => 
            GetRepository(() => new GameInfoRepository(UowDbContext, Mapper));
        public IGameRepository Games => 
            GetRepository(() => new GameRepository(UowDbContext, Mapper));
        public ILanguageRepository Languages => 
            GetRepository(() => new LanguageRepository(UowDbContext, Mapper));
        public IOrderLineRepository OrderLines => 
            GetRepository(() => new OrderLineRepository(UowDbContext, Mapper));
        public IOrdersRepository Orders => 
            GetRepository(() => new OrdersRepository(UowDbContext, Mapper));
        public IPaymentMethodRepository PaymentMethods => 
            GetRepository(() => new PaymentMethodRepository(UowDbContext, Mapper));
        public IPublisherRepository Publishers => 
            GetRepository(() => new PublisherRepository(UowDbContext, Mapper));
        public IGamePictureRepository GamePictures => 
            GetRepository(() => new GamePictureRepository(UowDbContext, Mapper));
    }
}