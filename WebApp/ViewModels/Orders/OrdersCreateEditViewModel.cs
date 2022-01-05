using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Orders
{
    /// <summary>
    /// Create-edit viewmodel for Order
    /// </summary>
    public class OrderCreateEditViewModel
    {
        /// <summary>
        /// Order
        /// </summary>
        public BLL.App.DTO.Orders Orders { get; set; } = default!;

        /// <summary>
        /// Select list of Addresses
        /// </summary>
        public SelectList? AddressSelectList { get; set; }
        /// <summary>
        /// Select list of PaymentMethods
        /// </summary>
        public SelectList? PaymentMethodSelectList { get; set; }
    }
}