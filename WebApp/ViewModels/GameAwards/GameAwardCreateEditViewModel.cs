using Microsoft.AspNetCore.Mvc.Rendering;
using GameAward = BLL.App.DTO.GameAward;

namespace WebApp.ViewModels.GameAwards
{
    /// <summary>
    /// Create-edit viewmodel for GameAward
    /// </summary>
    public class GameAwardCreateEditViewModel
    {
        /// <summary>
        /// GameAward
        /// </summary>
        public GameAward GameAward { get; set; } = default!;

        /// <summary>
        /// Select list of Awards
        /// </summary>
        public SelectList? AwardSelectList { get; set; }
        /// <summary>
        /// Select list of GameInfos
        /// </summary>
        public SelectList? GameInfoSelectList { get; set; }
    }
}