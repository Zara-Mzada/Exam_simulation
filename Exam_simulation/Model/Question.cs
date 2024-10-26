namespace Exam_simulation.Model;

public class Question
{
    public string Content { get; set; }
    public Dictionary<string, string> Answers { get; set; }
    public string Correct_answer { get; set; }
    public int Categ_ID { get; set; }
    public int Question_ID { get; set; }

    public Question(string content, Dictionary<string, string> answer, string correct_key, int categ_id)
    {
        this.Content = content;
        this.Answers = answer;
        this.Correct_answer = correct_key;
        this.Categ_ID = categ_id;
        this.Question_ID = GeneratePass();
    }
    
    public int GeneratePass()
    {
        Random random = new Random();
        int password = random.Next(1000, 10000);
        return password;
    }
}