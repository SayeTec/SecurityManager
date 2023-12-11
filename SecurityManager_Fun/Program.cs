using SecurityManager_Fun.Data;
using SecurityManager_Fun.Model;

public class Program
{
    public static void Main(String[] args)
    {
        using (var context = new AppDBContex())
        {
            context.Add(new Role
            {
                ID = null,
                Name="Admin",
                Code="AD"
            });
            context.SaveChanges();
        }
    }
}