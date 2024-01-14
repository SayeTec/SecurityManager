namespace SecurityManager_Fun.Model
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public PriorityType Priority { get; set; }

        public enum PriorityType
        {
            MainAdmin = 0,
            Admin = 1,
            Basic = 2
        }

        public override string ToString()
        {
            return $"{ID}: {Name} => {Code}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Role other = (Role)obj;
            return Name == other.Name && Code == other.Code && Priority == other.Priority;
        }

    }
}
