using AnimeInfo.Models;
public interface IBlogRepository
{
	Task <List<Blog>> GetReviews();
	Task <Blog?>  GetBlogById(int id);
	public Task <int> StoreReview(Blog model);
    public Task <List<Blog>> GetBlogs();
    public Task StoreBlog(Blog model);

    public Task AddComment(Comment comment);
}



