using System;
using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class FeedbackMapper: BaseMapper<PublicApi.DTO.v1.Feedback, BLL.App.DTO.Feedback>
    {
        public static BLL.App.DTO.Feedback MapToBll(PublicApi.DTO.v1.FeedbackAdd feedback)
        {
            return new BLL.App.DTO.Feedback()
            {
                Comment = feedback.Comment,
                GameInfoId = feedback.GameInfoId,
                Rating = feedback.Rating,
                TimeAdded = DateTime.Now
            };
        }

        public FeedbackMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}