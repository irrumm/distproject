using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class GameAwardAdd
    {
        public string YearWon { get; set; } = default!;

        public Guid GameInfoId { get; set; }

        public Guid AwardId { get; set; }
    }

    public class GameAward
    {
        public Guid Id { get; set; }
        
        public string YearWon { get; set; } = default!;

        public string GameInfoTitle { get; set; } = default!;
                
        public string AwardName { get; set; } = default!;

        public string AwardHost { get; set; } = default!;
        
        public Guid GameInfoId { get; set; }

        public Guid AwardId { get; set; }
    }
}