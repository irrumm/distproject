using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class GameInfoAdd
    {
        [MaxLength(64)] public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public int RentalCost { get; set; }

        public int ReplacementCost { get; set; }

        public string MainPictureUrl { get; set; } = default!;
        
        [MaxLength(32)] public string ProductCode { get; set; } = default!;
        
        public Guid LanguageId { get; set; }
        
        public Guid PublisherId { get; set; }
    }

    public class GameInfo
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)] public string Title { get; set; } = default!;

        [MaxLength(255)] public string Description { get; set; } = default!;

        public int RentalCost { get; set; }

        public int ReplacementCost { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Today;

        [MaxLength(32)] public string ProductCode { get; set; } = default!;

        public string MainPictureUrl { get; set; } = default!;
        
        public string PublisherName { get; set; } = default!;
        
        public string LanguageName { get; set; } = default!;        
        
        public Guid LanguageId { get; set; }
        
        public Guid PublisherId { get; set; }
        
    }
}