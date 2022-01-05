using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App
{
    public class Feedback: DomainEntityId
    {
        [MaxLength(255)] public string Comment { get; set; } = default!;
        
        public DateTime TimeAdded { get; set; } = DateTime.Now;

        public int Rating { get; set; }

        [ForeignKey("GameInfo")]
        public Guid? GameInfoId { get; set; }
        public GameInfo? GameInfo { get; set; }

        [ForeignKey("AppUser")]
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}