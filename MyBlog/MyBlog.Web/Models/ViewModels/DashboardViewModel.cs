namespace MyBlog.Web.Models.ViewModels {
    public class DashboardViewModel {
        public int ArticleCount { get; set; }
        public int ProjectCount { get; set; }
        public int SkillCount { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}
