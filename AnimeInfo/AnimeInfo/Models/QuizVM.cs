using AnimeInfo.AnimeQuiz;

namespace AnimeInfo.Models
{
    public class QuizVM
    {
        private List<Question> _questions = new List<Question>();

        public Quiz()
        {
            Question q1 = new Question() { Q: "Which three series are known as the “Big Three” of anime/manga in the 2000s?" , 
                A: "Naruto, One Piece, and Bleach."}
        }
        public List<Question> Questions
        {
            get { return _questions; }
        }
    }
}



/* 
 *       Question: Which three series are known as the “Big Three” of anime/manga in the 2000s?
        Answer: Naruto, One Piece, and Bleach.

    Question: Who is the creator of Naruto?
        Answer: Masashi Kishimoto.

    Question: What does the term “shonen” refer to in the anime and manga industry?
        Answer: A genre targeting young male audiences, often featuring action, adventure, and coming-of-age themes.

    Question: Which magazine is known for publishing One Piece, Naruto, and Dragon Ball?
        Answer: Weekly Shonen Jump.

    Question: Who is the creator of Dragon Ball, a foundational series for modern shonen anime?
        Answer: Akira Toriyama.
 * 
 * 
 */