using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class GameAward: DomainEntityId
    {
        public string YearWon { get; set; } = default!;

        [ForeignKey("GameInfo")]
        public Guid? GameInfoId { get; set; }
        public GameInfo? GameInfo { get; set; }
        
        [ForeignKey("Award")]
        public Guid? AwardId { get; set; }
        public Award? Award { get; set; }

    }
}