using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using DAL.App.DTO.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Identity;
using AppUser = Domain.App.Identity.AppUser;

namespace DAL.App.DTO
{
    public class Feedback: DomainEntityId
    {
        public string Comment { get; set; } = default!;

        public string GameInfoTitle { get; set; } = default!;

        public DateTime TimeAdded { get; set; }

        public int Rating { get; set; }

        public string UserName = default!;
        public Guid GameInfoId { get; set; }
        public GameInfo? GameInfo { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}