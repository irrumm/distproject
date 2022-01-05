using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class GameCategory: DomainEntityId
    {
        [ForeignKey("GameInfo")]
        public Guid? GameInfoId { get; set; }
        public GameInfo? GameInfo { get; set; }
        
        
        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        
    }
}