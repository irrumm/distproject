using Microsoft.AspNetCore.Mvc.Rendering;
using GamePicture = BLL.App.DTO.GamePicture;

namespace WebApp.ViewModels.GamePictures
{
    /// <summary>
    /// Create-edit viewmodel for GamePicture
    /// </summary>
    public class GamePictureCreateEditViewModel
    {
        /// <summary>
        /// GamePicture
        /// </summary>
        public GamePicture GamePicture { get; set; } = default!;

        /// <summary>
        /// Select list of GameInfos
        /// </summary>
        public SelectList? GameInfoSelectList { get; set; }
    }
}