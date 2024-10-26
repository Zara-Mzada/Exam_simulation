using System;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Exam_simulation.Controller;
using Exam_simulation.Model;


namespace Exam_simulation.Services
{
    public static class FileService<T>
    {
        public static string pathFolder = "/Users/zara/RiderProjects/Exam_simulation";
        public static string folderName = "/AllFiles/";
        public static DirectoryInfo myDir;

        public static void CreateDir()
        {
            if (!Directory.Exists(pathFolder+folderName))
            {
                myDir = Directory.CreateDirectory(pathFolder+folderName);
            }
        }

        public static void CreateFile(string fileName)
        {
            if (!File.Exists(pathFolder+folderName+fileName+".txt"))
            {
                File.Create(pathFolder + folderName + fileName+".txt");
            }
        }

        public static void WriteFile<T>(string path, List<T> data)
        {
            string filePath = Path.Combine(pathFolder + folderName + path + ".txt");
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(filePath);
                sw.Write(json);  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
        

        public static List<Student> GetAllStudents(string path, StudentController studentController)
        {
            List<Student> data;
            using (StreamReader sr = new StreamReader(pathFolder+folderName+path+".txt"))
            {
                string alldata = sr.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<Student>>(alldata);
                if (data != null)
                {
                    studentController.Students.AddRange(data);
                }
        
                return studentController.Students;
            }
        }
        
        public static List<Question> GetAllQuestions(string path, QuestionController questionController)
        {
            List<Question> data;
            using (StreamReader sr = new StreamReader(pathFolder+folderName+path+".txt"))
            {
                string allquest = sr.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<Question>>(allquest);
                if (data != null)
                {
                    questionController.Questions.AddRange(data);
                }
        
                return questionController.Questions;
            }
        }
    }
}