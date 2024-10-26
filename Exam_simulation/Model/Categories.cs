namespace Exam_simulation.Model.Categories;

public class Categories
{
    public string Category_Name { get; set; }
    public int Category_ID { get; set; }
    public static int counter = 0;

    public Categories(string name)
    {
        counter++;
        Category_ID = counter;
        this.Category_Name = name;
    }
}