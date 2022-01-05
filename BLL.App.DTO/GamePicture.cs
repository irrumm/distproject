using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace BLL.App.DTO
{
    public class GamePicture : DomainEntityId
    {
        [MaxLength(128)] public string PictureUrl { get; set; } = default!;
        
        public DateTime DateAdded { get; set; } = DateTime.Today;
        
        public Guid GameInfoId { get; set; }
        public GameInfo? GameInfo { get; set; }
        
        public string GameInfoTitle { get; set; } = default!;
    }
}