namespace AnimeInfo.AnimeQuiz
{
    public class Question
    {
        public string Q { get; set; } = string.Empty;
        public string A { get; set; } = string.Empty;
        public string UserA { get; set; } = string.Empty;
        public bool IsRight { get; set; }
    }
}