using Microsoft.AspNetCore.Mvc.Rendering;
using GameInfo = BLL.App.DTO.GameInfo;

namespace WebApp.ViewModels.GameInfos
{
    /// <summary>
    /// Create-edit viewmodel for GameInfo
    /// </summary>
    public class GameInfoCreateEditViewModel
    {
        /// <summary>
        /// GameInfo
        /// </summary>
        public GameInfo GameInfo { get; set; } = default!;

        /// <summary>
        /// Select list of Languages
        /// </summary>
        public SelectList? LanguageSelectList { get; set; }
        /// <summary>
        /// Select list of Publishers
        /// </summary>
        public SelectList? PublisherSelectList { get; set; }
    }
}