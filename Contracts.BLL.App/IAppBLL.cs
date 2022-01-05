using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;
using Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IAddressService Addresses { get; }
        IAwardService Awards { get; }
        ICategoryService Categories { get; }
        IFeedbackService Feedbacks { get; }
        IGameAwardService GameAwards { get; }
        IGameCategoryService GameCategories { get; }
        IGameInfoService GameInfos { get; }
        IGameService Games { get; }
        ILanguageService Languages { get; }
        IOrderLineService OrderLines { get; }
        IOrdersService Orders { get; }
        IPaymentMethodService PaymentMethods { get; }
        IPublisherService Publishers { get; }
        IGamePictureService GamePictures { get; }
    }
}