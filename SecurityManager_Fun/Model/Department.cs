using SecurityManager_Fun.Data;

namespace SecurityManager_Fun.Model
{
    public class Department
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public int CountryID { get; set; }

        public override string ToString()
        {
            using (var context = new AppDBContext())
            {
                return $"{ID}: {Address} Capacity:" +
                    $"{Capacity} Country:" +
                    $"{context.Countries.Find(CountryID)}";
            }
        }
    }
}
