﻿using SecurityManager_Fun.Data;

namespace SecurityManager_Fun.Model
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? RoleID { get; set; }
        public int? DepartmentID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal GrossRate { get; set; }

        // Navigation properties
        public Role Role { get; set; }
        public Department Department { get; set; }

        public override string ToString()
        {
            using (var context = new AppDBContext())
            {
                return $"{ID}: {Name} {Surname} Contacts: " +
                    $"{Phone} {Email} System login:" +
                    $"{Login} Role:" +
                    $"{RoleID}. {context.Roles.Find(RoleID)} Department:" +
                    $"{DepartmentID}. {context.Departments.Find(DepartmentID)}";
            }
        }
    }
}
