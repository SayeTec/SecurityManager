using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Logic
{
    public class SessionManager
    {
        private static readonly SessionManager instance = new SessionManager();

        public Employee CurrentEmployee { get; private set; }

        private SessionManager() { }

        public static SessionManager Instance { get {  return instance; } }

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
