using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class OrderLine: DomainEntityId
    {
        [ForeignKey("Orders")]
        public Guid? OrderId { get; set; }
        public Orders? Orders { get; set; }
        
        [ForeignKey("Game")]
        public Guid? GameId { get; set; }
        public Game? Game { get; set; }
        
    }
}