using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CHARIBOY_ARTS.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        [ValidateNever]
        [DisplayName("Upload An Image")]
        public string? ImageUrl { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Total Price")]
        public decimal PriceTotal { get; set; }
        [Required]
        [DisplayName("Accepted Discount")]
        public decimal PriceDiscount { get; set; }
        [Required]
        [DisplayName("Total Discount")]
        public decimal PriceDiscountTotal { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
