using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Game: DomainEntityId
    {
        public Guid GameInfoId { get; set; }
        public GameInfo? GameInfo { get; set; }
        
        public bool Available { get; set; }
        
        public string GameInfoTitle { get; set; } = default!;
    }
}