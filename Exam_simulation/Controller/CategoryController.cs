using Exam_simulation.Model.Categories;

namespace Exam_simulation.Controller;

public class CategoryController
{
    public List<Categories> Categories { get; set; }

    public CategoryController()
    {
        Categories = new List<Categories>();
    }
    
    public List<Categories> GetCategories()
    {
        foreach (Categories category in Categories)
        {
            Console.WriteLine("======================================\n" +
                              $"Category id: {category.Category_ID}\n" +
                              $"Category name: {category.Category_Name}\n");
        }

        return Categories;
    }

    public bool CheckCategory(int id)
    {
        foreach (Categories category in Categories)
        {
            if (id == category.Category_ID)
            {
                return true;
            }
        }

        return false;
    }

    public List<Categories> UpdateCategory(int id, string newName)
    {
        foreach (Categories category in Categories)
        {
            if (id == category.Category_ID)
            {
                category.Category_Name = newName;
            }
        }
        return Categories;
    }

    public List<Categories> DeleteCategory(int id)
    {
        foreach (Categories category in Categories)
        {
            if (id == category.Category_ID)
            {
                Categories.RemoveAt(Categories.IndexOf(category));
            }
        }

        return Categories;
    }
}