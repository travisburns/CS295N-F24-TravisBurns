namespace AnimeInfo.Models
{
    public class AppUser
    {
        public int Id { get; set; }  // This will be your primary key
        public string Name { get; set; }
        public DateTime SignUpdate { get; set; }
    }
}