using AnimeInfo.AnimeQuiz;

public class QuizVM
{
    private Quiz _quiz;

    public QuizVM()
    {
        _quiz = new Quiz();
    }

    public List<Question> Questions => _quiz.Questions;
}