using Microsoft.AspNetCore.Mvc.Rendering;
using OrderLine = BLL.App.DTO.OrderLine;

namespace WebApp.ViewModels.OrderLines
{
    /// <summary>
    /// Create-edit viewmodel for OrderLine
    /// </summary>
    public class OrderLineCreateEditViewModel
    {
        /// <summary>
        /// OrderLine
        /// </summary>
        public OrderLine OrderLine { get; set; } = default!;

        /// <summary>
        /// Select list of Games
        /// </summary>
        public SelectList? GameSelectList { get; set; }
    }
}