using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CategoryMapper: BaseMapper<PublicApi.DTO.v1.Category, BLL.App.DTO.Category>
    {
        public static BLL.App.DTO.Category MapToBll(PublicApi.DTO.v1.CategoryAdd category)
        {
            return new BLL.App.DTO.Category()
            {
                Name = category.Name
            };
        }

        public CategoryMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}