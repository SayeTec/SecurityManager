namespace SecurityManager_Fun.Model
{
    internal class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal GrossRate { get; set; }

    }
}
