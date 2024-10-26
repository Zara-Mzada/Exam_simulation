using System.Collections;

namespace Exam_simulation.Model;

public class Admin
{
    public string Name { get; set; }
    public string Password { get; set; }

    public Admin(string name, string password)
    {
        this.Name = name;
        this.Password = password;
    }
}