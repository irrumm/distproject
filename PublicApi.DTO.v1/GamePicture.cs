using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class GamePictureAdd
    {
        [MaxLength(128)] public string PictureUrl { get; set; } = default!;

        public Guid GameInfoId { get; set; }
    }

    public class GamePicture
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] public string PictureUrl { get; set; } = default!;
        
        public DateTime DateAdded { get; set; } = DateTime.Today;
        
        public string GameInfoTitle { get; set; } = default!;        
        
        public Guid GameInfoId { get; set; }
    }
}