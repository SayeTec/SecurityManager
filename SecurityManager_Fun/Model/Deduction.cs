using SecurityManager_Fun.Data;

namespace SecurityManager_Fun.Model
{
    public class Deduction
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DeductionType Type { get; set; }
        public bool IsPercentage { get; set; }
        public decimal Value { get; set; }
        public int? CountryID { get; set; }

        public enum DeductionType 
        {
            Tax = 1, Bonus = 2
        }

        // Navigation property
        public Country? Country { get; set; }

        public string NameAndValue { get => $"{Name} : {(IsPercentage ? Value.ToString("F3") + "%" : Value.ToString("F2") + "$")}"; }

        public override string ToString()
        {
            using (var context = new AppDBContext())
            {
                return $"{ID}: {Name} Description:" +
                    $"{(Description == null ? "None" : Description)} Type:" +
                    $"{Type} Value:" +
                    $"{Value}{(IsPercentage ? "%" : "$")} Country:" +
                    $"{context.Countries.Find(CountryID)}";
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Deduction deduction &&
                   ID == deduction.ID &&
                   Name == deduction.Name &&
                   Description == deduction.Description &&
                   Type == deduction.Type &&
                   IsPercentage == deduction.IsPercentage &&
                   Value == deduction.Value /*&&
                   Country?.Equals(deduction.Country) == true*/;
        }
    }
}
