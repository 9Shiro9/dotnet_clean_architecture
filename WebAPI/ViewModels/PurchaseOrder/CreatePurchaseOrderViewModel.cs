using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.PurchaseOrder
{
    public class CreatePurchaseOrderViewModel
    {
        [Required(ErrorMessage = "Required OrderNumber.")]
        public string OrderNumber { get; set; }

        public List<CreatePurchaseOrderItemViewModel> Items { get; set; }
    }

    public class CreatePurchaseOrderItemViewModel
    {
        [Required(ErrorMessage = "Required ProductId.")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Required Quantity.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Required UnitPrice.")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Required TotalPrice.")]
        public decimal TotalPrice { get; set; }
    }

}
