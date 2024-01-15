using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Models {
    public class Tag {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(32)]
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
