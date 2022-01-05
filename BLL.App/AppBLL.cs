using System;
using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Base.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace BLL.App
{
    public class AppBLL: BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }

        public IAddressService Addresses => 
            GetService<IAddressService>(() => new AddressService(Uow, Uow.Addresses, Mapper));
        
        public IAwardService Awards => 
            GetService<IAwardService>(() => new AwardService(Uow, Uow.Awards, Mapper));
        
        public ICategoryService Categories => 
            GetService<ICategoryService>(() => new CategoryService(Uow, Uow.Categories, Mapper));
        
        public IFeedbackService Feedbacks => 
            GetService<IFeedbackService>(() => new FeedbackService(Uow, Uow.Feedbacks, Mapper));
        
        public IGameAwardService GameAwards => 
            GetService<IGameAwardService>(() => new GameAwardService(Uow, Uow.GameAwards, Mapper));
        
        public IGameCategoryService GameCategories => 
            GetService<IGameCategoryService>(() => new GameCategoryService(Uow, Uow.GameCategories, Mapper));
        
        public IGameInfoService GameInfos => 
            GetService<IGameInfoService>(() => new GameInfoService(Uow, Uow.GameInfos, Mapper));
        
        public IGameService Games => 
            GetService<IGameService>(() => new GameService(Uow, Uow.Games, Mapper));
        
        public ILanguageService Languages => 
            GetService<ILanguageService>(() => new LanguageService(Uow, Uow.Languages, Mapper));
        
        public IOrderLineService OrderLines => 
            GetService<IOrderLineService>(() => new OrderLineService(Uow, Uow.OrderLines, Mapper));
        
        public IOrdersService Orders => 
            GetService<IOrdersService>(() => new OrdersService(Uow, Uow.Orders, Mapper));
        
        public IPaymentMethodService PaymentMethods => 
            GetService<IPaymentMethodService>(() => new PaymentMethodService(Uow, Uow.PaymentMethods, Mapper));
        
        public IPublisherService Publishers => 
            GetService<IPublisherService>(() => new PublisherService(Uow, Uow.Publishers, Mapper));
        
        public IGamePictureService GamePictures => 
            GetService<IGamePictureService>(() => new GamePictureService(Uow, Uow.GamePictures, Mapper));
        
    }
}