using System;
using System.Collections;
using System.Text.RegularExpressions;
using Exam_simulation.Controller;
using Exam_simulation.Model;
using Exam_simulation.Model.Categories;
using Exam_simulation.Services;

public class Program
{
    public static void Main(string[] args)
    {
        // Regex
        string isNumeric = @"^\d+$";
        string notEmpty = @"^(?!\s*$).+";
        string onlyString = @"^[a-zA-Z]+$";
        string fullName = @"^[A-Z][a-z]+(?: [A-Z][a-z]+)?$";
        ///////////////////////////////

        Admin admin1 = new Admin("Zara", "z1234");
        AdminController adminController = new AdminController(admin1);
        CategoryController categoryController = new CategoryController();
        StudentController studentController = new StudentController();
        QuestionController questionController = new QuestionController();
        //------------------
        Categories cat1 = new Categories("IT");
        categoryController.Categories.Add(cat1);
        Categories cat2 = new Categories("Programming");
        categoryController.Categories.Add(cat2);
        Categories cat3 = new Categories("DevOps");
        categoryController.Categories.Add(cat3);
        //-------------------
        
        // Program part
        
        if (Directory.Exists(FileService<Student>.pathFolder+FileService<Student>.folderName))
        {
            FileService<Student>.GetAllStudents("Student", studentController);
            FileService<Question>.GetAllQuestions("Question", questionController);
        }
        else
        {
            FileService<DirectoryInfo>.CreateDir();
            FileService<Student>.CreateFile("Student");
            FileService<Question>.CreateFile("Question");
            goto ReEnter;
        }
        ReEnter:
        Console.Write("Welcome! Are you Admin or Student (a/s): ");
        var role = Console.ReadLine();
        if (role == "a" || role == "A")
        {
            ReAdmin:
            Console.Write("Enter your name: ");
            string admin_name = Console.ReadLine();
            Console.Write("Enter your password: ");
            string admin_password = Console.ReadLine();
            if (Regex.IsMatch(admin_name, notEmpty) && Regex.IsMatch(admin_password, notEmpty))
            {
                goto CreateAdmin;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong name or password! Enter again");
                goto ReAdmin;
            }
            
            CreateAdmin:
            Admin admin = new Admin(admin_name, admin_password);
            if (adminController.SignInAdmin(admin))
            {
                ReChoice:
                Console.Write("What do you want to do:\n" +
                              "1. Add student\n" +
                              "2. Add category\n" +
                              "3. Add question\n" +
                              "4. Update category\n" +
                              "5. Update question\n" +
                              "6. Delete category\n" +
                              "7. Delete question\n" +
                              "8. Exit\n" +
                              "Enter your choose: ");

                string admin_choice = Console.ReadLine();
                if (admin_choice == "1")
                {
                    ReStuName:
                    Console.Write("Enter student name: ");
                    string student_name = Console.ReadLine();
                    if (Regex.IsMatch(student_name, fullName))
                    {
                        categoryController.GetCategories();
                        ReStuID:
                        Console.Write("Enter exam category id: ");
                        var id_con = Console.ReadLine();
                        if (Regex.IsMatch(id_con, isNumeric))
                        {
                            int examId = Convert.ToInt32(id_con);
                            if (categoryController.CheckCategory(examId))
                            {
                                Student student = new Student(student_name, examId);
                                studentController.Students.Add(student);
                                studentController.GetStudents();
                                goto ReEnter;
                            }
                            else
                            {
                                Console.WriteLine("This id doesn't exist! Enter again");
                                goto ReStuID;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Id must be integer! Enter again");
                            goto ReStuID;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong data format! Enter again");
                        goto ReStuName;
                    }
                }
                else if (admin_choice == "2")
                {
                    Console.Write("Enter category name: ");
                    string cat_name = Console.ReadLine();
                    Categories category = new Categories(cat_name);
                    ReCon:
                    Console.Write("Do you want to add question?(y/n): ");
                    string continue_ = Console.ReadLine();
                    if (continue_ == "y")
                    {
                        Console.Write("Enter question content: ");
                        string question_content = Console.ReadLine();
                        ReAmount:
                        Console.Write("How many variants do you want to add: ");
                        var num = Console.ReadLine();
                        Dictionary<string, string> answers = new Dictionary<string, string>();
                        if (Regex.IsMatch(num, isNumeric))
                        {
                            int amount = Convert.ToInt32(num);
                            for (int i = 0; i < amount; i++)
                            {
                                Console.Write("Enter the variant: ");
                                string key = Console.ReadLine();
                                Console.Write("Enter the variant content: ");
                                string value = Console.ReadLine();
                                if (!answers.ContainsKey(key))
                                {
                                    answers.Add(key, value);
                                }
                                else
                                {
                                    Console.WriteLine("The key already exists! Enter again");
                                    i--;
                                }
                            }

                            Console.Write("Enter correct variant's key: ");
                            string answer = Console.ReadLine();
                            Question question = new Question(question_content, answers, answer, category.Category_ID);
                            questionController.Questions.Add(question);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Wrong data format! Enter again");
                            goto ReAmount;
                        }


                    }
                    else if (continue_ == "n")
                    {
                        goto ReEnter;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wrong choice! Enter again!");
                        goto ReCon;
                    }
                    goto ReChoice;
                }
                else if (admin_choice == "3")
                {
                    ReQue:
                    Console.Write("Enter question content: ");
                    string question_content = Console.ReadLine();
                    if (Regex.IsMatch(question_content, notEmpty))
                    {
                        goto ReAmount;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("It can't be empty enter again");
                        goto ReQue;
                    }
                    ReAmount:
                    Console.Write("How many variants do you want to add: ");
                    var num = Console.ReadLine();
                    Dictionary<string, string> answers = new Dictionary<string, string>();
                    string correct_key;
                    int cat_id = 0;
                    if (Regex.IsMatch(num, isNumeric))
                    {
                        int amount = Convert.ToInt32(num);
                        for (int i = 0; i < amount; i++)
                        {
                            ReVar:
                            Console.Write("Enter the variant: ");
                            string key = Console.ReadLine();
                            if (Regex.IsMatch(key, notEmpty))
                            {
                                goto ReCon;
                            }
                            else
                            {
                                Console.WriteLine("Wrong data format! Enter again");
                                goto ReVar;
                            }
                            ReCon:
                            Console.Write("Enter the variant content: ");
                            string value = Console.ReadLine();
                            if (Regex.IsMatch(value, notEmpty))
                            {
                                if (!answers.ContainsKey(key))
                                {
                                    answers.Add(key, value);
                                }
                                else
                                {
                                    Console.WriteLine("The key already exists! Enter again");
                                    i--;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Wrong data format! Enter again!");
                                goto ReCon;
                            }
                        }

                        ReCorAns:
                        Console.Write("Enter correct variant's key: ");
                        correct_key = Console.ReadLine();
                        if (Regex.IsMatch(correct_key, notEmpty) && answers.ContainsKey(correct_key))
                        {
                            goto ReCat;
                        }
                        else
                        {
                            Console.WriteLine("Wrong correct answer! Enter again");
                            goto ReCorAns;
                        }
                        ReCat:
                        Console.Write("Which category do you want to add, enter name of category: ");
                        string cat_name = Console.ReadLine();
                        bool isFound = false;
                        foreach (Categories category in categoryController.Categories)
                        {
                            if (category.Category_Name == cat_name)
                            {
                                cat_id = category.Category_ID;   
                                isFound = true;
                                break;
                            }
                        }

                        if (!isFound)
                        {
                            Console.WriteLine("Category doesn't exist! Enter again");
                            goto ReCat;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wrong choice! Enter again");
                        goto ReChoice;
                    }

                    Question question = new Question(question_content, answers, correct_key, cat_id);
                    questionController.Questions.Add(question);
                    goto ReChoice;
                }
                else if (admin_choice == "4")
                {
                    categoryController.GetCategories();
                    ReCatID:
                    Console.Write("Enter category id which you want to update: ");
                    var cat_id = Console.ReadLine();
                    if (Regex.IsMatch(cat_id, isNumeric))
                    {
                        int id = Convert.ToInt32(cat_id);
                        if (categoryController.CheckCategory(id))
                        {
                            ReName:
                            Console.Write("Enter new name: ");
                            string newName = Console.ReadLine();
                            if (Regex.IsMatch(newName, notEmpty))
                            {
                                categoryController.UpdateCategory(id, newName);
                                Console.WriteLine("Updated successfully...");
                                goto ReChoice;
                            }
                            else
                            {
                                Console.WriteLine("It can't be empty! Enter again");
                                goto ReName;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong id! Enter again");
                            goto ReCatID;
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Wrong data format! Enter again!");
                        goto ReCatID;
                    }
                }
                else if (admin_choice == "5")
                {
                    questionController.GetQuestions();
                    string new_content;
                    Dictionary<string, string> answers;
                    string correct_key;
                    ReID:
                    Console.Write("Enter question id which you want to update: ");
                    var id_con = Console.ReadLine();
                    if (Regex.IsMatch(id_con, isNumeric))
                    {
                        int id = Convert.ToInt32(id_con);
                        if (questionController.CheckQuestionID(id))
                        {
                            ReNewCon:
                            Console.Write("Enter new content: ");
                            new_content = Console.ReadLine();
                            if (Regex.IsMatch(new_content, notEmpty))
                            {
                                ReAmount:
                                Console.Write("How many variants do you want to create: ");
                                var amount_con = Console.ReadLine();
                                if (Regex.IsMatch(amount_con, isNumeric))
                                {
                                    int amount = Convert.ToInt32(amount_con);
                                    answers = new Dictionary<string, string>();
                                    for (int i = 0; i < amount; i++)
                                    {
                                        ReVar:
                                        Console.Write("Enter the variant: ");
                                        string key = Console.ReadLine();
                                        if (Regex.IsMatch(key, notEmpty))
                                        {
                                            goto ReCon;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Wrong data format! Enter again");
                                            goto ReVar;
                                        }
                                        ReCon:
                                        Console.Write("Enter the variant content: ");
                                        string value = Console.ReadLine();
                                        if (Regex.IsMatch(value, notEmpty))
                                        {
                                            if (!answers.ContainsKey(key))
                                            {
                                                answers.Add(key, value);
                                            }
                                            else
                                            {
                                                Console.WriteLine("The key already exists! Enter again");
                                                i--;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Wrong data format! Enter again!");
                                            goto ReCon;
                                        }
                                    }
                                    ReCorAns:
                                    Console.Write("Enter correct variant's key: ");
                                    correct_key = Console.ReadLine();
                                    if (Regex.IsMatch(correct_key, notEmpty) && answers.ContainsKey(correct_key))
                                    {
                                        questionController.UpdateQuestion(id, new_content, answers, correct_key);
                                        goto ReChoice;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong correct answer! Enter again");
                                        goto ReCorAns;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Wrong data format! Enter again");
                                    goto ReAmount;
                                }
                            }
                            else
                            {
                                Console.WriteLine("It can't be empty! Enter again");
                                goto ReNewCon;
                            }
                        }
                        else
                        {
                            Console.WriteLine("This id doesn't exist! Enter again");
                            goto ReID;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wrong data format or id! Enter again!");
                        goto ReID;
                    }
                }
                else if (admin_choice == "6")
                {
                    categoryController.GetCategories();
                    ReIdDel:
                    Console.Write("Enter category id you want to delete: ");
                    var id_con = Console.ReadLine();
                    if (Regex.IsMatch(id_con, isNumeric))
                    {
                        int id = Convert.ToInt32(id_con);
                        if (categoryController.CheckCategory(id))
                        {
                            categoryController.DeleteCategory(id);
                            goto ReChoice;
                        }
                        else
                        {
                            Console.WriteLine("This id doesn't exist! Enter again");
                            goto ReIdDel;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong data format! Enter again");
                        goto ReIdDel;
                    }
                }
                else if (admin_choice == "7")
                {
                    questionController.GetQuestions();
                    ReIdQueDel:
                    Console.Write("Enter question id you want to delete: ");
                    var id_con = Console.ReadLine();
                    if (Regex.IsMatch(id_con, isNumeric))
                    {
                        int id = Convert.ToInt32(id_con);
                        if (questionController.CheckQuestionID(id))
                        {
                            questionController.DeleteQuestion(id);
                            goto ReChoice;
                        }
                        else
                        {
                            Console.WriteLine("This id doesn't exist! Enter again");
                            goto ReIdQueDel;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong data format! Enter again");
                        goto ReIdQueDel;
                    }
                }
                else if (admin_choice == "8")
                {
                    Console.WriteLine("Process ended...");
                    FileService<Student>.WriteFile("Student", studentController.Students);
                    FileService<Question>.WriteFile("Question", questionController.Questions);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong name or password! Enter again");
                    goto ReAdmin;
                }

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong name or password! Enter again");
                goto ReEnter;
            }
        }
        else if (role == "s" || role == "S")
        {
                ReName:
                Console.Write("Enter your fullname: ");
                string fullname = Console.ReadLine();
                RePass:
                Console.Write("Enter your password: ");
                var pass_con = Console.ReadLine();
                if (Regex.IsMatch(pass_con, isNumeric))
                {
                    int pass = Convert.ToInt32(pass_con);
                    if (studentController.CheckStudent(fullname, pass))
                    {
                        questionController.SelectQuestion(fullname, pass, studentController);
                        questionController.ShuffleQuestions();
                        questionController.CheckAnswer();
                        int totalAnswers = questionController.correct_answers + questionController.wrong_answers +
                                           questionController.empty_answers;
                        decimal quality_percentage = totalAnswers > 0 ? (questionController.correct_answers / (decimal)totalAnswers) * 100 : 0;
                        Result result = new Result(questionController.correct_answers, questionController.wrong_answers, questionController.empty_answers, quality_percentage);
                        ResultController resultController = new ResultController();
                        resultController.Results.Add(result);
                        
                        resultController.ShowResult(fullname, questionController.correct_answers, questionController.wrong_answers, questionController.empty_answers, quality_percentage);
                        
                        RePrint:
                        Console.Write("Do you want to print your result? (y/n): ");
                        string student_choice = Console.ReadLine();
                        if (student_choice == "y")
                        {
                            ReFile:
                            Console.Write("Enter new file name (ex: myResult): ");
                            string fileName = Console.ReadLine();
                            if (Regex.IsMatch(fileName, notEmpty))
                            {
                                FileService<Result>.WriteFile(fileName, resultController.Results);
                                goto ReEnter;
                            }
                            else
                            {
                                goto ReFile;
                            }
                        }
                        else if (student_choice == "n")
                        {
                            goto ReEnter;
                        }
                        else
                        {
                            Console.WriteLine("Wrong choice! Enter again");
                            goto RePrint;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wrong name or password! Enter again");
                        goto ReName;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong data format! Enter again");
                    goto RePass;
                }
        }
        else
        {
                Console.Clear();
                Console.WriteLine("Wrong choice! Enter again!");
                goto ReEnter;
        }

    }
}