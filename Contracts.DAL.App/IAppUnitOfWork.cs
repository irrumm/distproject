using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IAddressRepository Addresses { get; }
        IAwardRepository Awards { get; }
        ICategoryRepository Categories { get; }
        IFeedbackRepository Feedbacks { get; }
        IGameAwardRepository GameAwards { get; }
        IGameCategoryRepository GameCategories { get; }
        IGameInfoRepository GameInfos { get; }
        IGameRepository Games { get; }
        ILanguageRepository Languages { get; }
        IOrderLineRepository OrderLines { get; }
        IOrdersRepository Orders { get; }
        IPaymentMethodRepository PaymentMethods { get; }
        IPublisherRepository Publishers { get; }
        IGamePictureRepository GamePictures { get; }
    }
}