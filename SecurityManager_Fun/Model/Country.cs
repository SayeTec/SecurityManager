using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
