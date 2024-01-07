using Microsoft.EntityFrameworkCore;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Data
{
    public class AppDBContext : DbContext
    {
        //Local DB config
        /*private static string server = "localhost";
        private static string database = "test";
        private static string user = "root";
        private static string password = "";*/

        //Global DB config
        private static string server = "mysqlserver41.database.windows.net,1433";
        private static string database = "SecurityManagerTest";
        private static string user = "tekla";
        private static string password = "Maciek123";

        private static readonly string MYSQL_CONNECTION_CONFIG = $"Server=tcp:{server};Initial Catalog={database};Persist Security Info=False;User ID={user};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(MYSQL_CONNECTION_CONFIG);
    }
}
