using SecurityManager_Fun.Data;
using System.Xml.Linq;

namespace SecurityManager_Fun.Model
{
    public class Department
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public int CountryID { get; set; }

        // Navigation property
        public Country Country { get; set; }

        public override string ToString()
        {
            using (var context = new AppDBContext())
            {
                return $"{ID}: {Address} Capacity:" +
                    $"{Capacity} Country:" +
                    $"{context.Countries.Find(CountryID)}";
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Department other = (Department)obj;
            return Address == other.Address && Capacity == other.Capacity && Country.Equals(other.Country);
        }
    }
}
