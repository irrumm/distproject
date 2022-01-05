using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class FeedbackMapper: BaseMapper<BLL.App.DTO.Feedback, DAL.App.DTO.Feedback>, IBaseMapper<BLL.App.DTO.Feedback, DAL.App.DTO.Feedback>
    {
        public FeedbackMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}