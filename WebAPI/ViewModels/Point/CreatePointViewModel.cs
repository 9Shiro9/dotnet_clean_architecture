using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.Point
{
    public class CreatePointViewModel
    {
        [Required(ErrorMessage ="Required MemberCode.")]
        public string MemberCode { get; set; }
        public string CouponCode { get; set; }

        [Required(ErrorMessage = "Required ReceiptNumber.")]
        public string ReceiptNumber { get; set; }
        public List<CreatePointItemViewModel> Items { get; set; } = new();

    }

    public class CreatePointItemViewModel
    {
        [Required(ErrorMessage = "Required ProductCode.")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Required Category.")]
        public string ProductType { get; set; }

        [Required(ErrorMessage = "Required Price.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Required Quantity.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Required TotalPrice.")]
        public decimal TotalPrice { get; set; }
    }
}
