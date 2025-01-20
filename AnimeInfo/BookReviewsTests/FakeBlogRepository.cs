using AnimeInfo.Data;
using AnimeInfo.Models;

namespace AnimeInfo.Tests
{
    public class FakeBlogRepository : IBlogRepository
    {
        private List<Blog> blogs = new List<Blog>();

        public List<Blog> GetBlogs()
        {
            return blogs;
        }

        public List<Blog> GetReviews()
        {
            return blogs;  // Since this is the same as GetBlogs in your implementation
        }

        public Blog GetBlogById(int id)
        {
            return blogs.Find(b => b.Id == id);
        }

        public int StoreReview(Blog model)
        {
            if (model != null)
            {
                model.Id = blogs.Count + 1;
                blogs.Add(model);
                return 1;
            }
            return 0;
        }

        public void StoreBlog(Blog model)
        {
            if (model != null)
            {
                model.Id = blogs.Count + 1;
                blogs.Add(model);
            }
        }
    }
}