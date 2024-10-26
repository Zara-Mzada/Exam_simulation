using System.Collections;
using Exam_simulation.Model;

namespace Exam_simulation.Controller;

public class ResultController
{
    public List<Result> Results { get; set; }

    public ResultController()
    {
        Results = new List<Result>();
    }

    public void ShowResult(string fullname, int correct, int wrong, int empty, decimal quality)
    {
        Console.WriteLine($"Fullname: {fullname}\n" +
                          $"Correct answers: {correct}\n" +
                          $"Wrong answers: {wrong}\n" +
                          $"Empty answers: {empty}\n" +
                          $"Quality: {quality}");
    }
}