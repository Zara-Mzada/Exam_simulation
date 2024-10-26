using System.Collections;

namespace Exam_simulation.Model;

public class Result
{
    public int Correct_answers { get; set; }
    public int Wrong_answers { get; set; }
    public int Empty_answers { get; set; }
    public decimal Quality_percentage { get; set; }
    public Result(int correct, int wrong, int empty, decimal quality)
    {
        this.Correct_answers = correct;
        this.Wrong_answers = wrong;
        this.Empty_answers = empty;
        this.Quality_percentage = quality;
    }
}