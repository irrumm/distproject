using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GameAward: DomainEntityId
    {
        [MaxLength(4)] public string YearWon { get; set; } = default!;
        
        public Guid GameInfoId { get; set; }
        public GameInfo? GameInfo { get; set; }
        
        public Guid AwardId { get; set; }
        public Award? Award { get; set; }
        
        public string GameInfoTitle { get; set; } = default!;
        
        public string AwardName { get; set; } = default!;

        public string AwardHost { get; set; } = default!;

    }
}