using Microsoft.AspNetCore.Mvc.Rendering;
using Game = BLL.App.DTO.Game;

namespace WebApp.ViewModels.Games
{
    /// <summary>
    /// Create-edit viewmodel for Game
    /// </summary>
    public class GameCreateEditViewModel
    {
        /// <summary>
        /// Game
        /// </summary>
        public Game Game { get; set; } = default!;

        /// <summary>
        /// Select list of GameInfos
        /// </summary>
        public SelectList? GameInfoSelectList { get; set; }
    }
}