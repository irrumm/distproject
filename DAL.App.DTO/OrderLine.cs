using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace DAL.App.DTO
{
    public class OrderLine: DomainEntityId
    {
        public Guid OrderId { get; set; }
        public Orders? Orders { get; set; }
        
        public Guid GameId { get; set; }
        public Game? Game { get; set; }
        
        public string GameInfoTitle { get; set; } = default!;
        
        public Guid GameInfoId { get; set; } = default!;
        
    }
}