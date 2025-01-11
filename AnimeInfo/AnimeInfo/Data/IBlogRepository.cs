using AnimeInfo.Models;
using System;

public interface IBlogRepository
{
	public List<Blog> GetReviews();
	public Blog?  GetBlogById(int id);
	public int StoreReview(Blog model);
    public List <Blog> GetBlogs();
    void StoreBlog(Blog model);
}



