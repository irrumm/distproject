using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class GameInfo: DomainEntityId
    {
        [MaxLength(64)] public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public int RentalCost { get; set; }

        public int ReplacementCost { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Today;

        [MaxLength(32)] public string ProductCode { get; set; } = default!;

        public string MainPictureUrl { get; set; } =
            "https://img.favpng.com/22/10/12/question-mark-desktop-wallpaper-grey-computer-icons-png-favpng-FTNv8p4eRd8kVubahAEhbJXCc.jpg";
        
        public ICollection<Award>? GameAwards { get; set; }
        public ICollection<Category>? GameCategories { get; set; }
        public ICollection<Feedback>? GameFeedbacks { get; set; }
        public ICollection<GamePicture>? GamePictures { get; set; }
        
        [ForeignKey("Language")]
        public Guid? LanguageId { get; set; }
        public Language? Language { get; set; }

        [ForeignKey("Publisher")]
        public Guid? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        
    }
}