using System;

namespace PublicApi.DTO.v1
{
    public class GameCategoryAdd
    {
        public Guid GameInfoId { get; set; }
        
        public Guid CategoryId { get; set; }
    }

    public class GameCategory
    {
        public Guid Id { get; set; }
        
        public string GameInfoTitle { get; set; } = default!;
        
        public string CategoryName { get; set; } = default!;        
        
        public Guid GameInfoId { get; set; }
        
        public Guid CategoryId { get; set; }
    }
}