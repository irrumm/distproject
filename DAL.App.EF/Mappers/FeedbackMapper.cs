using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class FeedbackMapper: BaseMapper<DAL.App.DTO.Feedback, Domain.App.Feedback>, IBaseMapper<DAL.App.DTO.Feedback, Domain.App.Feedback>
    {
        public FeedbackMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}