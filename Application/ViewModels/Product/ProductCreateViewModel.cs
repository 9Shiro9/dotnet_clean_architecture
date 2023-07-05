using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Product
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Required Product Name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required Product Description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required Product Price.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Required Product Quantity.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Required Product SupplierId.")]
        public string SupplierId { get; set; }
    }
}
