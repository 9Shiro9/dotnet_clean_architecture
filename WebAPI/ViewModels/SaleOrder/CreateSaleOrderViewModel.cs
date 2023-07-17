using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.SaleOrder
{
    public class CreateSaleOrderViewModel
    {
        [Required(ErrorMessage = "Required OrderNumber.")]
        public string OrderNumber { get; set; }

        [Required(ErrorMessage = "Required CustomerId.")]
        public string CustomerId { get; set; }
        public List<CreateSaleOrderItemViewModel> Items { get; set; }
    }

    public class CreateSaleOrderItemViewModel
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
