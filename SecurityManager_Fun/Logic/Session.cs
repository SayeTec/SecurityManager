using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Logic
{
    public class Session
    {
        private static readonly Session instance = new Session();

        public Employee CurrentEmployee { get; private set; }

        private Session() { }

        public static Session Instance { get {  return instance; } }

        public void SetCurrentEmployee(Employee employee)
        {
            CurrentEmployee = employee;
        }

        public void ClearCurrentEmployee()
        {
            CurrentEmployee = null;
        }
    }
}
