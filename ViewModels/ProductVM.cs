using CHARIBOY_ARTS.Data;
using CHARIBOY_ARTS.Models;

namespace CHARIBOY_ARTS.ViewModels
{
    public class ProductVM
    {
        public Product? Product { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public string? UserName { get; set; }
    }
}
