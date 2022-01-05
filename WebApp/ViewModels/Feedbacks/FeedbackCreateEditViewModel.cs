using Microsoft.AspNetCore.Mvc.Rendering;
using Feedback = BLL.App.DTO.Feedback;

namespace WebApp.ViewModels.Feedbacks
{
    /// <summary>
    /// Create-edit viewmodel for Feedback
    /// </summary>
    public class FeedbackCreateEditViewModel
    {
        /// <summary>
        /// Feedback
        /// </summary>
        public Feedback Feedback { get; set; } = default!;

        /// <summary>
        /// Select list of GameInfos
        /// </summary>
        public SelectList? GameInfoSelectList { get; set; }
    }
}