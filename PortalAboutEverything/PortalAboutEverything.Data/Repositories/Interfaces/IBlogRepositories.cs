using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories.Interfaces
{
    public interface IBlogRepositories : IBaseRepository<Post>
    {
        void AddComment(int postId, string text, string name);
        List<Post> GetAllWithCommentsBlog();
        int? GetDislikeCountByPostId(int postId);
        int? GetLikeCountByPostId(int postId);
        List<Post> GetPostsByUserId(int userId);
        List<Post> GetPostsWithCommentsBlogByUserId(int userId);
        void Update(Post post);
        void UpdateDislikeCountByPostId(int postId);
        void UpdateLikeCountByPostId(int postId);
    }
}