using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.App.DTO;
using Domain.Base;

namespace BLL.App.DTO
{
    public class GameCategory: DomainEntityId
    {
        public Guid GameInfoId { get; set; }
        public GameInfo? GameInfo { get; set; }
        
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public string GameInfoTitle { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
    }
}