using Microsoft.EntityFrameworkCore;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Data
{
    public class AppDBContext : DbContext
    {
        //Local DB config
        /*private static string server = "localhost";
        private static string database = "SecurityManagerTest";
        private static string user = "root";
        private static string password = "";

        private static readonly string MYSQL_CONNECTION_CONFIG = $"SERVER={server};DATABASE={database};USER={user};PASSWORD={password}";
*/
        //Global DB config
        private static string server = "mysqlserver41.database.windows.net,1433";
        private static string database = "SecurityManagerTest";
        private static string user = "tekla";
        private static string password = "Maciek123";

        private static readonly string MYSQL_CONNECTION_CONFIG = $"Server=tcp:{server};Initial Catalog={database};Persist Security Info=False;User ID={user};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=300;";


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentDeduction> PaymentDeductions { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(MYSQL_CONNECTION_CONFIG);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.GrossRate)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Deduction>()
                .Property(d => d.Value)
                .HasColumnType("decimal(10,4)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Value)
                .HasColumnType("decimal(10,4)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
