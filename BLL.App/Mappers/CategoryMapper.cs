using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class CategoryMapper: BaseMapper<BLL.App.DTO.Category, DAL.App.DTO.Category>, IBaseMapper<BLL.App.DTO.Category, DAL.App.DTO.Category>
    {
        public CategoryMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}