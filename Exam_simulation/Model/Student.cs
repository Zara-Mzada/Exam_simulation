namespace Exam_simulation.Model;

public class Student
{
    public string FullName { get; set; }
    public int? Password { get; set; }
    public int Exam_ID { get; set; }

    public Student(string fullName, int examId, int? password = null)
    {
        this.FullName = fullName;
        this.Password = password ?? GeneratePass();
        this.Exam_ID = examId;
    }
    
    private int GeneratePass()
    {
        Random random = new Random();
        int password = random.Next(1000, 10000);
        return password;
    }
}