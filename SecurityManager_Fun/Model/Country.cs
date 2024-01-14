namespace SecurityManager_Fun.Model
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public override string ToString()
        {
            return $"{ID}: {Name} Symbol: " +
                $"{Symbol}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Country other = (Country)obj;
            return Name == other.Name && Symbol == other.Symbol;
        }
    }
}
