﻿namespace SecurityManager_Fun.Model
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            return $"{ID}: {Name} => {Code}";
        }
    }
}
