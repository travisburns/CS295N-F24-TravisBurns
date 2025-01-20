using AnimeInfo.AnimeQuiz;
using System.Threading;

public class Quiz
{
    private List<Question> _questions = new List<Question>();

    public Quiz()
    {
        _questions.Add(new Question
        {
            Q = "Which three series are known as the Big Three of anime/manga in the 2000s?",
            A = "Naruto, One Piece, and Bleach"
        });
        _questions.Add(new Question
        {
            Q = "Who is the creator of Naruto?",
            A = "Masashi Kishimoto"
        });
        _questions.Add(new Question
        {
            Q = "What does the term shonen refer to in the anime and manga industry?",
            A = "Weekly Shonen Jump"
        });
        _questions.Add(new Question
        {
            Q = "Who is the creator of Dragon Ball, a foundational series for modern shonen anime?",
            A = "Akira Toriyama"
        });
    }

    public List<Question> Questions => _questions;

    public bool checkAnswer(Question question)
    {
        if (string.IsNullOrEmpty(question.UserA))
            return false;

        return question.UserA.Equals(question.A, StringComparison.OrdinalIgnoreCase);
    }
}