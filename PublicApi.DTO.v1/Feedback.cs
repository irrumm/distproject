using System;
using System.Globalization;

namespace PublicApi.DTO.v1
{
    public class FeedbackAdd
    {
        public string Comment { get; set; } = default!;

        public int Rating { get; set; }
        
        public Guid GameInfoId { get; set; }
    }

    public class Feedback
    {
        public Guid Id { get; set; }
        
        public string Comment { get; set; } = default!;

        public string GameInfoTitle { get; set; } = default!;

        public DateTime TimeAdded { get; set; }

        public int Rating { get; set; }
        
        public Guid GameInfoId { get; set; }

        public Guid AppUserId { get; set; }
        
        public string UserName { get; set; } = default!;
    }
}