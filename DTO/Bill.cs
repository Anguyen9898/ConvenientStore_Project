using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Bill
    {
        public string billCode { get; set; }
        //public string iD { get; set; }
        //public string cusCode { get; set; }
        public DateTime day { get; set; }
        public double total { get; set; }
        public Bill(string billCode, DateTime day, double total)
        {
            this.billCode = billCode;
            //this.iD = iD;
            //this.cusCode = cusCode;
            this.day = day;
            this.total = total;
        }
    }
}
