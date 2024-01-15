using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Web.Models {
    public class ArticleTag {
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public int TagId { get; set; }
        [ForeignKey("ArticleId")]
        public Article? Article { get; set; }
        [ForeignKey("TagId")]
        public Tag? Tag { get; set; }
    }
}
