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
    }
}
