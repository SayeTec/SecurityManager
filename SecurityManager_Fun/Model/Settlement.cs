using Org.BouncyCastle.Crypto.Utilities;
using SecurityManager_Fun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Model
{
    internal class Settlement
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public SettlementType Type { get; set; }
        public bool IsPercentage { get; set; }
        public decimal Value { get; set; }
        public int CountryID { get; set; }

        public enum SettlementType //TODO: Is it appropriate implementation, or must it be a new table?
        {
            None = 0, Tax = 1, Bonus = 2
        }

        public override string ToString()
        {
            using (var context = new AppDBContex())
            {
                return $"{ID}: {Name} Description:" +
                    $"{(Description == null ? "None" : Description)} Type:" +
                    $"{Type} Value:" +
                    $"{Value}{(IsPercentage ? "%" : "$")} Country:" +
                    $"{context.Countries.Find(CountryID)}";
            } 
        }
    }
}
