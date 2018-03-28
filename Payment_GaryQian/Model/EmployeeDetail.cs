using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_GaryQian.Model
{
    public class EmployeeDetail
    {
        [JsonProperty("first name")]
        public string FirstName { get; set; }
        [JsonProperty("last name")]
        public string LastName { get; set; }
        [JsonProperty("annual salary")]
        public string AnnualSalary { get; set; }
        [JsonProperty("super rate (%)")]
        public string SuperRate { get; set; }
        [JsonProperty("payment start date")]
        public string Period { get; set; }
        public EmployeeDetail(string first_name, string last_name, string salary, string super_rate, string period)
        {
            FirstName = first_name;
            LastName = last_name;
            AnnualSalary = salary;
            SuperRate = super_rate;
            Period = period;
        }
    }
}
