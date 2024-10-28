using System.Collections;
using System.Text.RegularExpressions;
using Exam_simulation.Model;

namespace Exam_simulation.Controller;

public class QuestionController
{
    public List<Question> Questions { get; set; }
    // public StudentController studentController = new StudentController();
    public QuestionController()
    {
        Questions = new List<Question>();
    }
    
    public List<Question> GetQuestions()
    {
        foreach (Question question in Questions)
        {
            Console.WriteLine("=================\n" +
                              $"Question id: {question.Question_ID}\n" +
                              $"Question content: {question.Content}");
        }

        return Questions;
    }
    

    public bool CheckQuestionID(int id)
    {
        foreach (Question question in Questions)
        {
            if (id == question.Question_ID)
            {
                return true;
            }
        }
        return false;
    }

    public List<Question> UpdateQuestion(int id, string newContent, Dictionary<string, string> newAnswers, string correctAnswer)
    {
        foreach (Question question in Questions)
        {
            if (question.Question_ID == id)
            {
                question.Content = newContent;
                question.Answers = newAnswers;
                question.Correct_answer = correctAnswer;
            }
        }
        return Questions;
    }
    
    
    public List<Question> DeleteQuestion(int id)
    {
        for (int i = Questions.Count - 1; i >= 0; i--)
        {
            if (Questions[i].Question_ID == id)
            {
                Questions.RemoveAt(i);
            }
        }
        return Questions;
    }


    public int exam;
    public void SelectQuestion(string fullname, int pass, StudentController studentController)
    {
        foreach (Student student in studentController.Students)
        {
            if (student.FullName == fullname && student.Password == pass)
            {
                exam = student.Exam_ID;
            }
        }
    }
    
    public List<string> Answers = new List<string>();
    public List<Question> shuffledList;
    public List<Question> ShuffleQuestions()
    {
        List<Question> newQuestions = new List<Question>();
        foreach (Question question in Questions)
        {
            if (exam == question.Categ_ID)
            {
                newQuestions.Add(question);
            }
        }
        Random random = new Random();
        shuffledList = newQuestions.OrderBy(quest => random.Next()).ToList();
        int id = 1;
        foreach (Question question in shuffledList)
        {
            Console.Write($"Question {id++}\n");
            
            foreach (var answer in question.Answers)
            {
                Console.WriteLine($"{answer.Key}) {answer.Value}");
            }
            Console.Write("Enter answer: ");
            string student_answer = Console.ReadLine();
            Answers.Add(student_answer);
        }
        
        return shuffledList;
    }
    

    public int correct_answers = 0;
    public int wrong_answers = 0;
    public int empty_answers = 0;
    public void CheckAnswer()
    {
        correct_answers = 0;
        wrong_answers = 0;
        empty_answers = 0;
        string notEmpty = @"^(?!\s*$).+";
        for (int i = 0; i < shuffledList.Count; i++)
        {
            if (Regex.IsMatch(Answers[i], notEmpty))
            {
                if (shuffledList[i].Correct_answer == Answers[i])
                {
                    correct_answers++;
                }
                else
                {
                    wrong_answers++;
                }
            }
            else
            {
                empty_answers++;
            }
        }
    }
}

