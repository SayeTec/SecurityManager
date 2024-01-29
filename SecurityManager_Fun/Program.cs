using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System.Reflection.Metadata.Ecma335;
using System.Text;

public class Program
{
    public static void Main(String[] args)
    {
        byte[] salt;
        string hash = AccountService.HashPassword("123", out salt);

        Console.WriteLine(ValuesValidation.ValidatePassword(null));

        using (var context = new AppDBContext())
        {
            int answer = 0;

            while (true)
            {
                Console.WriteLine("Which table you want to show?" +
                    "\n 1. Countries" +
                    "\n 2. Departments" +
                    "\n 3. Employees" +
                    "\n 4. Roles" +
                    "\n 5. Settlements" +
                    "\n 6. Work Schedule" +
                    "\n 0. Exit");
                Console.Write("Selection: ");
                answer = Convert.ToInt32(Console.ReadLine());

                switch (answer)
                {
                    case 1:
                        Console.WriteLine("\n\tCountries:");
                        context.Countries.Select(c => c).ToList().ForEach(c => Console.WriteLine("\n" + c + "\n")); 
                        break;

                    case 2:
                        Console.WriteLine("\n\tDepartments:");
                        context.Departments.Select(d => d).ToList().ForEach(d => Console.WriteLine("\n" + d + "\n"));
                        break;
                    
                    case 3:
                        Console.WriteLine("\n\tEmployees:");
                        context.Employees.Select(e => e).ToList().ForEach(e => Console.WriteLine("\n" + e + "\n"));
                        break;

                    case 5:
                        Console.WriteLine("\n\tDeductions:");
                        context.Deductions.Select(s => s).ToList().ForEach(s => Console.WriteLine("\n" + s + "\n"));
                        break;

                    case 0:
                        return;
                }
            }
            /*context.Add(new Employee
            {
                Name = "Tomek",
                Surname = "Robak",
                Email = "tomek.robak@gmail.com",
                Password = "Password",
                Phone = "+48 134-123-122",
                Login = "tomek.robak",
                GrossRate = 4000,
                RoleID = context.Roles.FirstOrDefault(r => r.Code.Equals("WK")).ID,
                DepartmentID = context.Departments.FirstOrDefault(d =>
                               d.CountryID.Equals(
                                   context.Countries.FirstOrDefault(c =>
                                   c.Symbol.Equals("GBR")).ID)).ID
            });*/

            /*context.Add(new Country
            {
                Name = "Great Britain",
                Symbol = "GBR"

            });*/

            context.SaveChanges();

            /*context.Roles.Select(r=>r).ToList().ForEach(r =>context.Remove(r));
            context.SaveChanges();*/

            //context.Roles.Select(r => r).ToList().ForEach(r => { if (r != null) Console.WriteLine(r); } );
        }

        
    }
}