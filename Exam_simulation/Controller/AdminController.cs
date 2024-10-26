using System.Collections;
using Exam_simulation.Model;

namespace Exam_simulation.Controller;

public class AdminController
{
    public ArrayList Admins { get; set; }

    public AdminController(Admin admin)
    {
        Admins = new ArrayList();
        Admins.Add(admin);
    }
    
    // Functions
    public bool SignInAdmin(Admin admin)
    {
        foreach (Admin ad in Admins)
        {
            if (admin.Name == ad.Name && admin.Password == ad.Password)
            {
                return true;
            }
        }
        return false;
    }
}