using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GameInfo: DomainEntityId
    {
        [MaxLength(64)] public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public int RentalCost { get; set; }

        public int ReplacementCost { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Today;

        [MaxLength(32)] public string ProductCode { get; set; } = default!;

        public string MainPictureUrl { get; set; } = default!;
        
        public IEnumerable<Guid>? GameAwards { get; set; }
        public IEnumerable<Guid>? GameCategories { get; set; }
        
        public Guid LanguageId { get; set; }
        public Language? Language { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        public string PublisherName { get; set; } = default!;
        public string LanguageName { get; set; } = default!;
    }
}