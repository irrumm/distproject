using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class PublisherMapper: BaseMapper<BLL.App.DTO.Publisher, DAL.App.DTO.Publisher>, IBaseMapper<BLL.App.DTO.Publisher, DAL.App.DTO.Publisher>
    {
        public PublisherMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}