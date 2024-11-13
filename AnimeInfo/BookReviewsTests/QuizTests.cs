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
                Q = "Who is the author of One Piece?",
                A = "Eiichiro Oda",
                UserA = "Eiichiro Oda"  // Correct answer
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
                UserA = "Tite Kubo"  // Wrong answer (Kubo is Bleach's author)
            };
            //act
            bool isCorrect = quiz.checkAnswer(q2);
            //assert
            Assert.False(isCorrect);
        }
    }
}


