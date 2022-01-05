using Microsoft.AspNetCore.Mvc.Rendering;
using GameCategory = BLL.App.DTO.GameCategory;

namespace WebApp.ViewModels.GameCategories
{
    /// <summary>
    /// Create-edit viewmodel for GameCategory
    /// </summary>
    public class GameCategoryCreateEditViewModel
    {
        /// <summary>
        /// GameCategory
        /// </summary>
        public GameCategory GameCategory { get; set; } = default!;

        /// <summary>
        /// Select list of Categories
        /// </summary>
        public SelectList? CategorySelectList { get; set; }
        /// <summary>
        /// Select list of GameInfos
        /// </summary>
        public SelectList? GameInfoSelectList { get; set; }
    }
}