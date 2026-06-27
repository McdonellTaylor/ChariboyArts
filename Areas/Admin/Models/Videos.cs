using System.ComponentModel.DataAnnotations;

namespace CHARIBOY_ARTS.Areas.Admin.Models
{
    public class Videos
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? FileName { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string? FilePath { get; set; }
    }
}
