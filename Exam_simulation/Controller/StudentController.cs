using System.Collections;
using Exam_simulation.Model;

namespace Exam_simulation.Controller;

public class StudentController
{
    public List<Student> Students;

    public StudentController()
    {
        Students = new List<Student>();
    }
    
    // Function
    public List<Student> GetStudents()
    {
        foreach (Student student in Students)
        {
            Console.WriteLine($"{student.FullName}\n" +
                              $"{student.Password}");
        }

        return Students;
    }
    
    public bool CheckStudent(string fullname, int pass)
    {
        foreach (Student student in Students)
        {
            if (student.FullName == fullname && student.Password == pass)
            {
                return true;
            }
        }

        return false;
    }
}