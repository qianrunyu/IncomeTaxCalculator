using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_GaryQian.Model
{
    public class EmployeeOutputDetail
    {
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("pay period")]
        public string Period { get; private set; }
        [JsonProperty("gross income")]
        public string GrossIncome { get; private set; }
        [JsonProperty("income tax")]
        public string IncomeTax { get; private set; }
        [JsonProperty("net income")]
        public string NetIncome { get; private set; }
        [JsonProperty("super")]
        public string Super { get; private set; }
        //to show "Err." in error cell, all output type is set to string.
        public EmployeeOutputDetail(string first_name, string last_name, string Period, double GrossIncome, double IncomeTax, double NetIncome, double Super)
        {
            Name = first_name.Trim() + " " + last_name.Trim();
            this.Period = Period;
            this.GrossIncome = GrossIncome == -1 ? Properties.Resources.ErrorCell : GrossIncome.ToString();
            this.IncomeTax = IncomeTax == -1 ? Properties.Resources.ErrorCell : IncomeTax.ToString();
            this.NetIncome = NetIncome==-1? Properties.Resources.ErrorCell : NetIncome.ToString();
            this.Super = Super == -1 ? Properties.Resources.ErrorCell : Super.ToString();
        }
    }
}
