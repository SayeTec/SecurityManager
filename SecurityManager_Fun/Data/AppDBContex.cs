using Microsoft.EntityFrameworkCore;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Data
{
    internal class AppDBContex : DbContext
    {
        private static string server = "localhost";
        private static string database = "test";
        private static string user = "root";
        private static string password = "";

        private static readonly string MYSQL_CONNECTION_CONFIG = $"server={server};database={database};user={user};password={password};";

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySQL(MYSQL_CONNECTION_CONFIG);
    }
}
