using AnimeInfo.AnimeQuiz;
namespace BookReviewsTests
{
    public class QuizTests
    {
        Quiz quiz = new Quiz();
        Question q1;
        Question q2;

        [Fact]
        public void checkCorrectAnswer()
        {
            //arrange 
            Quiz quiz = new Quiz();
            Question q1 = new Question()
            {
                Q = "Who is the creator of Dragon Ball, a foundational series for modern shonen anime?",
                A = "Akira Toriyama",
                UserA = "Akira Toriyama"  // Correct answer
            };
            //act
            bool isCorrect = quiz.checkAnswer(q1);
            //assert
            Assert.True(isCorrect);
        }

        [Fact]
        public void checkWrongAnswer()
        {
            //arrange 
            Quiz quiz = new Quiz();
            Question q2 = new Question()
            {
                Q = "Who is the author of Naruto?",
                A = "Masashi Kishimoto",
                UserA = "Tite Kubo"  // Wrong answer 
            };
            //act
            bool isCorrect = quiz.checkAnswer(q2);
            //assert
            Assert.False(isCorrect);
        }
    }
}


