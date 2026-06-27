using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHARIBOY_ARTS.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string? Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }

        public ICollection<Comment> Replies { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
    }
}
